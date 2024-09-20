using EducationPortal.Application.Abstractions;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Exceptions;
using EducationPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
          var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password,15);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token,
            };

        }
    }
}
