using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface ISuggestesUsersService
    {
        List<ApplicationUser> GetSuggestesUsers(string userId);
    }
}
