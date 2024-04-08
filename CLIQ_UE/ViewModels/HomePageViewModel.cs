using CLIQ_UE.Models;

namespace CLIQ_UE.ViewModels
{
    public class HomePageViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public List<Post>? LatestPosts { get; set; }
        ApplicationUser user { get; set; }

       public List<ApplicationUser> SuggestesUsers { get; set; }
    }
}
