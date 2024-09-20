using EducationPortal.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Queries.GetUserProfileById
{
    public class GetUserProfileByIdQueryResponse
    {
        public UserProfileDto UserProfile { get; set; }
    }
}
