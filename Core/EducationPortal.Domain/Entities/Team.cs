using EducationPortal.Domain.Entities.Common;

namespace EducationPortal.Domain.Entities
{
    public class Team : BaseEntity
    {
        public Guid ManagerId { get; set; }
        public string Department { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<ReferenceCode> ReferenceCodes { get; set; }
        public User User { get; set; }
    }
}