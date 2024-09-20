using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Courses.Command.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommandRequest>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0);
        }
    }


}
