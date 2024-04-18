using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserServices userServices;
        private readonly IFollowersServices followersServices;

        public SearchController(ISearchService searchService, UserManager<ApplicationUser> userManager, IUserServices userServices, IFollowersServices followersServices)
        {
            this.searchService = searchService;
            this.userManager = userManager;
            this.userServices = userServices;
            this.followersServices = followersServices;
        }
        public async Task<IActionResult> searchUsers(string str)
        {
            var cuurentUser = await userManager.GetUserAsync(User);
            List<string> ids = searchService.usersResultId(str);

            List<SearchResultVM> results = new List<SearchResultVM>();

            foreach (string id in ids)
            {
                var user = userServices.GetByID(id);
                if (cuurentUser.Id != user.Id)
                {

                    results.Add(new SearchResultVM()
                    {
                        UserId = user.Id,
                        FullName = user.FirstName + " " + user.LastName,
                        userImage = user.PersonalImage,
                        IsIFollow = followersServices.IsUserFollowing(cuurentUser.Id, user.Id),
                        IsFollowingMe = followersServices.IsUserFollowing(user.Id, cuurentUser.Id),
                        mutualFollowers = followersServices.GetMutualFollowersCount(id, cuurentUser.Id),
                    }); ;
                }
            }

            return Json(results);
        }
    }
}
