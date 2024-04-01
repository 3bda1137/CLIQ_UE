using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationContext context;

        public UserServices(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.context = context;
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
            applicationUser.PersonalImage = "./images/defualtImage.jpg";
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

        //public ApplicationUser GetUserById(string id)
        //{
        //    return context.Users.FirstOrDefault(u => u.Id == id);
        //}
    }
}
