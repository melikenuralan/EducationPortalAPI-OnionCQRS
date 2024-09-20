using EducationPortal.Application.Dtos.Team;
using EducationPortal.Application.Dtos.User;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface ITeamService
    {
        Task<TeamResponseDto> CreateTeamAsync(CreateTeamRequestDto request);
        Task<string> GenerateReferenceCodeAsync(int teamId);
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamByIdDto> GetTeamByIdAsync(int teamId);
        Task<List<ListUserDto>> GetUsersByTeamIdAsync();
    }
}
