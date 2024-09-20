using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Courses.Command.UpdateCourse
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateCourseCommandRequest>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Title");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Description");

        }
    }

}
