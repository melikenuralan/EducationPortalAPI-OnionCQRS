using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using MediatR;

namespace EducationPortal.Application.Features.Categories.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandReponse>
    {
        readonly ICategoryWriteRepository _categoryWriteRepository;
        readonly ICategoryReadRepository _categoryReadRepository;
        readonly ICurrentUserService _currentUserService;

        public UpdateCategoryCommandHandler(ICategoryWriteRepository categoryWriterepository, ICategoryReadRepository categoryReadRepository, ICurrentUserService currentUserService)
        {
            _categoryWriteRepository = categoryWriterepository;
            _categoryReadRepository = categoryReadRepository;
            _currentUserService = currentUserService;
        }

        public async Task<UpdateCategoryCommandReponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {

            Category category = await _categoryReadRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return new UpdateCategoryCommandReponse
                {
                    Succeeded = false,
                    Message = "Category not found"
                };
            }


            category.Id = request.Id;
            category.Title = request.Title;
            category.UpdatedBy = _currentUserService.Username;

            var result = _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveAsync();

            return new UpdateCategoryCommandReponse()
            {
                Succeeded = result,
                Message = "Updated Successfully"
            };
        }
    }
}
