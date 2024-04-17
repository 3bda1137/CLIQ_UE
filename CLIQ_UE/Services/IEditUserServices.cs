using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IEditUserServices
    {
        public void CompleteProfile(CompleteProfileViewModel userViewModel, string userId);
        public void UpdateProfile(ChangeInfoOfUserViewModel changeInfoOfUserViewModel, string userId);
        public ApplicationUser GetById(string appUserId); 
        public void UpdateUser(ApplicationUser applicationUser);
        public EditProfileViewModel GetUserViewModelById(string userId);

    }
}
