using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class LastMessageRepository : ILastMessageRepository
    {
        private readonly ApplicationContext context;

        public LastMessageRepository( ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(LastMessage lastMessage)
        {
            context.LastMessages.Add(lastMessage);
            context.SaveChanges();
        }

        public void Delete(LastMessage lastMessage)
        {
           context.LastMessages.Remove(lastMessage);
            context.SaveChanges();
        }

        public LastMessage Get(string senderId, string ReseverId)
        {
            LastMessage lastMessage = context.LastMessages
                .FirstOrDefault(lm => lm.SendId == senderId && lm.ReseverID == ReseverId);
            return lastMessage;
        }

        public void Update(LastMessage lastMessage)
        {
            context.LastMessages.Update(lastMessage);
            context.SaveChanges();
        }
    }
}
