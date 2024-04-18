using CLIQ_UE.Helpers;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class ChatController : Controller
    {
        private readonly IFollowersServices followersServices;
        private readonly IChatIndividualServices chatIndividualServices;
        private readonly ILastSeenServices lastSeenServices;
        private readonly IOnlineUserServices onlineUserServices;
        private readonly ILastMessageServices lastMessageServices;
        private readonly UserManager<ApplicationUser> userManager;

        //private string userId; 
        public ChatController(IFollowersServices followersServices
                                , IChatIndividualServices chatIndividualServices
                                , ILastSeenServices lastSeenServices
                                , IOnlineUserServices onlineUserServices
                                ,ILastMessageServices lastMessageServices
                                , UserManager<ApplicationUser> userManager)
        {
            this.followersServices = followersServices;
            this.chatIndividualServices = chatIndividualServices;
            this.lastSeenServices = lastSeenServices;
            this.onlineUserServices = onlineUserServices;
            this.lastMessageServices = lastMessageServices;
            this.userManager = userManager;
            //this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            List<UserConntactViewModel> userConntactVM = followersServices
                .GetAllByFollowingId(userId);
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
                userConntactViewModels =
                followersServices.GetAllByFollowingId(userId);

            return Json(userConntactViewModels);
        }

        public async Task<IActionResult> GetMessages(string otherUserId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ChatIndividual> chatt = chatIndividualServices.GetChat(otherUserId, userId);
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);
            return Json(new { chat = chatt, myImage = applicationUser.PersonalImage });
        }

        public IActionResult GetLastSeen(string otherUserId)
        {
            var last = onlineUserServices.GetByID(otherUserId);
            if (last == null)
            {
                var lastSeen = lastSeenServices.GetByUserId(otherUserId).LastSeenTime;
                return Json(FormatTimeForChat.CalculateLastSeen(lastSeen.ToString("yyyy-MM-dd HH:mm")));
            }
            else 
            {
                return Json("Online");
            }
        }
    }
}
