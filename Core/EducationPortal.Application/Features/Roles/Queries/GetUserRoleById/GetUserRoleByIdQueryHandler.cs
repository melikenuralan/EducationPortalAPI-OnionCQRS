using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Roles.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQueryRequest, GetUserRoleByIdQueryResponse>
    {
        readonly IUserService _userService;

        public GetUserRoleByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserRoleByIdQueryResponse> Handle(GetUserRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetUserRoleByIdsAsync(request.UserId);
            return userRoles;
        }
    }
}
