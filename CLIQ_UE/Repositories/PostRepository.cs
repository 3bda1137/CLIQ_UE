using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public void CreatePost(CreatePostViewModel postModel, ApplicationUser user)
        {
            IFormFile imageFile = postModel.PostImage;

            using (var stream = imageFile.OpenReadStream())
            {
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }

                string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string imagePath = Path.Combine("wwwroot", "images", imageFileName);

                // Save the image to the specified path
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                // Construct the URL of the image
                string imageUrl = $"/images/{imageFileName}";

                // Create the post object
                Post post = new Post
                {
                    PostDate = DateTime.Now,
                    UserId = user.Id,
                    privacy = postModel.privacyValue,
                    ImageData = imageData,
                    TextContent = postModel.postContent,
                    PostImage = imageUrl,
                    User = user
                };

                context.Posts.Add(post);
            }
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
