using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.User;

namespace EducationPortal.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<RegisterWithReferenceCodeResponseDto> RegisterWithReferenceCode(RegisterWithReferenceCodeRequestDto request);
        Task<TokenDto> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
    }
}

