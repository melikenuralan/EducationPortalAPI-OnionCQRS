using EducationPortal.Application.Features.Roles.Queries.GetUserRoleById;
using EducationPortal.Application.Features.UserAuth.Commands.AssignRoleToUser;
using EducationPortal.Application.Features.UserAuth.Commands.RegisterUser;
using EducationPortal.Application.Features.UserAuth.Commands.RemoveUser;
using EducationPortal.Application.Features.UserAuth.Commands.UpdateUser;
using EducationPortal.Application.Features.UserAuth.Queries.GetAllUsers;
using EducationPortal.Application.Features.UserAuth.Queries.GetUserProfileById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
        {
            RegisterUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRolesByIdAsync([FromQuery] GetUserRoleByIdQueryRequest getUserRoleByIdQueryRequest)
        {
            GetUserRoleByIdQueryResponse response = await _mediator.Send(getUserRoleByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleCommandRequest assignRoleCommandRequest)
        {
            AssignRoleCommandResponse response = await _mediator.Send(assignRoleCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdateUserCommandResponse response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUser(RemoveUserCommandRequest removeUserCommandRequest)
        {
            RemoveUserCommandReponse response = await _mediator.Send(removeUserCommandRequest);
            return Ok(response);
        }
    }
}
