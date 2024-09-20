using EducationPortal.Application.Abstractions.IBackgroundServices;
using EducationPortal.Application.Features.Courses.Command.AssignCourses;
using EducationPortal.Application.Features.Courses.Command.CreateCourses;
using EducationPortal.Application.Features.Courses.Command.DeleteCourse;
using EducationPortal.Application.Features.Courses.Command.UpdateCourse;
using EducationPortal.Application.Features.Courses.Queries;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class CourseController : ControllerBase
    {
        readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> GetAllCourses()
        {
            var response = await _mediator.Send(new GetAllCoursesQueryRequest());

            return Ok(response);       
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse(CreateCourseCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(DeleteCourseCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,TeamManager")]
        public async Task<IActionResult> AssignCourse([FromForm]AssignCourseCommandRequest request)
        {
            var jobId = BackgroundJob.Enqueue<ICourseBackgroundJobs>(jobs => jobs.AssignCourseAsync(request));
            return Accepted(new { JobId = jobId });
        }
    }
}
