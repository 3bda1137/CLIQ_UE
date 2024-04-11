using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface ILastSeenServices
    {
        public void Add(LastSeen lastSeen);
        public void Update(LastSeen lastSeen);
        public void Delete(LastSeen lastSeen);
        public LastSeen GetByUserId(string userId);
    }
}
