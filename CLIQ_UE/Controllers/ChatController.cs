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
        private readonly ILastSeenServices lastSeenServices;
        private readonly IOnlineUserServices onlineUserServices;

        //private string userId; 
        public ChatController(IFollowersServices followersServices
                                ,IChatIndividualServices chatIndividualServices
                                ,ILastSeenServices lastSeenServices
                                ,IOnlineUserServices onlineUserServices)
        {
            this.followersServices = followersServices;
            this.chatIndividualServices = chatIndividualServices;
            this.lastSeenServices = lastSeenServices;
            this.onlineUserServices = onlineUserServices;
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
        
        public IActionResult GetLastSeen(string otherUserId)
        {
            var last= onlineUserServices.GetByID(otherUserId);
            if (last == null)
            {
                var lastSeen = lastSeenServices.GetByUserId(otherUserId).LastSeenTime;
                return Json(CalculateLastSeen(lastSeen.ToString("yyyy-MM-dd HH:mm")));
            }
            else
            {
                return Json("Online");
            }
        }
        static string CalculateLastSeen(string personTime)
        {
            // Parse the person's time string into a DateTime object
            DateTime personDateTime = DateTime.ParseExact(personTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            // Get today's date without the time component
            DateTime today = DateTime.Today;

            // Get the time difference
            TimeSpan timeDifference = DateTime.Now - personDateTime;

            // Check if the date difference is zero
            if (personDateTime.Date == today)
            {
                // If the time difference is zero, return "online"
                if (timeDifference.TotalMinutes <= 0)
                {
                    return "Online";
                }
                // If the time difference is greater than zero, return the specified time without a date
                else
                {
                    return "Today " + personDateTime.ToString("hh:mm tt");
                }
            }
            // Check if the date difference is one (yesterday)
            else if (personDateTime.Date == today.AddDays(-1))
            {
                // Return "yesterday" with the time without the date
                return "Yesterday " + personDateTime.ToString("hh:mm tt");
            }
            // If the difference is greater than one, return the given date as it is
            else
            {
                return personDateTime.ToString("yyyy-MM-dd hh:mm tt");
            }
        }


    }
}
