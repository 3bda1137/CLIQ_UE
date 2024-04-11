using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface ISuggestesUsersRepository
    {
        List<ApplicationUser> GetSuggestesUsers(string userId);
    }
}
