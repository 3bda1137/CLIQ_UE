using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IBookMrakRepository
    {
        void AddBookMark(BookMark bookMark);
        List<BookMark> GetAllBookMarkList(String userId);
    }
}
