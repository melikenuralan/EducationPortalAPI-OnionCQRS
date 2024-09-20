using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? claimsPrincipal)
        {
            string? userId = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid parsedUserId)? parsedUserId: Guid.Empty;
        }
        public static string GetUsername(this ClaimsPrincipal? claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.Name) ?? "Unknown";
        }
        public static string GetRole(this ClaimsPrincipal? claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
        }
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal? claimsPrincipal)
        {
            return claimsPrincipal?.FindAll(ClaimTypes.Role)?.Select(claim => claim.Value) ?? Enumerable.Empty<string>();
        }
    }
}
