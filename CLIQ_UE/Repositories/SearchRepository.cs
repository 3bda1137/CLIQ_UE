using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class SearchRepository:ISearchRepository
    {
        private readonly ApplicationContext context;
        public SearchRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<string> usersResultId(string str)
        {
            List<string> usersId = context.Users.Where(u=>u.FirstName.Contains(str) || u.LastName.Contains(str)).Select(u => u.Id).ToList();
            return usersId;
        }
    }
}
