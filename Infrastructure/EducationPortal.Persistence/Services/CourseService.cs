using EducationPortal.Application.Abstractions.IBackgroundServices;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace EducationPortal.Persistence.Services
{
    public class CourseService : ICourseService
    {
        readonly UserManager<User> _userManager;
        readonly ICourseWriteRepository _courseWriteRepository;
        readonly ICourseReadRepository _courseReadRepository;
        readonly INotificationService _notificationService;
        readonly ICourseBackgroundJobs _courseBackgroundJobs;
        readonly EduPortalDbContext _dbContext;
        readonly ICurrentUserService _currentUserService;
        public CourseService(UserManager<User> userManager, ICourseWriteRepository courseRepository, ICourseReadRepository courseReadRepository, INotificationService notificationService, ICourseBackgroundJobs courseBackgroundJobs, EduPortalDbContext dbContext, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _courseWriteRepository = courseRepository;
            _courseReadRepository = courseReadRepository;
            _notificationService = notificationService;
            _courseBackgroundJobs = courseBackgroundJobs;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        public async Task<bool> AssignCoursesToUserAsync(Guid userId, int[] courseIds, string assignedBy)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var courses = await _courseReadRepository.GetWhere(c => courseIds.Contains(c.Id) && !c.IsDeleted).ToListAsync();
            if (courses.Count != courseIds.Length) return false;

            if (user.UserCourses == null)
            {
                user.UserCourses = new List<UserCourse>();
            }

            foreach (var course in courses)
            {
                if (!user.UserCourses.Any(uc => uc.CourseId == course.Id))
                {
                    user.UserCourses.Add(new UserCourse { UserId = user.Id, CourseId = course.Id, AssignedBy = assignedBy, IsAssigned = true });
                    _courseBackgroundJobs.ScheduleReminders(user.Id, course.Title, course.StartDate, course.EndDate);
                }
            }
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(int courseId)
        {
            var course = await _courseReadRepository.GetSingleAsync(x => x.Id == courseId && !x.IsDeleted, tracking: true);

            if (course == null) return false;

            course.IsDeleted = true;
            course.DeletedBy = _currentUserService.Username ?? "Unknown User !";

            var userCourses = await _dbContext.Set<UserCourse>()
                .Where(uc => uc.CourseId == courseId)
                .ToListAsync();

            _dbContext.Set<UserCourse>().RemoveRange(userCourses);

            _courseWriteRepository.Update(course);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

