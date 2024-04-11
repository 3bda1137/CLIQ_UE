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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<PostsHub> hub;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager
            , IHubContext<PostsHub> hub, IUserLikePostService userLikePostService)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.hub = hub;
            this.userLikePostService = userLikePostService;
        }


        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostViewModel postModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.GetUserAsync(User);
                Post post = postService.CreatePost(postModel, user);

                postService.Save();

                await hub.Clients.All.SendAsync("NewPostCreated", post);


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

            if(Post != null)
            {
                //The first time to interact
                if(userLikePost == null)
                {
                    if (LikeOption)
                    {
                        Post.LikeCount++;
                        await postService.UpdatePost(Post);
                    }
                    else 
                    {
                        Post.DislikeCount++;
                        await postService.UpdatePost(Post);
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
                        }
                        else
                        {
                            Post.DislikeCount--;
                            await postService.UpdatePost(Post);
                            userLikePostService.Delete(userLikePost);
                        }
                    }
                }
                return Json(new {likes = Post.LikeCount, dislikes = Post.DislikeCount});
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
