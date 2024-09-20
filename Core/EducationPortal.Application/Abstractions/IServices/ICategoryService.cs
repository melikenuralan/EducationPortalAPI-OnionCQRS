using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface ICategoryService
    {
        Task<bool> IsCategoryValidAsync(int categoryId);
    }
}
