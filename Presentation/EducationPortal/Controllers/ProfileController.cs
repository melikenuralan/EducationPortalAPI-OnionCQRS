using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos.Profile;
using EducationPortal.Application.Features.UserAuth.Queries.GetUserProfileById;
using EducationPortal.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProfileController : ControllerBase
    {

        readonly IMediator _mediator;
        readonly IProfileService _profileService;
        readonly INotificationService _notificationService;
        readonly IUserCourseService _userCourseService;
        public ProfileController(IMediator mediator, IProfileService profileService, INotificationService notificationService, IUserCourseService userCourseService)
        {
            _mediator = mediator;
            _profileService = profileService;
            _notificationService = notificationService;
            _userCourseService = userCourseService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,TeamManager,Intern")]
        public async Task<IActionResult> GetUserProfile()
        {
            var response = await _mediator.Send(new GetUserProfileByIdQueryRequest());

            return Ok(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,TeamManager,Intern")]
        public async Task<IActionResult> GetAnnouncements()
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync();
            return Ok(notifications);
        }
        [HttpPut]
        [Authorize(Roles = "Intern")]
        public async Task<IActionResult> MarkCourseAsFavorite([FromBody] MarkCourseAsFavoriteRequestDto request)
        {
            var result = await _userCourseService.MarkCourseAsFavoriteAsync(request.CourseId);
            return result ? Ok("Course marked as favourite successfully.") : NotFound("User or course not found.");
        }

        [HttpDelete]
        [Authorize(Roles = "Intern")]
        public async Task<IActionResult> RemoveFavouriteCourse([FromBody] RemoveFavouriteCourseRequestDto request)
        {
            var result = await _userCourseService.RemoveFavoriteCourseAsync(request.CourseId);
            return result ? Ok("Course removed from favorites successfully.") : NotFound("User or course not found.");
        }

        [HttpPut]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> UpdateCourseCompletionStatus([FromBody] UpdateCourseCompletionStatusRequestDto request)
        {
            var result = await _userCourseService.UpdateCourseCompletionStatusAsync(request.UserId, request.CourseId);
            return result ? Ok("Course status changed successfully.") : NotFound("User or course not found.");
        }
    }
}
