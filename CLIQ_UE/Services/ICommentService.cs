using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;
using System.Security.Claims;

namespace CLIQ_UE.Services
{
    public interface ICommentService
    {
        Task<bool> AddComment(AddCommentViewModel commentVM, ClaimsPrincipal User);
        List<Comment> GetCommentsByPost(int postId, string UID);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
