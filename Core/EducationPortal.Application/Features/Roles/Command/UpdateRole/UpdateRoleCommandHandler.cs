using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationPortal.Application.Abstractions.IServices;
using MediatR;

namespace EducationPortal.Application.Features.Roles.Command.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        readonly IRoleService _roleservice;

        public UpdateRoleCommandHandler(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleservice.UpdateRole(request.Id, request.Name);

            return new()
            {
                Succeeded = result
            };
        }
    }
}
