using EducationPortal.Application.Dtos;
using EducationPortal.Application.Features.Courses.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Courses.Queries
{
    public class GetAllCoursesQueryResponse : IRequest<IList<GetAllCoursesQueryResponse>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; } 


    }
}
