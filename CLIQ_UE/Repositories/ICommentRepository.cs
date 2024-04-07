using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ICommentRepository
    {
        Task<int> AddComment(Comment comment);
        List<Comment> GetCommentsByPost(int postId, string UID);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment? GetCommentById(int Id);
    }
}
