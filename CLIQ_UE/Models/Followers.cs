namespace CLIQ_UE.Models
{
    public class Followers
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
        public string FollowerName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FollowingDate { get; set; }
    }
}
