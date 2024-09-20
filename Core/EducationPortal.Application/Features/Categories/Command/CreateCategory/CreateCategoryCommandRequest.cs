using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<Unit>
    {
        public string Title { get; set; }
    }
}
