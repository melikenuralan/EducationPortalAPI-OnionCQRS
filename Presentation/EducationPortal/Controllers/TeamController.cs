using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class TeamController : ControllerBase
    {
        readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequestDto request)
        {
            var result = await _teamService.CreateTeamAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> GetTeamsById(int teamId)
        {
            var teams = await _teamService.GetTeamByIdAsync(teamId);
            return Ok(teams);
        }

        [HttpPost]
        [Authorize(Roles = "TeamManager")]
        public async Task<IActionResult> GenerateReferenceCode(int teamId)
        {
            var referenceCode = await _teamService.GenerateReferenceCodeAsync(teamId);
            return Ok(new { ReferenceCode = referenceCode });
        }

        [Authorize(Roles = "TeamManager")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersInTeam()
        {
            var users = await _teamService.GetUsersByTeamIdAsync();
            return Ok(users);
        }
    }
}
