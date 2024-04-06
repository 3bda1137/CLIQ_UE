using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class LastMessageServices : ILastMessageServices
    {
        private readonly ILastMessageRepository lastMessageRepository;

        public LastMessageServices(ILastMessageRepository lastMessageRepository)
        {
            this.lastMessageRepository = lastMessageRepository;
        }
        public void Add(LastMessage lastMessage)
        {
            lastMessageRepository.Add(lastMessage);
        }

        public void Delete(LastMessage lastMessage)
        {
            lastMessageRepository.Delete(lastMessage);
        }

        public LastMessage Get(string senderId, string ReseverId)
        {
           LastMessage lastMessage = lastMessageRepository.Get(senderId, ReseverId);
            return lastMessage;
        }

        public void Update(LastMessage lastMessage)
        {
            lastMessageRepository.Update(lastMessage);
        }
    }
}
