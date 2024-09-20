using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Dtos.Profile
{
    public class UpdateCourseCompletionStatusRequestDto
    {
        public Guid UserId { get; set; }
        public int CourseId { get; set; }
    }
}
