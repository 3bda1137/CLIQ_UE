using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public Post CreatePost(CreatePostViewModel post, ApplicationUser user)

        {
            return postRepository.CreatePost(post, user);
        }
        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public void AddView(View view)
        {
            throw new NotImplementedException();
        }


        public void DeletePost(int id)
        {
            postRepository.DeletePost(id);
        }

        public List<Post> GetLatestPosts(int pageIndex, int pageSize, string UserId)
        {
            return postRepository.GetLatestPosts(pageIndex, pageSize, UserId);
        }


        public Post GetPostById(int id)
        {
            return postRepository.GetPostById(id);
        }

        public List<Reaction> GetReactionsByPostID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            postRepository.Save();
        }

        public async Task<int> UpdatePost(Post post)
        {
            return await postRepository.UpdatePost(post);
        }


        public List<Post> GetLatestPostsByUserId(string id, string CurrentId, int pageIndex, int pageSize)
        {
            return postRepository.GetLatestPostsByUserId(id,CurrentId, pageIndex, pageSize);
        }

        public List<string> allPostsImagesById(string id, string CurrentId)
        {
            return postRepository.allPostsImagesById(id,CurrentId);
        }
        public Task<int> IncreasePostComments(int postId)
        {
            Post Post = postRepository.GetPostById(postId);
            Post.CommentCount += 1;
            return postRepository.UpdatePost(Post);

        }

        public int GetUserPostCount(string userId)
        {
            return postRepository.GetUserPostCount(userId);
        }

        public void LoadFollowingId(string userId)
        {
            postRepository.LoadFollowingId(userId);
        }
    }
}
