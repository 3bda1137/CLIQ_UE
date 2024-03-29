namespace CLIQ_UE.Repositories
{
	public interface IChatIndividualRepository
	{
		public void AddMessageToChat(ChatIndividualRepository chatIndividual);
		public void RemoveMessageFromChat(ChatIndividualRepository chatIndividual);
		public List<ChatIndividualRepository> GetChatIndividual(int userId);
	}
}
