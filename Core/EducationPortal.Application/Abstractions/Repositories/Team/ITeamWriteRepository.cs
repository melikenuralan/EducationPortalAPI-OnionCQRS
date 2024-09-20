using EducationPortal.Application.Abstractions.Repositories.Generic;
using EducationPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.Repositories
{
    public interface ITeamWriteRepository : IWriteRepository<Team>
    {
    }
}
