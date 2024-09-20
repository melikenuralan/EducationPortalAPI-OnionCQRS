using EducationPortal.Application.Abstractions.IBackgroundServices;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Features.Courses.Command.AssignCourses;
using EducationPortal.Domain.Entities;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EducationPortal.Persistence.BackgroundJobs
{
    public class CourseBackgroundJobs : ICourseBackgroundJobs
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        public CourseBackgroundJobs(IMediator mediator, INotificationService notificationService, IBackgroundJobClient backgroundJobClient, IEmailService emailService, UserManager<User> userManager)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
            _userManager = userManager;
        }
        public async Task AssignCourseAsync(AssignCourseCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success)
            {
                var message = $"Course '{request.Title}' has been assigned to you by '{request.AssignedBy}'.";
                await _notificationService.SendNotificationAsync(request.UserId, message);
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user != null)
                {
                    var emailDto = new EmailDto
                    {
                        To = user.Email,
                        Subject = "New Course Assignment",
                        Body = message
                    };
                    _emailService.SendEmail(emailDto);
                }
            }
        }
        public bool ScheduleReminders(Guid userId, string courseTitle, DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.UtcNow;

            if (startDate > now)
            {
                _backgroundJobClient.Schedule(
                    () => _notificationService.SendReminderEmail(userId, courseTitle, true, startDate),
                    startDate.AddMinutes(-1)
                );
            }

            if (endDate > now)
            {
                _backgroundJobClient.Schedule(
                    () => _notificationService.SendReminderEmail(userId, courseTitle, false, endDate),
                    endDate.AddMinutes(-1)
                );
            }
            return true;
        }
    }
}