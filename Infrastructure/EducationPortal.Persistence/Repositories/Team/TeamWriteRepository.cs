using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class TeamWriteRepository : WriteRepository<Team>, ITeamWriteRepository
    {
        public TeamWriteRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
