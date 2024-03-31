using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Services
{
    public class EditUserServices : IEditUserServices
    {
        private readonly IEditUserRepository editUserRepository;

        public EditUserServices(IEditUserRepository editUserRepository)
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

        public void UpdateBio(EditBioAndUploadImageViewModel userViewModel, string userId)
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
                user.ProfileImage = userViewModel.ProfileImageName;
                user.PublicBirthDate = userViewModel.PublicBirthDate;

                editUserRepository.Update(user);
            }
        }

        public void UpdateProfile(EditProfileViewModel userViewModel, string userId)
        {
            ApplicationUser user = GetById(userId);
            if (user != null)
            {
                user.Country = userViewModel.Country;
                user.Language = userViewModel.Language;
                user.Email = userViewModel.Email;
                user.BirthDate = userViewModel.BirthDate;
                user.Gender = userViewModel.Gender;
                user.UserName = userViewModel.UserName;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                if (!string.IsNullOrEmpty(userViewModel.NewPassword))
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHasher.HashPassword(user, userViewModel.NewPassword);
                }

                editUserRepository.Update(user);
            }
        }
    }
}
