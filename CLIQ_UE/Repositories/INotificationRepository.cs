using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface INotificationRepository
    {
        void AddNotification(string userId, string createdByUserId, string content);
        List<Notification> GetNewNotifications(string userId);
        List<Notification> GetAllNotifications(string userId);
        void MarkNotificationsSeen(string userId);
    }
}
