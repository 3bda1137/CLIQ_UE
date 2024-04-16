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
        private readonly ISuggestesUsersService suggestesUsersService;
        private readonly INotificationService notificationService;
        private readonly IBookMarkService bookMarkService;

        // Injection in the constructor 
        public HomePageController(UserManager<ApplicationUser> userManager, IPostService postService, ISuggestesUsersService suggestesUsersService, INotificationService notificationService, IBookMarkService bookMarkService)
        {

            this.userManager = userManager;
            this.postService = postService;
            this.suggestesUsersService = suggestesUsersService;
            this.notificationService = notificationService;
            this.bookMarkService = bookMarkService;
        }

        public async Task<IActionResult> Index(HomePageViewModel model)
        {

            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                model.UserName = user.UserName;
                model.UserImage = user.PersonalImage;
                model.SuggestesUsers = suggestesUsersService.GetSuggestesUsers(user.Id);
                model.userId = user.Id;
                model.newNotificationCount = notificationService.GetNewNotifications(user.Id).Count();
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
            displayPostViewModel.BookmarksIds = bookMarkService.getAllPostsId(user.Id);

            displayPostViewModel.Posts = posts;
            displayPostViewModel.currentUserId = user.Id;
            displayPostViewModel.currentUserImage = user.PersonalImage;
            displayPostViewModel.currentUserusername = user.UserName;
            displayPostViewModel.currentUserFirstName = user.FirstName;
            displayPostViewModel.currentUserLastName = user.LastName;

            return Json(displayPostViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            List<Notification> notifications = notificationService.GetAllNotifications(user.Id);
            return Json(notifications);
        }




    }
}