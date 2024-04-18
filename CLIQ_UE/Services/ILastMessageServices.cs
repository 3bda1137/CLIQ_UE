using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface ILastMessageServices
    {
        public void Add(LastMessage lastMessage);
        public LastMessage Get(string senderId, string ReseverId);
        public void Update(LastMessage lastMessage);

        public void Delete(LastMessage lastMessage);
        //public List<UserConntactViewModel> GetAll(string userId);
    }
}
