using CLIQ_UE.Models;
using Microsoft.AspNetCore.Identity;

namespace CLIQ_UE.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public SearchRepository(ApplicationContext _context , UserManager<ApplicationUser> userManager) 
        {
            this.context = _context;
            this.userManager = userManager;
        }

        public List<ApplicationUser> GetAllUsers()
        {

                List<ApplicationUser> users = context.Users.ToList();
                return users;

        }

        public List<ApplicationUser> SearchUsers(string searchTerm)
        {
            //List<ApplicationUser> users = context.Users
            //                            .Where(u => u.FirstName.Contains(searchTerm))
            //                            .ToList();
            List<ApplicationUser> users = context.Users
                      .Where(u => u.FirstName.Contains(searchTerm.Trim()))
                      .ToList();
            return users;
        }
        public List<ApplicationUser> GetSuggestions(string term)
        {
            return context.Users
                .Where(u => u.FirstName.Contains(term))
                .ToList();
        }
    }
}
