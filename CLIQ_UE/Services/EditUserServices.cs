using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;

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

        public void Update(EditBioAndUploadImageViewModel userViewModel, string userId)
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
    }
}
