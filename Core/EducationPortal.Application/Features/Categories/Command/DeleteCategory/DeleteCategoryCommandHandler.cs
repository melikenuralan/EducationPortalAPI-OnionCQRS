using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using MediatR;

namespace EducationPortal.Application.Features.Categories.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, Unit>
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCategoryCommandHandler(
            ICategoryReadRepository categoryReadRepository,
            ICategoryWriteRepository categoryWriteRepository,
            ICurrentUserService currentUserService)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _currentUserService = currentUserService;
        }
        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepository.GetSingleAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                tracking: true);

            if (category == null)
            {
                return Unit.Value;
            }

            category.IsDeleted = true;
            category.DeletedBy = _currentUserService.Username ?? "Unknown";

            _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
