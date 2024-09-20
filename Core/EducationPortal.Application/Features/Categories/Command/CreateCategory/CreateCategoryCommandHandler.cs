using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using MediatR;

namespace EducationPortal.Application.Features.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Unit>
    {
        readonly ICategoryWriteRepository _categoryWriteRepository;
        readonly ICurrentUserService _currentUserService;

        public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository, ICurrentUserService currentUserService)
        {
            _categoryWriteRepository = categoryWriteRepository;
            _currentUserService = currentUserService;
        }
        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Title = request.Title,
                CreatedBy = _currentUserService.Username ?? "Unknown",
            };

            await _categoryWriteRepository.AddAsync(category);

            await _categoryWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
