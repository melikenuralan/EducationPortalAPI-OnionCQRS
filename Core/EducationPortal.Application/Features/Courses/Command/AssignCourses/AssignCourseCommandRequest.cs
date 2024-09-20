using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Courses.Command.AssignCourses
{
    public class AssignCourseCommandRequest : IRequest<AssignCourseCommandResponse>
    {
        public Guid UserId { get; set; }
        public int[] CourseIds { get; set; }
        public string Title { get; set; }
        public string AssignedBy {  get; set; }
    }
}
