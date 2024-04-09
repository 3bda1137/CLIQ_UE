namespace CLIQ_UE.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string CreatedByUserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }

        public DateTime notificationDate { get; set; }
        public string notificationShowDate { get; set; }

    }
}
