using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Queries.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}
