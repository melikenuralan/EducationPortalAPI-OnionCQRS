using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.Categories.Command.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<UpdateCategoryCommandReponse>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
