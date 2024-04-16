using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class BookMarkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookMarkService _bookMarkService;
        private readonly IUserServices _userServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService postService;

        public BookMarkController(IBookMarkService bookMarkService, IMapper mapper, IUserServices userServices, UserManager<ApplicationUser> userManager, IPostService postService)
        {
            _mapper = mapper;
            _userManager = userManager;
            this.postService = postService;
            _bookMarkService = bookMarkService;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllBookMarks()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var postIds = _bookMarkService.getAllPostsId(currentUserId);
            List<Post> posts = new List<Post>();

            if (postIds != null)
            {
                foreach (var post in postIds)
                {
                    Post p = postService.GetPostById(post);
                    if (p != null)
                    {
                        posts.Add(p);
                    }
                }
            }

            return Json(posts);
        }

        [HttpPost]
        public void AddBookMark(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Create a new bookmark
            BookMark bookmark = new BookMark
            {
                Id = Guid.NewGuid().ToString(),
                PostID = postId,
                UserID = userId,
                saveDate = DateTime.Now,

            };

            _bookMarkService.AddBookMark(bookmark);

        }


        public async Task<IActionResult> RemoveBookmark(int postId)
        {
            var user = await _userManager.GetUserAsync(User);
            _bookMarkService.removeBookmark(postId, user.Id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> IsisBookmarked(int postId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            bool isBookmarked = _bookMarkService.isBookmarked(postId, user.Id);
            if (isBookmarked)
            {
                return Ok();
            }
            else
                return BadRequest();

        }

    }


}

