using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Controllers
{
    public class FollowController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFollowersServices followersServices;
        private readonly IUserServices userServices;
        private readonly IHubContext<NotificationHub> notificationHub;
        private readonly INotificationService notificationService;

        public FollowController(UserManager<ApplicationUser> userManager, IFollowersServices followersServices, IUserServices userServices, IHubContext<NotificationHub> notificationHub, INotificationService notificationService)

        {
            this.userManager = userManager;
            this.followersServices = followersServices;
            this.userServices = userServices;
            this.notificationHub = notificationHub;
            this.notificationService = notificationService;
        }


        [HttpPost]
        public async Task<IActionResult> FollowUser([FromBody] string followingId)
        {
            ApplicationUser applicationUser = userServices.GetByID(followingId);
            var currentUser = await userManager.GetUserAsync(User);

            Followers newFollow = new Followers();

            if (!followersServices.IsUserFollowing(currentUser.Id, followingId))
            {
                newFollow.ImageUrl = applicationUser.PersonalImage;
                newFollow.FollowingName = applicationUser.FirstName + " " + applicationUser.LastName;
                newFollow.FollowingId = followingId;
                newFollow.FollowerId = currentUser.Id;
                newFollow.FollowingDate = DateTime.Now;

                followersServices.Add(newFollow);

                //// Send Notification  And Save in db
                notificationService.AddNotification(followingId, currentUser.Id, "followed you");
                await notificationHub.Clients.User(followingId).SendAsync("ReceiveFollowNotification");
            }

            newFollow.ImageUrl = applicationUser.PersonalImage;
            newFollow.FollowingName = applicationUser.FirstName + " " + applicationUser.LastName;


            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> UnFollowUser([FromBody] string followingId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            Followers currentFollow = new Followers();
            if (followersServices.IsUserFollowing(currentUser.Id, followingId))
            {
                currentFollow.FollowingId = followingId;
                currentFollow.FollowerId = currentUser.Id;

                followersServices.UnFollow(currentFollow);
                ///Send Notification --> 
                notificationService.AddNotification(followingId, currentUser.Id, "unfollowed your profile");

                await notificationHub.Clients.User(followingId).SendAsync("ReceiveUnfollowNotification");
            }

            return Ok();

        }

    }
}
