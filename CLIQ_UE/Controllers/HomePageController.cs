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

            if (user != null)
            {
                model.UserName = user.UserName;
                model.UserImage = user.PersonalImage;
                return View(model);
            }
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        public async Task<IActionResult> GetPosts(int pageIndex = 0, int pageSize = 5)
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            List<Post> posts = postService.GetLatestPosts(pageIndex, pageSize);

            displayPostViewModel displayPostViewModel = new displayPostViewModel();

            displayPostViewModel.Posts = posts;
            displayPostViewModel.currentUserId = user.Id;
            displayPostViewModel.currentUserImage = user.PersonalImage;
            displayPostViewModel.currentUserusername = user.UserName;
            displayPostViewModel.currentUserFirstName = user.FirstName;
            displayPostViewModel.currentUserLastName = user.LastName;



            return Json(displayPostViewModel);
        }


    }
}