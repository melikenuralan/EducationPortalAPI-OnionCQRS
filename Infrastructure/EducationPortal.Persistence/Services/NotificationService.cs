using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Persistence.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationWriteRepository _notifyWriteRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly EduPortalDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public NotificationService(INotificationWriteRepository notifyWriteRepository, ICurrentUserService currentUserService, EduPortalDbContext context, UserManager<User> userManager, IEmailService emailService)
        {
            _notifyWriteRepository = notifyWriteRepository;
            _currentUserService = currentUserService;
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task SendNotificationAsync(Guid userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                CreatedDate = DateTime.UtcNow,
                IsRead = false,
            };

            await _notifyWriteRepository.AddAsync(notification);
            await _notifyWriteRepository.SaveAsync();
        }
        public async Task<IEnumerable<NotifyDto>> GetNotificationsByUserIdAsync()
        {
            var userId = _currentUserService.UserId;
            var notifications = await _context.Notifications
               .Where(n => n.UserId == userId)
               .OrderByDescending(n => n.CreatedDate)
               .Select(n => new NotifyDto
               {
                   Message = n.Message,
                   IsRead = n.IsRead
               })
               .ToListAsync();

            return notifications;
        }
        public async Task SendReminderEmail(Guid userId, string courseTitle, bool isStartReminder, DateTime date)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                string subject;
                string body;

                if (isStartReminder)
                {
                    subject = "Course Start Reminder";
                    body = $"Reminder: Your course '{courseTitle}' starts tomorrow, {date:d}!";
                }
                else
                {
                    subject = "Course End Reminder";
                    body = $"Reminder: Your course '{courseTitle}' ends tomorrow, {date:d}!";
                }

                var emailDto = new EmailDto
                {
                    To = user.Email,
                    Subject = subject,
                    Body = body
                };

                _emailService.SendEmail(emailDto);

                await SendNotificationAsync(userId, body);

            }
        }
    }
}