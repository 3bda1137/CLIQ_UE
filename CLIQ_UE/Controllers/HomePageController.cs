using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace CLIQ_UE.Controllers
{
    [Authorize]
    public class HomePageController : Controller
    {


        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostService postService;

        // Injection in the constructor 
        public HomePageController(UserManager<ApplicationUser> userManager, IPostService postService)
        {

            this.userManager = userManager;
            this.postService = postService;
        }

        public async Task<IActionResult> Index(HomePageViewModel model)
        {

            var user = await userManager.GetUserAsync(User);
            model.UserName = user?.UserName!;
            model.UserImage = user?.ProfileImage!;
            model.LatestPosts = postService.GetLatestPosts();
            return View(model);
        }
    }
}
