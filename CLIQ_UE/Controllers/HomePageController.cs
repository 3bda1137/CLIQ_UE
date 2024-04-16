using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
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
        private readonly ISearchRepository searchRepository;

        // Injection in the constructor 
        public HomePageController(UserManager<ApplicationUser> userManager, IPostService postService, ISuggestesUsersService suggestesUsersService, INotificationService notificationService, ISearchRepository searchRepository)
        {

            this.userManager = userManager;
            this.postService = postService;
            this.suggestesUsersService = suggestesUsersService;
            this.notificationService = notificationService;
            this.searchRepository = searchRepository;
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
        [HttpGet]
        public IActionResult SearchByName(string search)
        {
            return View("SearchByUserName");
        }

            [HttpPost]
        public IActionResult SearchByUserName(string term)
        {
            //List<ApplicationUser> users = searchRepository.GetAllUsers();
            List<SearchUserViewModel> users;

            if (term != null)
            {
                var applicationUsers = searchRepository.SearchUsers(term);
                users = applicationUsers.Select(user => new SearchUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    UserImage = user.UserName
                }).ToList();
            }
            else
            {
                var applicationUsers = searchRepository.GetAllUsers();
                users = applicationUsers.Select(User => new SearchUserViewModel
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    UserName = User.UserName,
                    UserImage = User.UserName
                }).ToList();
            }

            ViewBag.Search = term;
            return View("SearchByUserName", users);
        }

        public IActionResult SearchSuggestions(string term)
        {
            var userNames = searchRepository.GetSuggestions(term);
            var userVM = userNames.Select(user => new SearchUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                UserImage = user.UserName
            }).ToList();
            return Json(userNames);
        }
    }
}