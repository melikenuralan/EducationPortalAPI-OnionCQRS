namespace EducationPortal.Application.Abstractions.IServices
{
    public interface ICourseService
    {
        Task<bool> AssignCoursesToUserAsync(Guid userId, int[] courseIds,string assignedBy);
        Task<bool> DeleteAsync(int courseId);
    }
}
