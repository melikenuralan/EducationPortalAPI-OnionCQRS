using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
