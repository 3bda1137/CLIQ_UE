using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class FollowController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFollowersServices followersServices;
        private readonly IUserServices userServices;

        public FollowController(UserManager<ApplicationUser> userManager, IFollowersServices followersServices,IUserServices userServices)
        {
            this.userManager = userManager;
            this.followersServices = followersServices;
            this.userServices = userServices;
        }


        [HttpPost]
        public async Task<IActionResult> FollowUser([FromBody] string followingId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            ApplicationUser applicationUser = userServices.GetByID(followingId);
            Followers newFollow = new Followers();
            newFollow.ImageUrl = applicationUser.PersonalImage;
            newFollow.FollowingName = applicationUser.FirstName + " " + applicationUser.LastName;
            newFollow.FollowingId = followingId;
            newFollow.FollowerId = currentUser.Id;
            newFollow.FollowingDate = DateTime.Now;

            followersServices.Add(newFollow);
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> UnFollowUser([FromBody] string followingId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            Followers currentFollow = new Followers();

            currentFollow.FollowingId = followingId;
            currentFollow.FollowerId = currentUser.Id;

            followersServices.UnFollow(currentFollow);
            return Ok();

        }

    }
}
