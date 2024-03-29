using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IEditUserRepository
    {

        public void Update(ApplicationUser appUser);
        public ApplicationUser GetById(string appUserId);


    }
}
