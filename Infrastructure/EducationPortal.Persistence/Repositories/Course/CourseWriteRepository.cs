using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class CourseWriteRepository : WriteRepository<Course>, ICourseWriteRepository
    {
        public CourseWriteRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
