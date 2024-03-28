using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface ICommentService
    {
        Comment AddComment(Comment comment);
        List<Comment> GetCommentsByPost(int postId);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
