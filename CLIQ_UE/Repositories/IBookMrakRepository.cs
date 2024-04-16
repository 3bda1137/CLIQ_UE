using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IBookMrakRepository
    {
        void AddBookMark(BookMark bookMark);
        void removeBookmark(int postId, string userId);
        List<BookMark> GetAllBookMarkList(String userId);
        bool isBookmarked(int postId, string userId);
        List<int> getAllPostsId(string userId);
    }
}
