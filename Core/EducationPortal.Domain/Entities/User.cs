using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Domain.Entities
{

    public class User : IdentityUser<Guid>
    {
        public int? TeamId { get; set; }
        public string? Department {  get; set; }
        public string FullName { get; set; }
        public string? Badge { get; set; }
        public double TotalScore { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public Team? Team { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }

    }
}
