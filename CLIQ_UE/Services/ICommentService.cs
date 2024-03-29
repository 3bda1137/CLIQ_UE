using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;
using System.Security.Claims;

namespace CLIQ_UE.Services
{
    public interface ICommentService
    {
        Task AddComment(AddCommentViewModel commentVM, ClaimsPrincipal User);
        List<Comment> GetCommentsByPost(int postId);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
