namespace CLIQ_UE.ViewModels
{
	public class ProfileViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set; }
		public string CoverImage { get; set; }

		public string userId { get; set; }

		public int newNotificationCount { get; set; }

		public int PostCount { get; set; }
		public int FollowingCount { get; set; }
		public int FollowersCount { get; set; }
		public string Location { get; set; }
		public string Bio { get; set; }
		public bool IsVerified { get; set; }
	}
}
