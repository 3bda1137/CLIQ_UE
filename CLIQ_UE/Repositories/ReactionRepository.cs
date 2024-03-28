using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApplicationContext context;

        public ReactionRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void AddReaction(Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public List<Reaction> GetReactionsByPost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
