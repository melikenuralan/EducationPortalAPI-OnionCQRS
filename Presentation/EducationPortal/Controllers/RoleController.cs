using EducationPortal.Application.Features.Roles.Command.CreateRole;
using EducationPortal.Application.Features.Roles.Command.DeleteRole;
using EducationPortal.Application.Features.Roles.Command.UpdateRole;
using EducationPortal.Application.Features.Roles.Queries.GetAllRoles;
using EducationPortal.Application.Features.Roles.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetRolesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoleById([FromRoute] GetRoleByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
