using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{

  

    public class SearchService:ISearchService
    {
        private readonly ISearchRepository searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }
        public   List<string> usersResultId(string str)
        {
           return searchRepository.usersResultId(str);

        }
    }
}
