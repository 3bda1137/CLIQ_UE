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
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public CommentsController(ICommentService commentService
            , IMapper mapper
            , IUserLikeCommentService userLikeCommentService
            ,IPostService postService)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.userLikeCommentService = userLikeCommentService;
            this.postService = postService;
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
                await postService.IncreasePostComments(commentVM.PostId);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetComments(int postId)
        {
            string UID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            List<Comment> comments = commentService.GetCommentsByPost(postId,UID);
            List<RespCommentVM> respCommentVMs = new List<RespCommentVM>();
            foreach (Comment comment in comments)
            {
                var respCommentVM = mapper.Map<Comment, RespCommentVM>(comment);

                respCommentVMs.Add(respCommentVM);
            }
            return Json(respCommentVMs);
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
