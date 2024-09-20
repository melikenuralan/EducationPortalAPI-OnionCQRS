using EducationPortal.Application.Dtos;
using EducationPortal.Domain.Entities;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface ITokenProvider
    {
        TokenDto CreateAccessToken(int minute, User user);
        string CreateRefreshToken();
    }
}
