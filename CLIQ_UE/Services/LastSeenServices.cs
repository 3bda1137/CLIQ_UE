using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class LastSeenServices : ILastSeenServices
    {
        private readonly ILastSeenRepository lastSeenRepository;

        public LastSeenServices(ILastSeenRepository lastSeenRepository )
        {
            this.lastSeenRepository = lastSeenRepository;
        }
        public void Add(LastSeen lastSeen)
        {
            lastSeenRepository.Add(lastSeen);
        }

        public void Delete(LastSeen lastSeen)
        {
            lastSeenRepository.Delete(lastSeen);
        }

        public LastSeen GetByUserId(string userId)
        {
            LastSeen lastSeen = lastSeenRepository.GetByUserId(userId);
            return lastSeen;
        }

        public void Update(LastSeen lastSeen)
        {
            lastSeenRepository.Update(lastSeen);
        }
    }
}
