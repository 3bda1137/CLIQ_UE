using CLIQ_UE.Models;

namespace CLIQ_UE.ViewModels
{
    public class UserConntactViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public LastMessage LastMessage { get; set; }
    }
}
