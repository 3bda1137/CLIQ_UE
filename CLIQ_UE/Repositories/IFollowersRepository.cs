using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IFollowersRepository
    {
        public void Add(Followers follower);
        public Followers GetByFollowerId(string followingId, string followerId);
        public List<Followers> GetAllByFollowingId(string followingId);
        public List<Followers> GetAllBySeachWords(string search, string followingId);
        public void Update(Followers follower);
        public void UnFollow(Followers follower);
        public bool IsUserFollowing(string followerId, string followingId);
        int GetFollowingCount(string followerId);
        int GetFollowerCount(string followingId);

        List<string> GetFollowersIds(string userid);
        List<string> GetFollowingIds(string userid);
        List<string> GetAllUsersToFollow(string userid);

        int GetMutualFollowersCount(string userId1, string userId2);

    }
}
