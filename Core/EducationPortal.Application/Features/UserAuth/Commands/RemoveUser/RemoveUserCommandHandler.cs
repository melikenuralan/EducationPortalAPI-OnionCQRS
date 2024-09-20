using EducationPortal.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.RemoveUser
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommandRequest, RemoveUserCommandReponse>
    {
        readonly IUserService _userService;

        public RemoveUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RemoveUserCommandReponse> Handle(RemoveUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteUserAsync(request.id);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
