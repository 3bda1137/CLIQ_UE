using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ILastMessageRepository
    {

        public void Add(LastMessage lastMessage);
        public LastMessage Get(string senderId, string ReseverId);
        public void Update(LastMessage lastMessage);
        public void Delete(LastMessage lastMessage);
    }
}
