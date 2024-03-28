using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IReactionService
    {
        void AddReaction(Reaction reaction);
        List<Reaction> GetReactionsByPost(int postId);
    }
}
