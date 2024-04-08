using CLIQ_UE.Helpers;
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

        public List<string> allPostsImagesById(string id)
        {
            List<string> images = context.Posts.Where(p => p.UserId == id).OrderByDescending(p => p.PostDate).Select(p => p.PostImage).ToList();
            return images;
        }

        public Post CreatePost(CreatePostViewModel postModel, ApplicationUser user)
        {
            byte[] imageData = null;
            string imageUrl = null;
            if (postModel.PostImage != null && postModel.PostImage.Length > 0)
            {
                IFormFile imageFile = postModel.PostImage;

                using (var stream = imageFile.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string imagePath = Path.Combine("wwwroot", "PostsImages", imageFileName);

                    // Save the image to the specified path
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    // Construct the URL of the image
                    imageUrl = $"/PostsImages/{imageFileName}";
                }

            }
            // Create the post object
            Post post = new Post
            {
                PostDate = DateTime.Now,
                UserId = user.Id,
                privacy = postModel.privacyValue,
                ImageData = imageData,
                postAddedTime = FormatTime.FormatingTime(DateTime.Now),
                TextContent = postModel.postContent,
                PostImage = imageUrl,
                User = user
            };

            context.Posts.Add(post);
            return post;
        }


        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetLatestPosts(int pageIndex, int pageSize)
        {
            int postsToSkip = pageIndex * pageSize;

            List<Post> latestPosts = context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.PostDate)
                .Skip(postsToSkip)
                .Take(pageSize)
                .Select(p => new Post
                {
                    postAddedTime = FormatTime.FormatingTime(p.PostDate),
                    CommentCount = p.CommentCount,
                    //Comments = p.Comments,
                    ViewsCount = p.ViewsCount,
                    DislikeCount = p.DislikeCount,
                    LikeCount = p.LikeCount,
                    RepostCount = p.RepostCount,
                    TextContent = p.TextContent,
                    Id = p.Id,
                    PostDate = p.PostDate,
                    PostImage = p.PostImage,
                    User = p.User,
                    privacy = p.privacy,
                })
                .ToList();

            foreach (var post in latestPosts)
            {
                post.postAddedTime = FormatTime.FormatingTime(post.PostDate);
            }

            return latestPosts;
        }


        public List<Post> GetLatestPostsByUserId(string id, int pageIndex, int pageSize)
        {
            int postsToSkip = pageIndex * pageSize;

            List<Post> latestPosts = context.Posts
                .Include(p => p.User)
                .Where(p => p.UserId == id)
                .OrderByDescending(p => p.PostDate)
                .Skip(postsToSkip)
                .Take(pageSize)
                .Select(p => new Post
                {
                    postAddedTime = FormatTime.FormatingTime(p.PostDate),
                    CommentCount = p.CommentCount,
                    Comments = p.Comments,
                    ViewsCount = p.ViewsCount,
                    DislikeCount = p.DislikeCount,
                    LikeCount = p.LikeCount,
                    RepostCount = p.RepostCount,
                    TextContent = p.TextContent,
                    Id = p.Id,
                    PostDate = p.PostDate,
                    PostImage = p.PostImage,
                    User = p.User,
                    privacy = p.privacy,
                })
                .ToList();

            foreach (var post in latestPosts)
            {
                post.postAddedTime = FormatTime.FormatingTime(post.PostDate);
            }

            return latestPosts;
        }



        public Post? GetPostById(int id)

        {
            return context.Posts.Include(p => p.User)
                  .Include(p => p.Reactions)
                  //.Include(p => p.Comments)
                  .Include(p => p.Views)
                  .FirstOrDefault(p => p.Id == id);
        }

        public List<Reaction> GetReactionsByPostID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetUserPostCount(string userId)
        {
            return  context.Posts.Count(p=>p.UserId == userId);
            
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<int> UpdatePost(Post post)
        {
            context.Posts.Update(post);
            return await context.SaveChangesAsync();
        }
    }


}
