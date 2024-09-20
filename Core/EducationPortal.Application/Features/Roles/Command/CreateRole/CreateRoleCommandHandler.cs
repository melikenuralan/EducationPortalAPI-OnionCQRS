using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationPortal.Application.Abstractions.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Application.Features.Roles.Command.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, CreateRoleCommandReponse>
    {
        readonly IRoleService _roleService;
        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<CreateRoleCommandReponse> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
          var result = await _roleService.CreateRole(request.Name);

            return new()
            {
                Succeeded = result,
            };
        }
    }
}
