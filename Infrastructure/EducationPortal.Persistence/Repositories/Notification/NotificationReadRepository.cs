using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistance.Repositories;

namespace EducationPortal.Persistence.Repositories.Notifications
{
    public class NotificationReadRepository : ReadRepository<Notification>, INotificationReadRepository
    {
        public NotificationReadRepository(EduPortalDbContext context) : base(context)
        {
        }
    }
}
