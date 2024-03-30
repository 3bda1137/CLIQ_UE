using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class EditUserRepository : IEditUserRepository
    {
        private readonly ApplicationContext context;

        public EditUserRepository(ApplicationContext _context)
        {
            context = _context;
        }

        public ApplicationUser GetById(string appUserId)
        {
            ApplicationUser user = context.ApplicationUsers.Find(appUserId);
            return user;
        }

        public void Update(ApplicationUser appUser)
        {
            context.ApplicationUsers.Update(appUser);
            context.SaveChanges();
        }
    }
}
