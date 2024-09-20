using EducationPortal.Application.Features.Courses.Command.CreateCourses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator() {

            //RuleFor(x => x.Id)
            //.NotEmpty()
            //.WithName("Id");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithName("Category Title");

        }
    }
}
