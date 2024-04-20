using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class ViewUserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostService postService;
        private readonly IUserServices userServices;
        private readonly IFollowersServices followersServices;
        private readonly IPostService postService1;
        private readonly INotificationService notificationService;
        private readonly IBookMarkService bookMarkService;

        public ViewUserProfileController(UserManager<ApplicationUser> userManager, IPostService postService, IUserServices userServices, IFollowersServices followersServices, IPostService postService1, INotificationService notificationService,
            IBookMarkService bookMarkService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.userServices = userServices;
            this.followersServices = followersServices;
            this.postService1 = postService1;
            this.notificationService = notificationService;
            this.bookMarkService = bookMarkService;
        }

        public async Task<IActionResult> Index(ProfileViewVM model, string userId)
        {
            ViewData["userId"] = userId;
            var currentUser = await userManager.GetUserAsync(User);
            var userVisited = userServices.GetByID(userId);

            if (userId == currentUser.Id)
            {
                return RedirectToAction("Index", "Profile");
            }
            if (currentUser != null && userVisited != null)
            {
                model.UserName = userVisited.UserName;
                model.UserImage = userVisited.PersonalImage;
                model.FirstName = userVisited.FirstName;
                model.LastName = userVisited.LastName;
                model.CoverImage = userVisited.ProfileImage;
                model.currentUserId = currentUser.Id;
                model.CurrentUserImage = currentUser.PersonalImage;
                model.CurrentUserName = currentUser.UserName;
                model.UserId = userId;
                model.PostCount = postService.GetUserPostCount(userVisited.Id);
                model.FollowersCount = followersServices.GetFollowerCount(userVisited.Id);
                model.FollowingCount = followersServices.GetFollowingCount(userVisited.Id);
                model.Location = currentUser.Location;
                model.Bio = currentUser.Bio;

                model.newNotificationCount = notificationService.GetNewNotifications(currentUser.Id).Count();


                model.IsFollowing = followersServices.IsUserFollowing(currentUser.Id, userVisited.Id);
                model.isFollowingMe = followersServices.IsUserFollowing(userVisited.Id, currentUser.Id);
                model.IsMutualFollowing = model.IsFollowing && followersServices.IsUserFollowing(userVisited.Id, currentUser.Id);

                return View(model);
            }

            return RedirectToAction("index", "HomePage");
        }


        [HttpGet]
        public async Task<IActionResult> GetPosts(string userId, int pageIndex = 0, int pageSize = 5)
        {


            var userVisited = userServices.GetByID(userId);
            ApplicationUser user = await userManager.GetUserAsync(User);
            postService.LoadFollowingId(user.Id);
            List<Post> posts = postService.GetLatestPostsByUserId(userId, user.Id, pageIndex, pageSize);

            displayPostViewModel displayPostViewModel = new displayPostViewModel();

            displayPostViewModel.Posts = posts;
            displayPostViewModel.currentUserId = userVisited.Id;
            displayPostViewModel.currentUserImage = userVisited.PersonalImage;
            displayPostViewModel.currentUserusername = userVisited.UserName;
            displayPostViewModel.currentUserFirstName = userVisited.FirstName;
            displayPostViewModel.currentUserLastName = userVisited.LastName;
            displayPostViewModel.BookmarksIds = bookMarkService.getAllPostsId(user.Id);


            return Json(displayPostViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetPostsImages(string userId)
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            postService.LoadFollowingId(user.Id);

            List<string> allImages = postService.allPostsImagesById(userId, user.Id);


            return Json(allImages);
        }
    }
}
