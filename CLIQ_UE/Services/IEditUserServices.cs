using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
    public interface IEditUserServices
    {
        public void Update(EditBioAndUploadImageViewModel userViewModel, string userId);
        public ApplicationUser GetById(string appUserId);
    }
}
