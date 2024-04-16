using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IBookMarkService
    {
        void AddBookMark(BookMark BookMarkVM);
        void removeBookmark(int postId, string userId);
        List<BookMark> GetAllBookMark(String UserID);
        bool isBookmarked(int postId, string userId);
        List<int> getAllPostsId(string userId);

    }
}
