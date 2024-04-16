using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ISearchRepository
    {

        List<ApplicationUser> GetAllUsers(); 
        List<ApplicationUser> SearchUsers(string searchTerm);
        List<ApplicationUser> GetSuggestions(string term);

    }
}
