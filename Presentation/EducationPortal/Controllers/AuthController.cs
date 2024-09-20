using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Application.Features.UserAuth.Commands.LoginUser;
using EducationPortal.Application.Features.UserAuth.Commands.RefreshTokenLogin;
using EducationPortal.Application.Features.UserAuth.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IFileService _fileService;
        readonly IAuthService _authService;

        public AuthController(IMediator mediator, IFileService fileService, IAuthService authService)
        {
            _mediator = mediator;
            _fileService = fileService;
            _authService = authService;
        }

        [HttpPost]   
        public async Task<IActionResult> RegisterWithReferenceCode([FromBody] RegisterWithReferenceCodeRequestDto request)
        {
            RegisterWithReferenceCodeResponseDto response = await _authService.RegisterWithReferenceCode(request);
            return Ok(response);
        }
       
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> RefreshTokenLogin([FromForm] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }
    }
}
