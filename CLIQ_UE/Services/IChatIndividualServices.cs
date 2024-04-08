using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
	public interface IChatIndividualServices
	{

        public void AddMessageToChat(ChatIndividual chatIndividual);
        public void RemoveMessageFromChat(ChatIndividual chatIndividual);
        public List<ChatIndividual> GetChat(string userId, string otherUserId);
    }
}
