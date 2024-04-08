using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IBookMrakRepository
    {
        Task<int> AddBookMark(BookMark bookMark);
        List<BookMark> GetAllBookMarkList(String userId);
    }
}
