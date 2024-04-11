using CLIQ_UE.Hubs;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<PostsHub> hub;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager, IHubContext<PostsHub> hub)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.hub = hub;
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


    }
}
