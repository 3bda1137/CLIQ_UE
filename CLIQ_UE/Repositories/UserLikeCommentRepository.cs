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
            return  _context
                    .UserLikeComments
                    .Find(userLikeComment.CommentId, userLikeComment.ApplicationUserId);
        }

        /*public List<UserLikeComment> GetCurUserLikeComments(string UID)
        {
            return _context
                    .UserLikeComments
                    .Where(u => u.ApplicationUserId == UID).ToList();
        }*/

        public async Task<int> LikeComment(UserLikeComment like)
        {
            _context
                .UserLikeComments
                .Add(like);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> UnLikeComment(UserLikeComment like)
        {
            _context
                .UserLikeComments
                .Remove(like);
           return await _context.SaveChangesAsync();
        }
    }
}
