namespace CLIQ_UE.ViewModels
{
	public class AddCommentViewModel
	{
		public int PostId { get; set; }
		//public string UserId { get; set; }
		public string CommentText { get; set; }
		public string? CommentImage { get; set; }
		public string FollowingId { get; set;}
	}
}
