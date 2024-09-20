using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Features.Roles.Command.UpdateRole;
using MediatR;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
                var result = await _userService.UpdateUserAsync(request.Id, request.FullName,request.Username,request.Email);

                return new()
                {
                    Succeeded = result
                };
            }
        }
    }

