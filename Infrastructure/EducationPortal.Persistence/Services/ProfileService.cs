using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Persistence.Services
{
    public class ProfileService : IProfileService
    {
        readonly UserManager<User> _userManager;
        private readonly EduPortalDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        readonly IUserCourseService _userCourseService;
        readonly ITeamReadRepository _teamReadRepository;
        public ProfileService(UserManager<User> userManager, EduPortalDbContext context, ICurrentUserService currentUserService, IUserCourseService userCourseService, ITeamReadRepository teamReadRepository)
        {
            _userManager = userManager;
            _context = context;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
            _teamReadRepository = teamReadRepository;
        }
        public async Task<double> CalculateUserPerformans(Guid userId)
        {
            var totalCourses = await _context.UserCourses.CountAsync(uc => uc.UserId == userId && uc.IsAssigned);
            var completedCourses = await _context.UserCourses.CountAsync(uc => uc.UserId == userId && uc.IsCompleted);

            if (totalCourses == 0) return 0;

            double totalScore = (completedCourses / (double)totalCourses) * 100;
            Console.WriteLine($"Completed Courses: {completedCourses}, Total Courses: {totalCourses}, Total Score: {totalScore}");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                user.Badge = totalScore >= 90 ? "Master Badge" :
                totalScore >= 50 ? "Persistent Achiever" :
                totalScore >= 10 ? "FirstStepSuccessBadge" :
                string.Empty;
                user.TotalScore = totalScore;
                await _userManager.UpdateAsync(user);
            }
            return totalScore;
        }
        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userId = _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var team = await _context.Teams.FindAsync(user.TeamId);
            var teamName = team?.Title;
            if (!string.IsNullOrEmpty(teamName) && userId != Guid.Empty)
            {
                await CalculateUserPerformans(userId);
                var _assignedCourses = await _userCourseService.GetAssignedCoursesAsync(userId);
                var _favoriteCourses = await _userCourseService.GetFavoriteCoursesAsync(userId);
                var _completedCourses = await _userCourseService.GetCompletedCoursesAsync(userId);

                return new UserProfileDto
                {
                    TeamName = teamName,
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.FullName,
                    Username = user.UserName,
                    Badge = user.Badge,
                    TotalScore = user.TotalScore,
                    AssignedCourses = _assignedCourses,
                    FavCourses = _favoriteCourses,
                    CompletedCourses = _completedCourses,
                };
            }
            else
                throw new Exception("An error occured");
        }
    }
}
