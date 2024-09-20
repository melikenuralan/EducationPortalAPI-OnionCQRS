using EducationPortal.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.AssignRoleToUser
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommandRequest, AssignRoleCommandResponse>
    {
        readonly IUserService _userService;

        public AssignRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AssignRoleCommandResponse> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleToUserAsync(request.UserId, request.Roles);

            return new();
        }
    }
}
