using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Dtos.Team
{
    public class TeamResponseDto
    {
        public string Title { get; set; }
        public string Department { get; set; }
        public Guid ManagerId { get; set; }
    }
}
