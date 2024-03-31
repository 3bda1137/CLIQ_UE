using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IEditUserServices
    {
        public void UpdateBio(EditBioAndUploadImageViewModel userViewModel, string userId);
        public void UpdateProfile(EditProfileViewModel userViewModel, string userId);
        public ApplicationUser GetById(string appUserId);
        public EditProfileViewModel GetUserViewModelById(string userId);

    }
}
