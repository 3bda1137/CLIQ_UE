namespace CLIQ_UE.ViewModels
{
    public class SearchResultVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string userImage { get; set; }
        public bool IsIFollow { get; set; }
        public bool IsFollowingMe { get; set; }
        public int mutualFollowers { get; set; }
    }
}
