using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CLIQ_UE.Controllers
{
    public class BookMarkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookMarkService _bookMarkService;
        private readonly IUserServices _userServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService postService;

        public BookMarkController(IBookMarkService bookMarkService, IMapper mapper, IUserServices userServices, UserManager<ApplicationUser> userManager,IPostService postService)
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


        public IActionResult GetAllBookMarks()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            List<BookMark> bookMarks = _bookMarkService.GetAllBookMark(currentUserId);
           // List<Post> posts = postService.GetPostById(currentUserId);

           // List<BookMarkViewModel> bookMarkVMs = _mapper.Map<List<BookMarkViewModel>>(bookMarks);

            return View("GetAllBookMark", bookMarks);
        }

        [HttpPost]
        public  void AddBookMark(string postId)
        {  if (!string.IsNullOrEmpty(postId))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    // Create a new bookmark
                    BookMark bookmark = new BookMark
                    {
                        Id=Guid.NewGuid().ToString(),
                        PostID = postId,
                        UserID = userId
                    
                    };

                     _bookMarkService.AddBookMark(bookmark);

                 
                }

              
            }
         
        }


    }

