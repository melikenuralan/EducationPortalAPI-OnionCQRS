using EducationPortal.Domain.Entities.Common;

namespace EducationPortal.Domain.Entities
{
    public class ReferenceCode : BaseEntity
    {
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
