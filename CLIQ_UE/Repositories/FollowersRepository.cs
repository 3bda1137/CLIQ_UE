﻿using CLIQ_UE.Models;

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
            var existingFollower = context.Followers.FirstOrDefault(f => f.FollowerId == follower.FollowerId && f.FollowingId == follower.FollowingId);
            if (existingFollower != null)
            {
                context.Followers.Remove(existingFollower);
                context.SaveChanges();
            }
        }


        public List<Followers> GetAllByFollowingId(string followingId)
        {
            List<Followers> followers = context.Followers
                .Where(f => f.FollowerId == followingId)
                .ToList();
            return followers;
        }

        public List<Followers> GetAllBySeachWords(string search, string followingId)
        {
            List<Followers> followers = context.Followers
                .Where(f => f.FollowingName.Contains(search) && f.FollowerId == followingId)
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

        public bool IsUserFollowing(string followerId, string followingId)
        {
            var followRelationship = context.Followers
                                            .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);

            return followRelationship != null;
        }

        public int GetFollowingCount(string followingIdId)
        {
            return context.Followers.Count(f => f.FollowingId == followingIdId);
        }

        public int GetFollowerCount(string followerId)
        {
            return context.Followers.Count(f => f.FollowerId == followerId);
        }


        public int GetMutualFollowersCount(string userId1, string userId2)
        {
            var followersUserId1 = GetFollowersIds(userId1);
            var followersUserId2 = GetFollowersIds(userId2);

            var mutualFollowersCount = followersUserId1.Intersect(followersUserId2).Count();

            return mutualFollowersCount;
        }
        public List<string> GetFollowersIds(string userid)
        {
            List<string> ids = context.Followers
                .Where(f => f.FollowerId == userid)
                .Select(f => f.FollowingId)
                .ToList();
            return ids;
        }

        public List<string> GetFollowingIds(string userid)
        {
            List<string> ids = context.Followers
                .Where(f => f.FollowingId == userid)
                .Select(f => f.FollowerId)
                .ToList();
            return ids;
        }

        public List<string> GetAllUsersToFollow(string userid)
        {
            List<string> followingIds = GetFollowingIds(userid);
            followingIds.Add(userid);

            List<string> allUserIds = context.Users.Select(u => u.Id).ToList();

            List<string> usersToFollowIds = allUserIds.Except(followingIds).ToList();

            return usersToFollowIds;
        }
    }
}
