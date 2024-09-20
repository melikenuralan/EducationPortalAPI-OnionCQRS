using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EducationPortal.Infrastructure.Services
{
    internal class TokenProvider : ITokenProvider
    {
        readonly IConfiguration _configuration;
        readonly UserManager<User> _userManager;

        public TokenProvider(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public TokenDto CreateAccessToken(int minute, User user)
        {
            TokenDto token = new();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Token:SecurityKey"]);
            var roles = _userManager.GetRolesAsync(user).Result;
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),

            }.Concat(roleClaims)),

                Expires = DateTime.Now.AddDays(minute),
                Issuer = _configuration["Token:Audience"],
                Audience = _configuration["Token:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHand = tokenHandler.CreateToken(tokenDescriptor);
            var writedToken = tokenHandler.WriteToken(tokenHand);
            token.AccessToken = writedToken;
            token.Expiration = DateTime.Now.AddDays(minute);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
