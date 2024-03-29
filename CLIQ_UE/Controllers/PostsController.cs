using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> CreatePost(CreatePostViewModel post)
        {
            ApplicationUser user = await userManager.GetUserAsync(User);

            postService.CreatePost(post, user);

            if (ModelState.IsValid)
            {
                postService.Save();
                return RedirectToAction("Index", "HomePage");
            }
            else
            {
                // Handle invalid model state
                return BadRequest(ModelState);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
