using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Domain.Entities
{
    public class UserCourse
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAssigned {  get; set; }
        public string? AssignedBy { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
