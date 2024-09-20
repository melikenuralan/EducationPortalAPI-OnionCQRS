using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories.Notifications
{
    public class NotificationWriteRepository : WriteRepository<Notification>, INotificationWriteRepository
    {
        public NotificationWriteRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
