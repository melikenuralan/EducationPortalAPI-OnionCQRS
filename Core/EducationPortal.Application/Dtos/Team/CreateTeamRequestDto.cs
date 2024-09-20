namespace EducationPortal.Application.Dtos.Team
{
    public class CreateTeamRequestDto
    {
        public string Title { get; set; }
        public string Department { get; set; }
        public Guid ManagerId { get; set; }
    }
}
