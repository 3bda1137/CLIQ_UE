using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IPostService
    {
        Post GetPostById(int id);
        List<Post> GetLatestPosts(int pageIndex, int pageSize, string UserId);
        Post CreatePost(CreatePostViewModel post, ApplicationUser user);
        List<Post> GetLatestPostsByUserId(string id, string CurrentId, int pageIndex, int pageSize);

        Task<int> UpdatePost(Post post);
        void DeletePost(int id);
        void Save();

        void AddReaction(Reaction reaction);
        void AddView(View view);
        List<Reaction> GetReactionsByPostID(int id);

        public List<string> allPostsImagesById(string id, string CurrentId);

        int GetUserPostCount(string userId);



        Task<int> IncreasePostComments(int postId);
        void LoadFollowingId(string userId);
    }
}
