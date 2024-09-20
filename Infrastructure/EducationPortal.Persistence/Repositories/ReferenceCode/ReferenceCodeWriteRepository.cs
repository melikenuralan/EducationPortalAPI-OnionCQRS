using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class ReferenceCodeWriteRepository : WriteRepository<ReferenceCode>, IReferenceCodeWriteRepository
    {
        public ReferenceCodeWriteRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
