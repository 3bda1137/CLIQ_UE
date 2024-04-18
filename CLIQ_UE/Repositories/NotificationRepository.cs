using CLIQ_UE.Helpers;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{

    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext context;
        private readonly IUserServices userServices;

        public NotificationRepository(ApplicationContext context, IUserServices userServices)
        {
            this.context = context;
            this.userServices = userServices;
        }

        public void AddNotification(string userId, string createdByUserId, string content)
        {

            ApplicationUser user = userServices.GetByID(createdByUserId);

            if (createdByUserId == userId)
            {
                return;
            }
            if (user != null && content != null)
            {
                Notification notification = new Notification();
                notification.Content = content;
                notification.UserId = userId;
                notification.CreatedByUserId = createdByUserId;
                notification.notificationDate = DateTime.Now;
                notification.notificationShowDate = "just now";
                notification.UserImage = user.PersonalImage;
                notification.UserName = user.FirstName + " " + user.LastName;
                notification.IsSeen = false;

                context.Notifications.Add(notification);
                context.SaveChanges();
            }
        }

        public List<Notification> GetNewNotifications(string userId)
        {



            List<Notification> notifications = context.Notifications
                .Where(n => n.UserId == userId && !n.IsSeen)
                .Select(n => new Notification
                {
                    UserId = n.UserId,
                    CreatedByUserId = n.CreatedByUserId,
                    UserName = n.UserName,
                    UserImage = n.UserImage,
                    Content = n.Content,
                    IsSeen = true,
                    notificationDate = n.notificationDate,
                    notificationShowDate = FormatTime.FormatingTime(n.notificationDate)
                })
                .ToList();
            return notifications;
        }

        public List<Notification> GetAllNotifications(string userId)
        {



            List<Notification> notifications = context.Notifications
             .Where(n => n.UserId == userId)
             .Select(n => new Notification
             {
                 UserId = n.UserId,
                 CreatedByUserId = n.CreatedByUserId,
                 UserName = n.UserName,
                 UserImage = n.UserImage,
                 Content = n.Content,
                 IsSeen = n.IsSeen,
                 notificationDate = n.notificationDate,
                 notificationShowDate = FormatTime.FormatingTime(n.notificationDate)
             })
             .ToList();

            context.Database.ExecuteSqlInterpolated($@"
                                                                                UPDATE Notifications
                                                                                    SET IsSeen = 1
                                                                                WHERE UserId = {userId}
                                                                                AND IsSeen = 0");

            return notifications;
        }

        // Update seen  in the database
        public void MarkNotificationsSeen(string userId)
        {
            context.Database.ExecuteSqlInterpolated($@"
                                                                                UPDATE Notifications
                                                                                    SET IsSeen = 1
                                                                                WHERE UserId = {userId}
                                                                                AND IsSeen = 0");
        }
    }
}
