using CLIQ_UE.Models;

namespace CLIQ_UE.ViewModels
{
    public class displayPostViewModel
    {
        public List<Post> Posts { get; set; }
        //public ApplicationUser user { get; set; }
        public string currentUserId { get; set; }
        public string currentUserImage { get; set; }
        public string currentUserFirstName { get; set; }
        public string currentUserLastName { get; set; }
        public string currentUserusername { get; set; }
        public string Bio { get; set; }
        public List<int> BookmarksIds { get; set; }

    }
}
