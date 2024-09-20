using EducationPortal.Application.Dtos.User;
using EducationPortal.Application.Features.Roles.Queries.GetUserRoleById;
using EducationPortal.Domain.Entities;

namespace EducationPortal.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<RegisterUserResponseDto> CreateAsync(RegisterUserDto model);
        Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate, int appendToAccessTokenDate);
        Task<List<ListUserDto>> GetAllUsersAsync();
        Task<GetUserRoleByIdQueryResponse> GetUserRoleByIdsAsync(Guid id);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsync(Guid UserId, string[] roles);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateUserAsync(Guid id, string fullName, string userName, string email);
    }
}
