using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class ChatController : Controller
    {
        private readonly IFollowersServices followersServices;
        private readonly IChatIndividualServices chatIndividualServices;
        //private string userId; 
        public ChatController(IFollowersServices followersServices,IChatIndividualServices chatIndividualServices)
        {
            this.followersServices = followersServices;
            this.chatIndividualServices = chatIndividualServices;
            //this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            List<UserConntactViewModel> userConntactVM = followersServices.GetAllByFollowingId(userId);
            return View(userConntactVM);
        }
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserConntactViewModel> userConntactViewModels;
            if (searchTerm != null)
                userConntactViewModels =
                followersServices.GetAllBySeachWords(searchTerm, userId);
            
            else
                userConntactViewModels=
                followersServices.GetAllByFollowingId(userId);

            return Json(userConntactViewModels);
        }

        public IActionResult GetMessages(string otherUserId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chat = chatIndividualServices.GetChat(otherUserId, userId);
            return Json(chat);
        }
    }
}
