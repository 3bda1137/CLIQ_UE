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
        public async Task<int> AddComment(Comment comment)
        {
            context
                .Comments
                .Add(comment);
            return await context.SaveChangesAsync();
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

        public List<Comment> GetCommentsByPost(int postId, string UID)
        {
            return context
                .Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .Include(c => c.UserLikeComments.Where(ULK => ULK != null && ULK.ApplicationUserId == UID))
                .ToList();
        }

        public async Task<int> UpdateComment(Comment comment)
        {
            context
                .Comments
                .Update(comment);
            return await context.SaveChangesAsync();
        }
    }
}
