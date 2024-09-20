using EducationPortal.Application.Abstractions.IServices;
using FluentValidation;

namespace EducationPortal.Application.Features.Courses.Command.CreateCourses
{

    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommandRequest>
    {
        public CreateCourseCommandValidator(ICategoryService categoryService)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(25)
                .WithName("Title");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(100)
                .WithName("Description");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
            .MustAsync(async (categoryId, cancellation) =>
            {
                return await categoryService.IsCategoryValidAsync(categoryId);
            })
            .WithMessage("The specified category does not exist or has been deleted.");
        }
    }
}
    

