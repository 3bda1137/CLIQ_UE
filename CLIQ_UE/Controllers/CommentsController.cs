using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CLIQ_UE.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserLikeCommentService userLikeCommentService;
        private readonly IMapper mapper;

        public CommentsController(ICommentService commentService
            , IMapper mapper
            , IUserLikeCommentService userLikeCommentService)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.userLikeCommentService = userLikeCommentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(AddCommentViewModel commentVM)
        {
            bool res = await commentService.AddComment(commentVM, User);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetComments(int postId)
        {
            return Json(commentService.GetCommentsByPost(postId));
        }

        public IActionResult LikeComment(int commentId)
        {
            int Id = commentId;
            if (ModelState.IsValid)
            {
                UserLikeComment userLikeComment = new UserLikeComment()
                {
                    CommentId = Id,
                    ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
                };
                return Json(userLikeCommentService.LikeComment(userLikeComment));
            }
            return Json(ModelState);
        }

        public IActionResult UnLikeComment(LikeCommentVM likeCommentVM)
        {
            if (ModelState.IsValid)
            {
                UserLikeComment userLikeComment = mapper.Map<UserLikeComment>(likeCommentVM);
                return Json(userLikeCommentService.LikeComment(userLikeComment));
            }
            return Json(ModelState);
        }
    }
}
