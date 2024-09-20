using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories
{
    public class TeamReadRepository : ReadRepository<Team>, ITeamReadRepository
    {
        public TeamReadRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
