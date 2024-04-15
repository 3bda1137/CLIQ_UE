using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class SuggestesUsersService : ISuggestesUsersService
    {
        private readonly ISuggestesUsersRepository suggestesUsersRepository;

        public SuggestesUsersService(ISuggestesUsersRepository suggestesUsersRepository)
        {
            this.suggestesUsersRepository = suggestesUsersRepository;
        }
        public List<ApplicationUser> GetSuggestesUsers(string userId)
        {
            return suggestesUsersRepository.GetSuggestesUsers(userId);
        }
    }
}
