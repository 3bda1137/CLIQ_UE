using CLIQ_UE.Helpers;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CLIQ_UE.Services
{
    public class FollowersServices : IFollowersServices
    {
        private readonly IFollowersRepository followersRepository;
        private readonly ILastMessageServices lastMessageServices;
        private readonly IUserServices userServices;

        public FollowersServices(IFollowersRepository followersRepository
                                    , ILastMessageServices lastMessageServices
                                    , IUserServices userServices)
        {
            this.followersRepository = followersRepository;
            this.lastMessageServices = lastMessageServices;
            this.userServices = userServices;
        }
        public void Add(Followers follower)
        {
            followersRepository.Add(follower);
        }

        public void UnFollow(Followers follower)
        {
            followersRepository.UnFollow(follower);
        }

        public List<UserConntactViewModel> GetAllByFollowingId(string followerId)
        {
            List<Followers> followers = followersRepository.GetAllByFollowingId(followerId);
            List<UserConntactViewModel> userConntactVMWithLastMessage = new List<UserConntactViewModel>();
            List<UserConntactViewModel> userConntactVMWithOutLastMessage = new List<UserConntactViewModel>();
            foreach (Followers follower in followers)
            {
                UserConntactViewModel viewModel = new UserConntactViewModel();

                if (follower.FollowerId == followerId)//FollowerId ==me
                {
                    viewModel.UserId = follower.FollowingId;
                    viewModel.UserName = follower.FollowingName;
                    viewModel.ImageUrl = follower.ImageUrl;
                    viewModel.LastMessage = lastMessageServices.Get(followerId, follower.FollowingId);
                    if (viewModel.LastMessage == null)
                        userConntactVMWithOutLastMessage.Add(viewModel);
                    else
                    {
                        viewModel.FormatedTime = FormatTimeForChat.CalculateLastSeenForUserList(viewModel.LastMessage.Time.ToString("yyyy-MM-dd HH:mm"));
                        userConntactVMWithLastMessage.Add(viewModel);
                    }
                }
            }
            userConntactVMWithLastMessage= userConntactVMWithLastMessage
                        .Where(e => e.LastMessage != null)
                        .OrderByDescending(e => e.LastMessage.Time)
                        .ToList();
            userConntactVMWithLastMessage.AddRange(userConntactVMWithOutLastMessage);
            return userConntactVMWithLastMessage;
        }

        public List<UserConntactViewModel> GetAllBySeachWords(string searchword, string followingId)

        {
            List<Followers> followers = followersRepository.GetAllBySeachWords(searchword, followingId);
            List<UserConntactViewModel> userConntactViewModel = new List<UserConntactViewModel>();
            foreach (Followers follower in followers)
            {
                UserConntactViewModel viewModel = new UserConntactViewModel();

                if (follower.FollowerId == followingId)//FollowingId ==me
                {
                    //ApplicationUser user = userServices.GetByID(follower.FollowerId);
                    viewModel.UserId = follower.FollowingId;
                    viewModel.UserName = follower.FollowingName;
                    viewModel.ImageUrl = follower.ImageUrl;
                    viewModel.LastMessage = lastMessageServices.Get(followingId, follower.FollowingId);
                   viewModel.FormatedTime= FormatTimeForChat.CalculateLastSeenForUserList(viewModel.LastMessage.Time.ToString("yyyy-MM-dd HH:mm"));

                }

                userConntactViewModel.Add(viewModel);
            }
            return userConntactViewModel;
        }

        public Followers GetByFollowerId(string followingId, string followerId)
        {
            Followers follower = followersRepository.GetByFollowerId(followingId, followerId);
            return follower;
        }

        public void Update(Followers follower)
        {
            followersRepository.Update(follower);
        }

        public bool IsUserFollowing(string followerId, string followingId)
        {
            return followersRepository.IsUserFollowing(followerId, followingId);
        }

        public int GetFollowingCount(string followerId)
        {
            return followersRepository.GetFollowingCount(followerId);
        }
        public int GetFollowerCount(string followingId)
        {
            return followersRepository.GetFollowerCount(followingId);
        }

        public List<string> GetFollowersIds(string userid)
        {
            return followersRepository.GetFollowersIds(userid);
        }

        public List<string> GetFollowingIds(string userid)
        {
            return followersRepository.GetFollowingIds(userid);
        }

        public List<string> GetAllUsersToFollow(string userid)
        {
            return followersRepository.GetAllUsersToFollow(userid);
        }
    }
}
