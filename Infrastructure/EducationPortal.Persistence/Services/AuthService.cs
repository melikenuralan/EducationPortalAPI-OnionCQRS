using EducationPortal.Application.Abstractions;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Application.Exceptions;
using EducationPortal.Application.Features.UserAuth.Commands.LoginUser;
using EducationPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<User> _userManager;
        readonly ITokenProvider _tokenProvider;
        readonly SignInManager<User> _signInManager;
        readonly IUserService _userService;
        readonly IReferenceCodeReadRepository _referenceCodeReadRepository;
        readonly ITeamReadRepository _teamReadRepository;
        readonly IReferenceCodeWriteRepository _referenceCodeWriteRepository;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, ITokenProvider tokenProvider, IReferenceCodeReadRepository referenceCodeReadRepository, ITeamReadRepository teamReadRepository, IReferenceCodeWriteRepository referenceCodeWriteRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _tokenProvider = tokenProvider;
            _referenceCodeReadRepository = referenceCodeReadRepository;
            _teamReadRepository = teamReadRepository;
            _referenceCodeWriteRepository = referenceCodeWriteRepository;
        }
        public async Task<TokenDto> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            User? user = await _userManager.FindByNameAsync(usernameOrEmail);

            if (user == null) user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null) throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {

                TokenDto token = _tokenProvider.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 20);

                return token;

            }
            else

                throw new AuthenticationErrorException();
        }
        public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenExpiryDate > DateTime.Now)
            {
                TokenDto token = _tokenProvider.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 20);
                return token;
            }
            else throw new AuthenticationErrorException();
        }
        public async Task<RegisterWithReferenceCodeResponseDto> RegisterWithReferenceCode(RegisterWithReferenceCodeRequestDto request)
        {
            var referenceCode = await _referenceCodeReadRepository.GetSingleAsync(r => r.Code == request.ReferenceCode);

            if (referenceCode == null || referenceCode.IsUsed || referenceCode.ExpirationDate < DateTime.UtcNow)
                    throw new InvalidOperationException("Invalid or expired reference code.");
            

            var team = await _teamReadRepository.GetByIdAsync(referenceCode.TeamId);
            if (team == null) throw new InvalidOperationException("Team not found.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                TeamId = team.Id,
                Department = team.Department
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            referenceCode.IsUsed = true;
            await _userManager.AddToRoleAsync(user, "Intern");
            _referenceCodeWriteRepository.Update(referenceCode);
            await _referenceCodeWriteRepository.SaveAsync();

            RegisterWithReferenceCodeResponseDto response = new() { Success = result.Succeeded };
            if (result.Succeeded)

                response.Message = "User registered Successfully !";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} {error.Description}";

            return response;
        }
    }
}
