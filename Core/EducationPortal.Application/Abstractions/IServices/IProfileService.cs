using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface IProfileService
    {
        Task<UserProfileDto> GetUserProfileAsync();
        Task<double> CalculateUserPerformans(Guid userId);
    }
}
