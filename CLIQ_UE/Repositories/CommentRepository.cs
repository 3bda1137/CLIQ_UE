using CLIQ_UE.Models;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext context;

        public CommentRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void AddComment(Comment comment)
        {
            context
                .Comments
                .Add(comment);
            context.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Comment? GetCommentById(int Id)
        {
            return context
                        .Comments
                        .Find(Id);

        }

        public List<Comment> GetCommentsByPost(int postId)
        {
            return context
                .Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .ToList();
        }

        public void UpdateComment(Comment comment)
        {
            context
                .Comments
                .Update(comment);
            context.SaveChanges();
        }
    }
}
