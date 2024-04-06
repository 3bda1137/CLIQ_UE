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
            throw new NotImplementedException();
        }

        public List<Post> GetLatestPosts(int pageIndex, int pageSize)
        {
            return postRepository.GetLatestPosts(pageIndex, pageSize);
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

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }


        public List<Post> GetLatestPostsByUserId(string id, int pageIndex, int pageSize)
        {
            return postRepository.GetLatestPostsByUserId(id, pageIndex, pageSize);
        }

        public List<string> allPostsImagesById(string id)
        {
            return postRepository.allPostsImagesById(id);
}
        public Task<int> IncreasePostComments(int postId)
        {
            Post Post = postRepository.GetPostById(postId);
            Post.CommentCount += 1;
            return postRepository.UpdatePost(Post);

        }
    }
}
