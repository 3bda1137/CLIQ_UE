using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository reactionRepository;

        public ReactionService(IReactionRepository reactionRepository)
        {
            this.reactionRepository = reactionRepository;
        }

        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public List<Reaction> GetReactionsByPost(int postId)
        {
            return reactionRepository.GetReactionsByPost(postId);
        }
    }
}
