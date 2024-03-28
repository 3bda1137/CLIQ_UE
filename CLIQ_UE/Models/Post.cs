namespace CLIQ_UE.Models
{
    public class Post
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } 
        public DateTime PostDate { get; set; }
        public List<string> PostImages { get; set; }
        public List<string> Videos { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int RepostCount { get; set; }
        public int CommentCount { get; set; }

        // Navigation properties
        public ICollection<View> Views { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public enum ReactionType
    {
        Like,
        Dislike,
        Love,
        Repost,

    }
}
