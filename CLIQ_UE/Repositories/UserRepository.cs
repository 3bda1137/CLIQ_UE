using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext _context)
        {
            context = _context;
        }

        public ApplicationUser GetById(string appUserId)
        {
            ApplicationUser user = context.ApplicationUsers.Find(appUserId);
            return user;
        }

        public ApplicationUser GetByUserName(string userName)
        {
            return context.ApplicationUsers
                .FirstOrDefault(user => user.UserName == userName);
        }
        public ApplicationUser GetByName(string FirstName)
        {
            return context.ApplicationUsers
                .FirstOrDefault(user => user.FirstName == FirstName);
        }
        public void Update(ApplicationUser appUser)
        {
            context.ApplicationUsers.Update(appUser);
            context.SaveChanges();
        }
    }
}
