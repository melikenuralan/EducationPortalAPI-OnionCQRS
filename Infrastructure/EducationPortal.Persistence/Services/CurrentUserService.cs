using EducationPortal.Application;
using EducationPortal.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Persistence.Services
{
    public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public bool IsAuthenticated =>
            httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ?? false;
        public Guid UserId =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ?? throw new ApplicationException();
        public string Username =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetUsername() ?? throw new ApplicationException();

        public string Role =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetRole() ?? throw new ApplicationException();

        public IEnumerable<string> Roles =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetRoles() ?? throw new ApplicationException();
    }
}
