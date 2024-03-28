using CLIQ_UE.Models;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;

        public PostRepository(ApplicationContext _context)
        {
            context = _context;
        }

        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public void AddView(View view)
        {
            throw new NotImplementedException();
        }

        public void CreatePost(Post newPost)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetLatestPosts()
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int id)
        {
            return context.Posts.Include(p => p.User)
                  .Include(p => p.Reactions)
                  .Include(p => p.Comments)
                  .Include(p => p.Views)
                  .FirstOrDefault(p => p.Id == id);
        }

        public List<Reaction> GetReactionsByPostID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
