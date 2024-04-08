using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IFollowersRepository
    {
        public void Add(Followers follower);
        public Followers GetByFollowerId(string followingId, string followerId);
        public List<Followers> GetAllByFollowingId(string followingId);
        public void Update(Followers follower);
        public void UnFollow(Followers follower);

    }
}
