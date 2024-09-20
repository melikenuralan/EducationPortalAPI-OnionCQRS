using AutoMapper;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace EducationPortal.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, IList<CategoryDto>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public GetAllCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _categoryReadRepository = categoryReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IList<CategoryDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryReadRepository.GetAll(false)
               .Where(c => _currentUserService.Roles.Contains("Admin") || !c.IsDeleted) 
               .OrderBy(c => c.Id) 
               .ToListAsync(default);

            var categoryDtos = _mapper.Map<IList<CategoryDto>>(categories);
            return categoryDtos;
        }
    }
}
