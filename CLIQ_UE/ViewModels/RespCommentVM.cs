namespace CLIQ_UE.ViewModels
{
    public class RespCommentVM
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserProfileImage { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? CommentImage { get; set; }
        public int LikeCount { get; set; }
        public bool IsLikedByMe { get; set; } = false;
    }
}
