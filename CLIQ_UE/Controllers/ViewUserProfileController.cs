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

        public ViewUserProfileController(UserManager<ApplicationUser> userManager, IPostService postService, IUserServices userServices, IFollowersServices followersServices, IPostService postService1)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.userServices = userServices;
            this.followersServices = followersServices;
            this.postService1 = postService1;
        }

        public async Task<IActionResult> Index(ProfileViewVM model, string userId)
        {
            ViewData["userId"] = userId;
            var currentUser = await userManager.GetUserAsync(User);
            var userVisited = userServices.GetByID(userId);

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

                model.PostCount = postService.GetUserPostCount(userVisited.Id);
                model.FollowersCount = followersServices.GetFollowerCount(userVisited.Id);
                model.FollowingCount = followersServices.GetFollowingCount(userVisited.Id);

                model.IsFollowing = followersServices.IsUserFollowing(currentUser.Id, userVisited.Id);
                model.IsMutualFollowing = model.IsFollowing && followersServices.IsUserFollowing(userVisited.Id, currentUser.Id);

                return View(model);
            }

            return RedirectToAction("index", "HomePage");
        }


        [HttpGet]
        public async Task<IActionResult> GetPosts(string userId, int pageIndex = 0, int pageSize = 5)
        {


            var userVisited = userServices.GetByID(userId);

            List<Post> posts = postService.GetLatestPostsByUserId(userId, pageIndex, pageSize);

            displayPostViewModel displayPostViewModel = new displayPostViewModel();

            displayPostViewModel.Posts = posts;
            displayPostViewModel.currentUserId = userVisited.Id;
            displayPostViewModel.currentUserImage = userVisited.PersonalImage;
            displayPostViewModel.currentUserusername = userVisited.UserName;
            displayPostViewModel.currentUserFirstName = userVisited.FirstName;
            displayPostViewModel.currentUserLastName = userVisited.LastName;



            return Json(displayPostViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetPostsImages(string userId)
        {


            List<string> allImages = postService.allPostsImagesById(userId);


            return Json(allImages);
        }
    }
}
