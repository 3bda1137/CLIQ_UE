using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
	public interface IChatIndividualServices
	{

        public void AddMessageToChat(string message, string currentId, string otherUserId);
        public void RemoveMessageFromChat(ChatIndividual chatIndividual);
        public List<ChatIndividual> GetChat(string userId, string otherUserId);
        public ChatIndividual GetOneMessage(string userId, string otherUserId);
    }
}
