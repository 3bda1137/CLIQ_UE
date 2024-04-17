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
        private readonly IFollowersServices followersServices;
        private readonly IBookMarkService bookMarkService;

        public ProfileController(UserManager<ApplicationUser> userManager, IPostService postService, INotificationService notificationService, IFollowersServices followersServices, IBookMarkService bookMarkService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.notificationService = notificationService;
            this.followersServices = followersServices;
            this.bookMarkService = bookMarkService;
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
                model.userId = user.Id;
                model.PostCount = postService.GetUserPostCount(user.Id);
                model.FollowersCount = followersServices.GetFollowerCount(user.Id);
                model.FollowingCount = followersServices.GetFollowingCount(user.Id);
                model.Location = user.Location;
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
            displayPostViewModel.BookmarksIds = bookMarkService.getAllPostsId(user.Id);



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
