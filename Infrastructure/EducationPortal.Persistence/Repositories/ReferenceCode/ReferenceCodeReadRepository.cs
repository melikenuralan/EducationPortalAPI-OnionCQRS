using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class ReferenceCodeReadRepository : ReadRepository<ReferenceCode>, IReferenceCodeReadRepository
    {
        public ReferenceCodeReadRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
