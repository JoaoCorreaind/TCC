using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
namespace WebApplication1.Interfaces
{
    public interface INotificationRepository
    {
        Task <Notification> CreateNotification(NotificationDto notification);
        Task <Notification> GetNotificationById(int notificationId);
        Task<List<Notification>> GetAll();
        Task<List<Notification>> GetUserNotifications(string userId);
        Task <bool> UnresolvedNotification(NotificationDto notification);

        Task<Notification> UpdateNotification(NotificationDto notification);
        Task<bool>  ResolveInformationNotifications(string userId);
    }
}
