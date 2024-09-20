using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface IUserCourseService
    {
        Task<bool> MarkCourseAsFavoriteAsync(int courseId);
        Task<bool> RemoveFavoriteCourseAsync(int courseId);
        Task<bool> UpdateCourseCompletionStatusAsync(Guid userId, int courseId);
        Task<List<AssignedCourseDto>> GetAssignedCoursesAsync(Guid userId);
        Task<List<FavoriteCourseDto>> GetFavoriteCoursesAsync(Guid userId);
        Task<List<CompletedCourseDto>> GetCompletedCoursesAsync(Guid userId);
    }
}
