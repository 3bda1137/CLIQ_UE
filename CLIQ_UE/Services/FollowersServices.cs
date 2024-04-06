using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using System.Collections.Generic;

namespace CLIQ_UE.Services
{
    public class FollowersServices : IFollowersServices
    {
        private readonly IFollowersRepository followersRepository;
        private readonly ILastMessageServices lastMessageServices;
        private readonly IUserServices userServices;

        public FollowersServices(IFollowersRepository followersRepository 
                                    ,ILastMessageServices lastMessageServices
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

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<UserConntactViewModel> GetAllByFollowingId(string followingId)
        {
            List<Followers> followers=followersRepository.GetAllByFollowingId(followingId);
            List<UserConntactViewModel> userConntactViewModel = new List<UserConntactViewModel>();
            foreach (Followers follower in followers)
            {
                UserConntactViewModel viewModel = new UserConntactViewModel();
             
                if (follower.FollowingId == followingId)//FollowingId ==me
                {
                    ApplicationUser user = userServices.GetByID(follower.FollowerId);
                    viewModel.UserId = user.Id;
                    viewModel.UserName=user.FirstName+" "+user.LastName;
                    viewModel.ImageUrl = user.PersonalImage;
                    viewModel.LastMessage = lastMessageServices.Get(followingId, user.Id);
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
    }
}
