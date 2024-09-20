using EducationPortal.Domain.Entities.Common;

namespace EducationPortal.Domain.Entities
{

    public class Category : BaseEntity
    {

        public ICollection<Course> Courses { get; set; } 

    }
}
