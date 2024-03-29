using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Repositories
{
    public interface IPostRepository
    {
        Post GetPostById(int id);
        List<Post> GetLatestPosts();
        void CreatePost(CreatePostViewModel post, ApplicationUser user);

        void UpdatePost(Post post);
        void DeletePost(int id);

        void Save();

        void AddReaction(Reaction reaction);
        void AddView(View view);
        List<Reaction> GetReactionsByPostID(int id);
    }
}
