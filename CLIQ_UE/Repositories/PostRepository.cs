using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public PostRepository(ApplicationContext _context, UserManager<ApplicationUser> userManager)
        {
            context = _context;
            this.userManager = userManager;
        }

        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public void AddView(View view)
        {
            throw new NotImplementedException();
        }

        public void CreatePost([FromBody] CreatePostViewModel postModle, ApplicationUser user)
        {
            CreatePostViewModel createPost = new CreatePostViewModel();

            Post post = new Post();
            {
                post.PostDate = DateTime.Now;
                post.UserId = user.Id;
                post.privacy = postModle.privacyValue;
                post.TextContent = postModle.postContent;
                post.PostImages = postModle.PostImages;
                post.User = user;

            }

            context.Posts.Add(post);

        }

        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetLatestPosts()
        {
            return context.Posts.OrderBy(p => p.PostDate)
                .Take(100)
                .ToList();
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

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
