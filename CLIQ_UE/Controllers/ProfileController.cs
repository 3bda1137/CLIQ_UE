using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostService postService;
        private readonly INotificationService notificationService;

        public ProfileController(UserManager<ApplicationUser> userManager, IPostService postService, INotificationService notificationService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.notificationService = notificationService;
        }
        public async Task<IActionResult> Index(ProfileViewModel model)
        {

            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                model.UserName = user.UserName;
                model.UserImage = user.PersonalImage;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.CoverImage = user.ProfileImage;
                model.newNotificationCount = notificationService.GetNewNotifications(user.Id).Count();
                return View(model);
            }
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        public async Task<IActionResult> GetPosts(int pageIndex = 0, int pageSize = 5)
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            List<Post> posts = postService.GetLatestPostsByUserId(user.Id, pageIndex, pageSize);

            displayPostViewModel displayPostViewModel = new displayPostViewModel();

            displayPostViewModel.Posts = posts;
            displayPostViewModel.currentUserId = user.Id;
            displayPostViewModel.currentUserImage = user.PersonalImage;
            displayPostViewModel.currentUserusername = user.UserName;
            displayPostViewModel.currentUserFirstName = user.FirstName;
            displayPostViewModel.currentUserLastName = user.LastName;



            return Json(displayPostViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetPostsImages()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            List<string> allImages = postService.allPostsImagesById(user.Id);


            return Json(allImages);
        }
    }
}
