using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class FollowersRepository : IFollowersRepository
    {
        private readonly ApplicationContext context;

        public FollowersRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void Add(Followers follower)
        {
            var followerId = follower.FollowerId;
            var followingId = follower.FollowingId;

            bool isFollowingEachOther = context.Followers.Any(f => f.FollowerId == followerId && f.FollowingId == followingId);
            if (!isFollowingEachOther)
            {
                context.Followers.Add(follower);
                context.SaveChanges();
            }

        }

        public void UnFollow(Followers follower)
        {
            var followerId = follower.FollowerId;
            var followingId = follower.FollowingId;

            bool isFollowingEachOther = context.Followers.Any(f => f.FollowerId == followerId && f.FollowingId == followingId);
            if (isFollowingEachOther)
            {
                context.Followers.Remove(follower);
                context.SaveChanges();
            }

        }

        public List<Followers> GetAllByFollowingId(string followingId)
        {
            List<Followers> followers = context.Followers
                .Where(f => f.FollowingId == followingId)
                .ToList();
            return followers;
        }

        public List<Followers> GetAllBySeachWords(string search,string followingId)
        {
            List<Followers> followers = context.Followers
                .Where(f => f.FollowerName.Contains(search) && f.FollowingId==followingId)
                .ToList();
            return followers;
        }

        public Followers GetByFollowerId(string followingId, string followerId)
        {
            Followers follower = context.Followers
                .FirstOrDefault(f => f.FollowingId == followingId && f.FollowerId == followerId);

            return follower;
        }

        public void Update(Followers follower)
        {
            context.Followers.Update(follower);
            context.SaveChanges();
        }
    }
}
