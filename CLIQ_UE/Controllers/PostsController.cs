using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserLikePostService userLikePostService;
        private readonly IFollowersServices followersServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<NotificationHub> notificationHub;
        private readonly INotificationService notificationService;
        private readonly IHubContext<PostsHub> hub;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager

            , IHubContext<PostsHub> hub, IUserLikePostService userLikePostService, IFollowersServices followersServices

            , IHubContext<NotificationHub> notificationHub,
            INotificationService notificationService)

        {
            this.postService = postService;
            this.userManager = userManager;
            this.hub = hub;
            this.userLikePostService = userLikePostService;

            this.followersServices = followersServices;

            this.notificationHub = notificationHub;
            this.notificationService = notificationService;
        }
        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostViewModel postModel)
        {
            if (ModelState.IsValid && (postModel.PostImage != null || postModel.postContent != null))
            {
                ApplicationUser user = await userManager.GetUserAsync(User);
                postService.LoadFollowingId(user.Id);
                Post post = postService.CreatePost(postModel, user);
                List<string> followerIds = followersServices.GetFollowingIds(user.Id);

                if (post.privacy == "friends")
                {
                    followerIds.Add(user.Id);
                    foreach (var followerId in followerIds)
                    {
                        string connectionId = ConnectionManager.GetConnectionIdForUserId(followerId);

                        if (!string.IsNullOrEmpty(connectionId))
                        {
                            await hub.Clients.Client(connectionId).SendAsync("NewPostCreated", post);
                        }
                    }
                }
                else if (post.privacy == "private")
                {
                    string connectionId = ConnectionManager.GetConnectionIdForUserId(user.Id);
                    await hub.Clients.Client(connectionId).SendAsync("NewPostCreated", post);

                }

                else
                {
                    await hub.Clients.All.SendAsync("NewPostCreated", post);
                }

                postService.Save();

                return RedirectToAction("index", "HomePage");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> InteractPost(int PostId, bool LikeOption)
        {



            string UID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserLikePost? userLikePost = userLikePostService.Get(PostId, UID);

            Post Post = postService.GetPostById(PostId);

            if (Post != null)
            {
                //The first time to interact
                if (userLikePost == null)
                {
                    if (LikeOption)
                    {
                        Post.LikeCount++;
                        await postService.UpdatePost(Post);
                        if (Post.UserId != UID)
                        {
                            notificationService.AddNotification(Post.UserId, UID, "loved your post");
                            await notificationHub.Clients.User(Post.UserId).SendAsync("LikeNotification", UID);
                        }
                    }
                    else
                    {
                        Post.DislikeCount++;
                        await postService.UpdatePost(Post);
                        if (Post.UserId != UID)
                        {
                            notificationService.AddNotification(Post.UserId, UID, "disliked your post");
                            await notificationHub.Clients.User(Post.UserId).SendAsync("DisLikeNotification");
                        }
                            
                    }
                    userLikePost = new UserLikePost()
                    {
                        PostId = PostId,
                        ApplicationUserId = UID,
                        isLiked = LikeOption
                    };
                    userLikePostService.Add(userLikePost);
                }
                //Has itneracted before
                else
                {
                    if (userLikePost!.isLiked) //Current state is liked => change it to disliked
                    {
                        if (!LikeOption)
                        {
                            Post.LikeCount--;
                            Post.DislikeCount++;
                            await postService.UpdatePost(Post);
                            userLikePost.isLiked = !userLikePost.isLiked;
                            userLikePostService.Update(userLikePost);
                            if (Post.UserId != UID)
                            {
                                notificationService.AddNotification(Post.UserId, UID, "disliked your post");
                                await notificationHub.Clients.User(Post.UserId).SendAsync("DisLikeNotification");
                            }
                                
                        }
                        else
                        {
                            Post.LikeCount--;
                            await postService.UpdatePost(Post);
                            userLikePostService.Delete(userLikePost);
                        }
                    }
                    else
                    {
                        if (LikeOption)
                        {
                            Post.LikeCount++;
                            Post.DislikeCount--;
                            await postService.UpdatePost(Post);
                            userLikePost.isLiked = !userLikePost.isLiked;
                            userLikePostService.Update(userLikePost);
                            if (Post.UserId != UID)
                            {
                                notificationService.AddNotification(Post.UserId, UID, "loved your post");
                                await notificationHub.Clients.User(Post.UserId).SendAsync("LikeNotification", UID);
                            }
                                
                        }
                        else
                        {
                            Post.DislikeCount--;
                            await postService.UpdatePost(Post);
                            userLikePostService.Delete(userLikePost);
                        }
                    }
                }
                return Json(new { likes = Post.LikeCount, dislikes = Post.DislikeCount });
            }
            else
            {
                return BadRequest();
            }
        }


        public IActionResult Delete(int id)
        {
            postService.DeletePost(id);
            return Ok();
        }

    }
}
