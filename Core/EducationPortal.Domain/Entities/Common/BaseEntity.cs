namespace EducationPortal.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; } 
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string DeletedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
