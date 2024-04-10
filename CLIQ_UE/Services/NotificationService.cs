using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }


        public void AddNotification(string userId, string createdByUser, string content)
        {
            notificationRepository.AddNotification(userId, createdByUser, content);
        }

        public List<Notification> GetAllNotifications(string userId)
        {
            return notificationRepository.GetAllNotifications(userId);
        }

        public List<Notification> GetNewNotifications(string userId)
        {
            return notificationRepository.GetNewNotifications(userId);
        }

        public void MarkNotificationsSeen(string userId)
        {
            notificationRepository.MarkNotificationsSeen(userId);
        }
    }
}
