using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using MediatR;

namespace EducationPortal.Application.Features.Courses.Command.CreateCourses
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommandRequest, Unit>
    {
        readonly ICourseWriteRepository _courseWriteRepository;
        readonly ICurrentUserService _currentUserService;

        public CreateCourseCommandHandler(ICourseWriteRepository courseWriteRepository, ICurrentUserService currentUserService)
        {
            _courseWriteRepository = courseWriteRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var course = new Course
            {
                Title = request.Title,
                Description = request.Description,
                CourseLevel = request.CourseLevel,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CategoryId = request.CategoryId,
                CreatedBy = _currentUserService.Username ?? "Unknown",
            };

            await _courseWriteRepository.AddAsync(course);

            await _courseWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
