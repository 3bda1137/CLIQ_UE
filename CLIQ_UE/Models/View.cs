namespace CLIQ_UE.Models
{
    public class View
    {
        public int ViewId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime ViewDate { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
    }
}
