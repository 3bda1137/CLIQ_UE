namespace CLIQ_UE.Models
{
    public class Followers
    {
        public int Id { get; set; }
        public string CurrentUser { get; set; }
        public string FollowingId { get; set; }
        public string FollowingName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FollowingDate { get; set; }

        public ApplicationUser? Follower { get; set; }
        public ApplicationUser? Following { get; set; }
    }
}
