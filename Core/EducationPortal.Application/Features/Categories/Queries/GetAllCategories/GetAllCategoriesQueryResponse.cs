using EducationPortal.Application.Features.Courses.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryResponse : IRequest<IList<GetAllCategoriesQueryResponse>>
    {
        public int Id { get; set; }
        public string Title { get; set; } 
    }
}
