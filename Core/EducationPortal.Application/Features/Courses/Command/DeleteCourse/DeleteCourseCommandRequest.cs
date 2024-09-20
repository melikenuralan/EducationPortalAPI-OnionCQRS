using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Courses.Command.DeleteCourse
{
    public class DeleteCourseCommandRequest : IRequest<Unit>
    {
        public int CourseId { get; set; }
    }
}
