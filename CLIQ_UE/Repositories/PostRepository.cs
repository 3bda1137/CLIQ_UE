using CLIQ_UE.Helpers;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CLIQ_UE.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFollowersServices followersServices;
        private List<string> followingIds;
        public PostRepository(ApplicationContext _context, UserManager<ApplicationUser> userManager, IFollowersServices followersServices)
        {
            context = _context;
            this.userManager = userManager;
            this.followersServices = followersServices;

        }

        public void LoadFollowingId(string userId)
        {
            followingIds = followersServices.GetFollowersIds(userId);
            followingIds.Add(userId);
        }


        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public void AddView(View view)
        {
            throw new NotImplementedException();
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
                User = user,
                isDeleted = false,

            };

            context.Posts.Add(post);
            return post;
        }

        public void DeletePost(int id)
        {
            var post = context.Posts.Find(id);
            if (post != null)
            {
                post.isDeleted = true;
                context.SaveChanges();
            }
        }


        public List<Post> GetLatestPosts(int pageIndex, int pageSize, string UserId)
        {
            int postsToSkip = pageIndex * pageSize;

            List<Post> latestPosts = context.Posts
                .Include(p => p.User)
.Where(p => !p.isDeleted &&
            ((p.privacy == "public") ||
             (p.privacy == "private" && p.UserId == UserId) ||
           (p.privacy == "friends" && followingIds.Contains(p.UserId))))

                .OrderByDescending(p => p.PostDate)
                .Skip(postsToSkip)
                .Take(pageSize)
                .Include(p => p.UsersLikedPost.Where(ULP => ULP != null && ULP.ApplicationUserId == UserId))
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
                    isLikedByMe = p.UsersLikedPost.Count() > 0 && p.UsersLikedPost.First().isLiked ? true
                    : p.UsersLikedPost.Count() > 0 && !p.UsersLikedPost.First().isLiked ? false
                    : null
                })
                .ToList();

            foreach (var post in latestPosts)
            {
                post.postAddedTime = FormatTime.FormatingTime(post.PostDate);
            }

            return latestPosts;
        }


        public List<Post> GetLatestPostsByUserId(string id, string CurrentId, int pageIndex, int pageSize)
        {
            int postsToSkip = pageIndex * pageSize;

            followingIds = followersServices.GetFollowersIds(CurrentId);

            List<Post> latestPosts = context.Posts
       .Where(p => !p.isDeleted && p.User.Id == id &&
            ((p.privacy == "public") ||
             (p.privacy == "private" && p.User.Id == CurrentId) ||
                (p.privacy == "friends" && (followingIds.Contains(p.User.Id) || p.User.Id == CurrentId))))
                .Include(p => p.User)
                .OrderByDescending(p => p.PostDate)
                .Skip(postsToSkip)
                .Take(pageSize)
                .Include(p => p.UsersLikedPost.Where(ULP => ULP != null && ULP.ApplicationUserId == id))
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
                    isLikedByMe = p.UsersLikedPost != null && p.UsersLikedPost.Count() > 0 && p.UsersLikedPost.First().isLiked ? true
                    : p.UsersLikedPost != null && p.UsersLikedPost.Count() > 0 && !p.UsersLikedPost.First().isLiked ? false
                    : null
                })

                .ToList();

            foreach (var post in latestPosts)
            {
                post.postAddedTime = FormatTime.FormatingTime(post.PostDate);
            }

            return latestPosts;
        }

        public List<string> allPostsImagesById(string id, string CurrentId)
        {
            followingIds = followersServices.GetFollowersIds(CurrentId);

            List<string> images = context.Posts
             .Where(p => p.UserId == id && p.PostImage != null && !p.isDeleted &&
            ((p.privacy == "public") ||
             (p.privacy == "private" && p.User.Id == CurrentId) ||
                (p.privacy == "friends" && (followingIds.Contains(p.User.Id) || p.User.Id == CurrentId))))
             .OrderByDescending(p => p.PostDate)
             .Select(p => p.PostImage)
             .ToList();

            return images;
        }

        public Post? GetPostById(int id)

        {
            Post p = context.Posts
                .Include(p => p.User)
                .Include(p => p.Reactions)
                //.Include(p => p.Comments)
                .Include(p => p.Views)
            .FirstOrDefault(p => p.Id == id && !p.isDeleted);
            if (p == null) return null;
            p.postAddedTime = FormatTime.FormatingTime(p.PostDate);
            return p;

        }

        public List<Reaction> GetReactionsByPostID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetUserPostCount(string userId)
        {
            return context.Posts.Count(p => p.UserId == userId && !p.isDeleted);

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
