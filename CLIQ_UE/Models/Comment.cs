namespace CLIQ_UE.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? CommentImage { get; set; }
        public int LikeCount { get; set; }

        // Navigation properties
        public Post? Post { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
