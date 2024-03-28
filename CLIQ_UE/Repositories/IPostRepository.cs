using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IPostRepository
    {
        Post GetPostById(int id);
        List<Post> GetLatestPosts();
        void CreatePost(Post newPost);
        void UpdatePost(Post post);
        void DeletePost(int id);


        void AddReaction(Reaction reaction);
        void AddView(View view);
        List<Reaction> GetReactionsByPostID(int id);
    }
}
