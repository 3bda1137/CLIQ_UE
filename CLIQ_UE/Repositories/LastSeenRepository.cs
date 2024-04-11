using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class LastSeenRepository : ILastSeenRepository
    {
        private readonly ApplicationContext context;

        public LastSeenRepository( ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(LastSeen lastSeen)
        {
            context.LastSeens.Add(lastSeen);
            context.SaveChanges();
        }

        public void Delete(LastSeen lastSeen)
        {
            context.LastSeens.Remove(lastSeen);
            context.SaveChanges();
        }

        public LastSeen GetByUserId(string userId)
        {
            LastSeen lastSeen=context.LastSeens.FirstOrDefault(e=>e.UserID==userId);
            return lastSeen;
        }

        public void Update(LastSeen lastSeen)
        {
            context.LastSeens.Update(lastSeen);
           context.SaveChanges();
        }
    }
}
