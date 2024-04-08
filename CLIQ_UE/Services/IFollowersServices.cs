using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IFollowersServices
    {
        public void Add(Followers follower);
        public Followers GetByFollowerId(string followingId, string followerId);
        public List<UserConntactViewModel> GetAllByFollowingId(string followingId);
        public List<UserConntactViewModel> GetAllBySeachWords(string searchword,string followingId);
        public void Update(Followers follower);
        public void UnFollow(Followers follower);
    }
}
