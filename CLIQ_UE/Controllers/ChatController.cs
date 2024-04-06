using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
	public class ChatController : Controller
	{
        private readonly IFollowersServices followersServices;

        public ChatController(IFollowersServices followersServices)
        {
            this.followersServices = followersServices;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserConntactViewModel> userConntactVM = followersServices.GetAllByFollowingId(userId); 
			return View(userConntactVM);
		}
	}
}
