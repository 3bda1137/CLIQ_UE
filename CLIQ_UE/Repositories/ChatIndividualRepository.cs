

using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class ChatIndividualRepository : IChatIndividualRepository
    {
        private readonly ApplicationContext context;

        public ChatIndividualRepository( ApplicationContext context)
        {
            this.context = context;
        }
        public void AddMessageToChat(ChatIndividual chatIndividual)
        {
            context.ChatIndividual.Add(chatIndividual);
            context.SaveChanges();
        }

        public List<ChatIndividual> GetChat(string userId, string otherUserId)
        {
            List<ChatIndividual> chat= context.ChatIndividual
                .Where(c=>(c.ReceiverId == userId&&c.SenderId==otherUserId)
                          || c.ReceiverId == otherUserId && c.SenderId == userId)
                .ToList();
            return chat;
        }

        public void RemoveMessageFromChat(ChatIndividual chatIndividual)
        {
            context.ChatIndividual.Remove(chatIndividual);
            context.SaveChanges();
        }
    }
}
