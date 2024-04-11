using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ILastSeenRepository
    {
        public void Add(LastSeen lastSeen);
        public void Update(LastSeen lastSeen);
        public void Delete(LastSeen lastSeen);
        public LastSeen GetByUserId(string userId);
    }
}
