using EducationPortal.Application.Features.Categories.Command.CreateCategory;
using EducationPortal.Application.Features.Categories.Command.DeleteCategory;
using EducationPortal.Application.Features.Categories.Command.UpdateCategory;
using EducationPortal.Application.Features.Categories.Queries.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }      
        [HttpGet]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _mediator.Send(new GetAllCategoriesQueryRequest());

            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
