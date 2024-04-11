using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class ChatIndividualServices : IChatIndividualServices
    {
        private readonly IChatIndividualRepository chatIndividualRepository;

        public ChatIndividualServices(IChatIndividualRepository chatIndividualRepository)
        {
            this.chatIndividualRepository = chatIndividualRepository;
        }

        public void AddMessageToChat(string message,string currentId, string otherUserId)
        {
            DateTime currentTime = DateTime.Now;
            ChatIndividual chatIndividual = new ChatIndividual();
            chatIndividual.SenderId = currentId;
            chatIndividual.ReceiverId = otherUserId;
            chatIndividual.MessageContant = message;
            chatIndividual.CreatedAt = DateTime.Now;
            chatIndividual.Time = currentTime.ToString("HH:mm tt");
            chatIndividualRepository.AddMessageToChat(chatIndividual);
        }

        public List<ChatIndividual> GetChat(string userId, string otherUserId)
        {
            List<ChatIndividual> chat = chatIndividualRepository
                .GetChat(userId, otherUserId);
            return chat;
        }

        public ChatIndividual GetOneMessage(string userId, string otherUserId)
        {
            ChatIndividual message= chatIndividualRepository.GetOneMessage(userId, otherUserId);
            return message;
        }

        public void RemoveMessageFromChat(ChatIndividual chatIndividual)
        {
            chatIndividualRepository.RemoveMessageFromChat(chatIndividual);
        }
    }
}
