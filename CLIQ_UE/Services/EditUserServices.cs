using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Services
{
    public class EditUserServices : IEditUserServices
    {
        private readonly IUserRepository editUserRepository;

        public EditUserServices(IUserRepository editUserRepository)
        {
            this.editUserRepository = editUserRepository;
        }

        public ApplicationUser GetById(string appUserId)
        {
            ApplicationUser applicationUser = editUserRepository.GetById(appUserId);
            return applicationUser;
        }

        public EditProfileViewModel GetUserViewModelById(string userId)
        {
            ApplicationUser user = GetById(userId);
            EditProfileViewModel editProfileViewModel = new EditProfileViewModel();
            editProfileViewModel.Gender = user.Gender;
            editProfileViewModel.UserName = user.UserName;
            editProfileViewModel.Email = user.Email;
            editProfileViewModel.BirthDate = user.BirthDate;
            editProfileViewModel.Country = user.Country;
            editProfileViewModel.Language = user.Language;
            return editProfileViewModel;
        }

        public void CompleteProfile(CompleteProfileViewModel userViewModel, string userId)
        {
            ApplicationUser user = GetById(userId);
            if (user != null)
            {
                user.Bio = userViewModel.Bio;
                user.Location = userViewModel.Location;
                user.BirthDate = userViewModel.DateOfBirth;
                user.PhoneNumber = userViewModel.PhoneNum;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Gender = userViewModel.Gender;
                user.Language=userViewModel.Language;
                if(userViewModel.Gender == "female") {
                    user.PersonalImage = "/images/defaultimages/Women-avatar.png";
                }
                else
                {
                    user.PersonalImage = "/images/defaultimages/man-avatar.png";
                }
                user.ProfileImage= "/images/defaultimages/cover.jpg";
                user.PublicBirthDate = userViewModel.PublicBirthDate;

                editUserRepository.Update(user);
            }
        }

        public void UpdateProfile(ChangeInfoOfUserViewModel changeInfoOfUserViewModel, string userId)
        {
            ApplicationUser user = GetById(userId);
            if (user != null)
            {
                user.Country = changeInfoOfUserViewModel.Country;
                user.Language = changeInfoOfUserViewModel.Language;
                user.BirthDate = changeInfoOfUserViewModel.BirthDate;
                user.Gender = changeInfoOfUserViewModel.Gender;
                user.FirstName = changeInfoOfUserViewModel.FirstName;
                user.LastName = changeInfoOfUserViewModel.LastName;
                editUserRepository.Update(user);
            }
        }

        public void UpdateUser(ApplicationUser applicationUser)
        {
            editUserRepository.Update(applicationUser);
        }
    }
}
