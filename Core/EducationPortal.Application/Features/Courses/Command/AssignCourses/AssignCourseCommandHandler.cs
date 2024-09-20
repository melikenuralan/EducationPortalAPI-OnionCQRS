using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Application.Features.Courses.Command.AssignCourses
{
    public class AssignCourseCommandHandler : IRequestHandler<AssignCourseCommandRequest, AssignCourseCommandResponse>
    {
        readonly UserManager<User> _userManager;
        readonly ICourseService _courseService;

        public AssignCourseCommandHandler(UserManager<User> userManager, ICourseService courseService)
        {
            _userManager = userManager;
            _courseService = courseService;
        }

        public async Task<AssignCourseCommandResponse> Handle(AssignCourseCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _courseService.AssignCoursesToUserAsync(request.UserId, request.CourseIds,request.AssignedBy);

            return new AssignCourseCommandResponse
            {
                Success = result,
                Message = result ? "Courses assigned successfully" : "Failed to assign courses"
            };

        }
    }
}
