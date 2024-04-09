using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class UserLikePostRepository : IUserLikePostRepository
    {
        private readonly ApplicationContext _context;

        public UserLikePostRepository(ApplicationContext context)
        {
            _context = context;
        }

        public UserLikePost? Get(int postId, string UID)
        {
            return _context.UserLikePosts.Find(postId, UID);
        }

        public UserLikePost Update(UserLikePost user)
        {
            UserLikePost res = _context.UserLikePosts.Update(user).Entity;
            _context.SaveChanges();
            return res;
        }

        public UserLikePost Add(UserLikePost user)
        {
            UserLikePost res = _context.UserLikePosts.Add(user).Entity;
            _context.SaveChanges();
            return res;
        }

        public void Delete(UserLikePost likePost)
        {
            _context
                .UserLikePosts
                .Remove(likePost);
            _context.SaveChanges();
        }
    }
}
