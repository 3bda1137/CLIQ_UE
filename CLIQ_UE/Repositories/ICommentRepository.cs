using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        List<Comment> GetCommentsByPost(int postId);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment? GetCommentById(int Id);
    }
}
