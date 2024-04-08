using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;

        public UserServices(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public CompleteProfileViewModel MapAppUserToViewModel(ApplicationUser applicationUser)
        {
            CompleteProfileViewModel completeProfileViewModel = new CompleteProfileViewModel();
            completeProfileViewModel.Bio = applicationUser.Bio;
            completeProfileViewModel.FirstName = applicationUser.FirstName;
            completeProfileViewModel.LastName = applicationUser.LastName;
            completeProfileViewModel.Location = applicationUser.Location;
            completeProfileViewModel.UserName = applicationUser.UserName;
            completeProfileViewModel.PhoneNum = applicationUser.PhoneNumber;
            return completeProfileViewModel;
        }

        public ApplicationUser MapRegisterViewModelToAppUser(RegisterViewModel viewModel)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Email = viewModel.Email;
            applicationUser.PasswordHash = viewModel.Password;
            applicationUser.PersonalImage = "./images/Man-Avatar.png";
            //??????
            applicationUser.UserName = GenerateUsernameFromEmail(viewModel.Email);
            return applicationUser;
        }

        private string GenerateUsernameFromEmail(string email)
        {
            int atIndex = email.IndexOf('@');
            string username;
            if (atIndex >= 0)
            {
                username = email.Substring(0, atIndex);
            }
            else
            {
                username = email;
            }
            return username;
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            return userRepository.GetByUserName(userName);
        }

        public ApplicationUser GetByID(string userId)
        {
            return userRepository.GetById(userId);
        }
    }
}
