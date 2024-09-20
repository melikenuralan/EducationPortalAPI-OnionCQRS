using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        public CategoryService(ICategoryReadRepository categoryReadRepository) => _categoryReadRepository = categoryReadRepository;
        
        public async Task<bool> IsCategoryValidAsync(int categoryId)
        {
            var category = await _categoryReadRepository.GetByIdAsync(categoryId);
            return category != null && !category.IsDeleted;
        }
    }
}
