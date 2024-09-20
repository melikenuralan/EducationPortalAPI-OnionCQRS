using AutoMapper;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.Profile;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Persistence.Services
{
    public class UserCourseService : IUserCourseService
    {
        readonly UserManager<User> _userManager;
        readonly ICurrentUserService _currentUserService;
        private readonly EduPortalDbContext _context;
        readonly INotificationService _notificationService;
        readonly IMapper _mapper;
        public UserCourseService(EduPortalDbContext context, ICurrentUserService currentUserService, UserManager<User> userManager, INotificationService notificationService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<bool> MarkCourseAsFavoriteAsync(int courseId)
        {
            var userId = _currentUserService.UserId;
            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
            if (userCourse == null) return false;

            userCourse.IsFavorite = true;
            _context.UserCourses.Update(userCourse);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveFavoriteCourseAsync(int courseId)
        {
            var userId = _currentUserService.UserId;
            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse == null) return false;

            userCourse.IsFavorite = false;
            _context.UserCourses.Update(userCourse);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCourseCompletionStatusAsync(Guid userId, int courseId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());
            var userCourse = await _context.UserCourses
                .Include(uc => uc.Course)
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            userCourse.IsCompleted = true;
            _context.UserCourses.Update(userCourse);

            await _context.SaveChangesAsync();
            await _notificationService.SendNotificationAsync(userId, $"You completed '{userCourse.Course?.Title}' successfully!");
            return true;
        }
        public async Task<List<AssignedCourseDto>> GetAssignedCoursesAsync(Guid userId)
        {
            var userCourses = await _context.UserCourses
                .Include(uc => uc.Course)
                .ThenInclude(c => c.Category)
                .Where(uc => uc.UserId == userId && uc.Course != null)
                .ToListAsync();

            return _mapper.Map<List<AssignedCourseDto>>(userCourses);
        }
        public async Task<List<FavoriteCourseDto>> GetFavoriteCoursesAsync(Guid userId)
        {
            var favoriteCourses = await _context.UserCourses
              .Include(uc => uc.Course)
              .Where(uc => uc.UserId == userId && uc.IsFavorite && uc.Course != null)
              .ToListAsync();

            return _mapper.Map<List<FavoriteCourseDto>>(favoriteCourses);
        }
        public async Task<List<CompletedCourseDto>> GetCompletedCoursesAsync(Guid userId)
        {
            var completedCourses = await _context.UserCourses
              .Include(uc => uc.Course)
              .Where(uc => uc.UserId == userId && uc.IsCompleted && uc.Course != null)
              .ToListAsync();

            return _mapper.Map<List<CompletedCourseDto>>(completedCourses);
        }
    }
}
