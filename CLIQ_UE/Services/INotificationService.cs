using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface INotificationService
    {
        void AddNotification(string userId, string createdByUser, string content);
        List<Notification> GetNewNotifications(string userId);
        List<Notification> GetAllNotifications(string userId);
        void MarkNotificationsSeen(string userId);
    }
}
