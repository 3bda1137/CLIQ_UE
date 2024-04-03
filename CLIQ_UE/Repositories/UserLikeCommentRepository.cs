using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class UserLikeCommentRepository : IUserLikeCommentRepository
    {
        private readonly ApplicationContext _context;

        public UserLikeCommentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public UserLikeComment? Get(UserLikeComment userLikeComment)
        {
            return _context
                    .UserLikeComments
                    .Find(userLikeComment.CommentId, userLikeComment.ApplicationUserId);
        }

        public void LikeComment(UserLikeComment like)
        {
            _context
                .UserLikeComments
                .Add(like);
            _context.SaveChanges();
        }

        public void UnLikeComment(UserLikeComment like)
        {
            _context
                .UserLikeComments
                .Remove(like);
            _context.SaveChanges();
        }
    }
}
