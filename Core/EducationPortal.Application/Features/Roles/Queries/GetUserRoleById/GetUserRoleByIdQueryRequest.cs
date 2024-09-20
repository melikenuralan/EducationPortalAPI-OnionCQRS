using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Roles.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryRequest : IRequest<GetUserRoleByIdQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}
