using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using MediatR;

namespace EducationPortal.Application.Features.Courses.Command.DeleteCourse
{

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommandRequest, Unit>
    {
        readonly ICourseService _courseService;
        public DeleteCourseCommandHandler(ICourseService courseService) => _courseService = courseService;

        public async Task<Unit> Handle(DeleteCourseCommandRequest request, CancellationToken cancellationToken)
        {
            await _courseService.DeleteAsync(request.CourseId);
            return Unit.Value;
        }

    }
}

