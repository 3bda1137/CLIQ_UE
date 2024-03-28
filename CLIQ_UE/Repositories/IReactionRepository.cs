using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IReactionRepository
    {
        void AddReaction(Reaction reaction);
        List<Reaction> GetReactionsByPost(int postId);
    }
}
