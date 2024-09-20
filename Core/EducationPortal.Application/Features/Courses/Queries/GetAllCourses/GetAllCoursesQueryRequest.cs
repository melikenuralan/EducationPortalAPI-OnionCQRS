using EducationPortal.Application.Dtos.UserCourse;
using MediatR;

namespace EducationPortal.Application.Features.Courses.Queries
{
    public class GetAllCoursesQueryRequest : IRequest<IList<CourseDto>>
    {
        public int CourseId { get; set; }
    }
}
