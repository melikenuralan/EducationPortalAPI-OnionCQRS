using EducationPortal.Domain.Entities.Enums;
using MediatR;

namespace EducationPortal.Application.Features.Courses.Command.CreateCourses
{
    public class CreateCourseCommandRequest: IRequest<Unit>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CourseLevel CourseLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; } 

       // public string CategoryTitle { get; set; } = string.Empty;
    }
}
