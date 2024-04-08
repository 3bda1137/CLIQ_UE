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

        public void AddMessageToChat(ChatIndividual chatIndividual)
        {
            chatIndividualRepository.AddMessageToChat(chatIndividual);
        }

        public List<ChatIndividual> GetChat(string userId, string otherUserId)
        {
            List<ChatIndividual> chat = chatIndividualRepository
                .GetChat(userId, otherUserId);
            return chat;
        }

        public void RemoveMessageFromChat(ChatIndividual chatIndividual)
        {
            chatIndividualRepository.RemoveMessageFromChat(chatIndividual);
        }
    }
}
