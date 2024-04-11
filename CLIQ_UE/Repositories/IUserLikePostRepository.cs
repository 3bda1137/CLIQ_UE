using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IUserLikePostRepository
    {
        UserLikePost? Get(int postId, string UID);
        UserLikePost Update(UserLikePost user);
        UserLikePost Add(UserLikePost user);
        void Delete(UserLikePost likePost);
    }
}
