using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLIQ_UE.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(AddCommentViewModel commentVM)
        {
            await commentService.AddComment(commentVM, User);
            return Ok();
        }
    }
}
