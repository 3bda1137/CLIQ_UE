using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
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

            if (!followersServices.IsUserFollowing(currentUser.Id, followingId) && currentUser.Id != followingId)
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
            if (followersServices.IsUserFollowing(currentUser.Id, followingId) && currentUser.Id != followingId)
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

        public async Task<IActionResult> getAllFollowers(string id)
        {
            List<string> followersId = followersServices.GetFollowersIds(id);
            List<Followers_FollowingListVM> model = new List<Followers_FollowingListVM>();
            ApplicationUser applicationUser = await userManager.GetUserAsync(User);
            foreach (var userId in followersId)
            {
                ApplicationUser user = userServices.GetByID(userId);
                model.Add(new Followers_FollowingListVM
                {
                    UserId = userId,
                    UserImage = user.PersonalImage,
                    UserName = user.FirstName + " " + user.LastName,
                    IsFollowing = followersServices.IsUserFollowing(applicationUser.Id, user.Id)
                });
            }


            return Json(model);

        }

        public async Task<IActionResult> getAllFollowing(string id)
        {
            List<string> followingIds = followersServices.GetFollowingIds(id);
            ApplicationUser applicationUser = await userManager.GetUserAsync(User);
            List<Followers_FollowingListVM> model = new List<Followers_FollowingListVM>();
            foreach (var userId in followingIds)
            {
                ApplicationUser user = userServices.GetByID(userId);
                model.Add(new Followers_FollowingListVM
                {
                    UserId = userId,
                    UserImage = user.PersonalImage,
                    UserName = user.FirstName + " " + user.LastName,
                    IsFollowing = followersServices.IsUserFollowing(applicationUser.Id, userId)
                });
            }


            return Json(model);

        }

        public async Task<IActionResult> getAllUsersToFollow(string id)
        {
            List<string> userToFollow = followersServices.GetAllUsersToFollow(id);
            ApplicationUser applicationUser = await userManager.GetUserAsync(User);
            List<Followers_FollowingListVM> model = new List<Followers_FollowingListVM>();
            foreach (var userId in userToFollow)
            {
                ApplicationUser user = userServices.GetByID(userId);
                model.Add(new Followers_FollowingListVM
                {
                    UserId = userId,
                    UserImage = user.PersonalImage,
                    UserName = user.FirstName + " " + user.LastName,
                    IsFollowing = followersServices.IsUserFollowing(applicationUser.Id, userId)
                });
            }


            return Json(model);

        }
    }
}
