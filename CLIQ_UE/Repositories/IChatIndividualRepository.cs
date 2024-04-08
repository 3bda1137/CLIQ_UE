using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
	public interface IChatIndividualRepository
	{
		public void AddMessageToChat(ChatIndividual chatIndividual);
		public void RemoveMessageFromChat(ChatIndividual chatIndividual);
		public List<ChatIndividual> GetChat(string userId,string otherUserId);
	}
}
