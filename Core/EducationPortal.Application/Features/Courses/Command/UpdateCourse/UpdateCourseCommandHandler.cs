using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using MediatR;

namespace EducationPortal.Application.Features.Courses.Command.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommandRequest, Unit>
    {
        private readonly ICourseReadRepository _courseReadRepository;
        private readonly ICourseWriteRepository _courseWriteRepository;   
        private readonly ICurrentUserService _currentUserService;



        public UpdateCourseCommandHandler(ICourseReadRepository courseReadRepository, ICourseWriteRepository courseWriteRepository, ICurrentUserService currentUserService)
        {
            _courseReadRepository = courseReadRepository;
            _courseWriteRepository = courseWriteRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateCourseCommandRequest request, CancellationToken cancellationToken)
        {
            Course course = await _courseReadRepository.GetByIdAsync(request.Id,true);

            course.Title = request.Title;
            course.Description = request.Description;
            course.StartDate = request.StartDate;
            course.EndDate = request.EndDate;
            course.CategoryId=request.CategoryId;
            course.UpdatedBy = _currentUserService.Username ?? "Unknown";


            _courseWriteRepository.Update(course); // Ensure this method marks the entity as modified //test
            await _courseWriteRepository.SaveAsync();


            return Unit.Value;
        }
    }
}
