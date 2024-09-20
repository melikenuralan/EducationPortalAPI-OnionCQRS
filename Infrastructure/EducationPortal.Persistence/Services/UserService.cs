using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Application.Exceptions;
using EducationPortal.Application.Features.Roles.Queries.GetUserRoleById;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace EducationPortal.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }
        public async Task<RegisterUserResponseDto> CreateAsync(RegisterUserDto model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName

            }, model.Password);

            RegisterUserResponseDto response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)

                response.Message = "User registered Successfully !";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} {error.Description}";

            return response;
        }   
        public async Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate, int appendToAccessTokenDate)
        {

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryDate = accessTokenDate.AddMinutes(appendToAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else throw new NotFoundUserException();
        }
        public async Task<List<ListUserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users
                .Select(usr => new ListUserDto
                {
                    Id = usr.Id,
                    Email = usr.Email,
                    FullName = usr.FullName,
                    UserName = usr.UserName,
                    Badge = usr.Badge,
                    TotalScore= usr.TotalScore, 
                })
                .ToListAsync();
            return users;
        }
        public async Task<GetUserRoleByIdQueryResponse> GetUserRoleByIdsAsync(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new NotFoundUserException($"User with ID {id} not found.");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            return new GetUserRoleByIdQueryResponse
            {
                UserId = id,
                UserName = user.UserName,
                Roles = roles,
                Claims = claims
            };
        }
        public int TotalUsersCount => _userManager.Users.Count();
        public async Task AssignRoleToUserAsync(Guid userId, string[] roles)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> UpdateUserAsync(Guid id, string fullName, string userName, string email)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            user.FullName = fullName;
            user.UserName = userName;
            user.Email = email;

            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        } 
    }
}

