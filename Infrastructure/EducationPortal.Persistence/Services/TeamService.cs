using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.Team;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Persistence.Services
{
    public class TeamService : ITeamService
    {
        readonly ITeamWriteRepository _teamWriteRepository;
        readonly IReferenceCodeWriteRepository _referenceCodeWriteRepository;
        readonly ITeamReadRepository _teamReadRepository;
        readonly ICurrentUserService _currentUserService;
        readonly UserManager<User> _userManager;
        readonly EduPortalDbContext _context;
        readonly IEmailService _emailService;

        public TeamService(ITeamWriteRepository teamWriteRepository, IReferenceCodeWriteRepository referenceCodeWriteRepository, ITeamReadRepository teamReadRepository, ICurrentUserService currentUserService, UserManager<User> userManager, EduPortalDbContext context, IEmailService emailService)
        {
            _teamWriteRepository = teamWriteRepository;
            _referenceCodeWriteRepository = referenceCodeWriteRepository;
            _teamReadRepository = teamReadRepository;
            _currentUserService = currentUserService;
            _userManager = userManager;
            _context = context;
            _emailService = emailService;
        }

        public async Task<TeamResponseDto> CreateTeamAsync(CreateTeamRequestDto request)
        {
            var manager = await _userManager.FindByIdAsync(request.ManagerId.ToString());

            var team = new Team
            {
                Title = request.Title,
                Department = request.Department,
                ManagerId = request.ManagerId
            };

            await _teamWriteRepository.AddAsync(team);
            await _teamWriteRepository.SaveAsync();

            manager.TeamId = team.Id;
            await _userManager.AddToRoleAsync(manager, "TeamManager");
            await _userManager.UpdateAsync(manager);

            return new TeamResponseDto
            {
                Title = team.Title,
                Department = team.Department,
                ManagerId = team.ManagerId
            };
        }
        public async Task<string> GenerateReferenceCodeAsync(int teamId)
        {
            var code = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            var referenceCode = new ReferenceCode
            {
                Code = code,
                ExpirationDate = DateTime.UtcNow.AddMinutes(7),
                IsUsed = false,
                TeamId = teamId
            };

            await _referenceCodeWriteRepository.AddAsync(referenceCode);
            await _referenceCodeWriteRepository.SaveAsync();
            var emailDto = new EmailDto
            {
                To = "anne24@ethereal.email",
                Subject = "New Reference Code",
                Body = $"Hello,<br><br>Your New Reference Code : <strong>{code}</strong><br><br>This code will expire in 7 minutes."
            };

             _emailService.SendEmail(emailDto);

            return code;
        }
        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamReadRepository.GetAllAsync();
            return teams.Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Title,
                Department = t.Department,
            });
        }
        public async Task<TeamByIdDto> GetTeamByIdAsync(int teamId)
        {
            var team = await _teamReadRepository.GetByIdAsync(teamId);

            var manager = await _userManager.Users
                .Where(u => u.Id == team.ManagerId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            return new TeamByIdDto
            {
                Id = team.Id,
                Name = team.Title,
                Department = team.Department,
                ManagerName = manager ?? "Forbidden"
            };
        }
        public async Task<List<ListUserDto>> GetUsersByTeamIdAsync()
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString());

            var usersInTeam = await _context.Users
                .Where(u => u.TeamId == user.TeamId.Value)
                .ToListAsync();

            var listUsers = usersInTeam.Select(u => new ListUserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                UserName=u.UserName,
                Badge = u.Badge,
                TotalScore = u.TotalScore
            }).ToList();

            return listUsers;
        }
    }
}
