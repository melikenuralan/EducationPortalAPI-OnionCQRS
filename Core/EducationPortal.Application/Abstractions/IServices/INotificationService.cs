using EducationPortal.Application.Dtos.User;
using EducationPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string message);
        Task<IEnumerable<NotifyDto>> GetNotificationsByUserIdAsync();
        Task SendReminderEmail(Guid userId, string courseTitle, bool isStartReminder, DateTime date);
    }
}