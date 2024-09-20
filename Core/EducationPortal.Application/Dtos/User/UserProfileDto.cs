using EducationPortal.Application.Dtos.Profile;

namespace EducationPortal.Application.Dtos.User
{
    public class UserProfileDto
    {
        public string TeamName { get; set; }
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Badge {  get; set; }
        public List<AssignedCourseDto> AssignedCourses { get; set; }
        public List<FavoriteCourseDto> FavCourses { get; set; }
        public List<CompletedCourseDto> CompletedCourses { get; set; }
        public double TotalScore { get; set; }
    }
}
