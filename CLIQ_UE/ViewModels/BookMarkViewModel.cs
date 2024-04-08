using CLIQ_UE.Models;

namespace CLIQ_UE.ViewModels
{
    public class BookMarkViewModel
    {
        public List<Post> Posts { get; set; }
        public string currentUserId { get; set; }
        public string currentUserImage { get; set; }
        public string currentUserFirstName { get; set; }
        public string currentUserLastName { get; set; }
        public string currentUserusername { get; set; }
    }
}
