using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IUserLikePostService
    {
        UserLikePost? Get(int postId, string UID);
        UserLikePost Update(UserLikePost user);
        UserLikePost Add(UserLikePost user);
        void Delete(UserLikePost likePost);
    }
}
