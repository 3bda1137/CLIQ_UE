using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IPostService
    {
        Post GetPostById(int id);
        List<Post> GetLatestPosts(int pageIndex, int pageSize);
        Post CreatePost(CreatePostViewModel post, ApplicationUser user);


        void UpdatePost(Post post);
        void DeletePost(int id);
        void Save();

        void AddReaction(Reaction reaction);
        void AddView(View view);
        List<Reaction> GetReactionsByPostID(int id);
    }
}
