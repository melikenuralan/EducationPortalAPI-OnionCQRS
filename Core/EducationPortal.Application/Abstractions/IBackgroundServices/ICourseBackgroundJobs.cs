using EducationPortal.Application.Features.Courses.Command.AssignCourses;

namespace EducationPortal.Application.Abstractions.IBackgroundServices
{
    public interface ICourseBackgroundJobs
    {
        Task AssignCourseAsync(AssignCourseCommandRequest request);
        bool ScheduleReminders(Guid userId, string courseTitle, DateTime startDate, DateTime endDate);
    }
}
