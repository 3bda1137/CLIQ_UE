using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IUserRepository
    {

        public void Update(ApplicationUser appUser);
        public ApplicationUser GetById(string appUserId);
        public ApplicationUser GetByUserName(string appUserId);
        public ApplicationUser GetByName(string FirstName);


    }
}
