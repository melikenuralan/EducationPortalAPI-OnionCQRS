using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
