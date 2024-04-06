using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IUserLikeCommentRepository
    {
        Task<int> LikeComment(UserLikeComment like);
        Task<int> UnLikeComment(UserLikeComment like);
        UserLikeComment? Get(UserLikeComment userLikeComment);
        //List<UserLikeComment> GetCurUserLikeComments(string UID);
    }
}
