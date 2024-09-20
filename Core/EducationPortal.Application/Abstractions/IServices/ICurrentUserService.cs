using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
        string Username { get; }
        public string? Role {  get; }
        public IEnumerable<string> Roles { get; }
    }
}
