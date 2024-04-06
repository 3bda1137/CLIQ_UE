using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IProfileRepository
    {
        Post GetPostById(int id);
        List<Post> GetLatestPosts(int pageIndex, int pageSize);


        void Save();

    }
}
