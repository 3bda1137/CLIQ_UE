using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class BookMarkController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBookMarkService bookMarkService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserServices userServices;

        public BookMarkController(IBookMarkService bookMarkService, IMapper mapper, IUserServices userServices)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.bookMarkService = bookMarkService;
            this.userServices = userServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllBookMark(String userID)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != userID.ToString())
            {
                return Unauthorized();
            }

            List<BookMark> bookMarks = bookMarkService.GetAllBookMark(userID);

            List<BookMarkViewModel> bookMarkVMs = mapper.Map<List<BookMarkViewModel>>(bookMarks);

            return Ok(bookMarkVMs);
        }
        public async Task<IActionResult> AddBookMark(BookMarkViewModel bookMarkVM)
        {
            bool Add = await bookMarkService.AddBookMark(bookMarkVM);
            if (Add)
            {

                await userServices.BookMark(bookMarkVM.currentUserId);
                return Ok();
            }
            return BadRequest();
        }


    }
}
    