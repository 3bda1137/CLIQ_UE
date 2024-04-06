using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IUserLikeCommentService
    {
        Task<bool> LikeComment(UserLikeComment like);
        //bool UnLikeComment(UserLikeComment like);
    }
}
