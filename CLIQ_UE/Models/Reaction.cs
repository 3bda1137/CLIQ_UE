namespace CLIQ_UE.Models
{
    public class Reaction
    {
        public int ReactionId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public ReactionType ReactionType { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public ApplicationUser User { get; set; }
    }
}
