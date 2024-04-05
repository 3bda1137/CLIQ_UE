using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IUserLikeCommentRepository
    {
        void LikeComment(UserLikeComment like);
        void UnLikeComment(UserLikeComment like);
        UserLikeComment? Get(UserLikeComment userLikeComment);
        //List<UserLikeComment> GetCurUserLikeComments(string UID);
    }
}
