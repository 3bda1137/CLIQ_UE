using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface ILastMessageServices
    {
        public void Add(LastMessage lastMessage);
        public LastMessage Get(string senderId, string ReseverId);
        public void Update(LastMessage lastMessage);
        public void Delete(LastMessage lastMessage);
    }
}
