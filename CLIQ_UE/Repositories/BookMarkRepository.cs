using CLIQ_UE.Models;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{
    public class BookMarkRepository : IBookMrakRepository
    {
        private readonly ApplicationContext context;

        public BookMarkRepository(ApplicationContext context)
        {
            this.context = context;
        }
        //public async Task<int> AddBookMark(BookMark bookMark)
        //{
        //    context.bookMarks.Add(bookMark);
        //    return await context.SaveChangesAsync();
        //}

        public List<BookMark> GetAllBookMarkList(String userId)
        {
            List<BookMark> bm = context.bookMarks
                .Where(b => b.UserID == userId)
                .Include(p => p.Posts)
                .ToList();

            return bm;
        }

        public List<int> getAllPostsId(string userId)
        {
            List<int> savedPostIds = context.bookMarks
                                .Where(b => b.UserID == userId)
                                .Select(b => b.PostID)
                                .ToList();

            return savedPostIds;
        }

        public bool isBookmarked(int postId, string userId)
        {
            bool bookmarkExists = context.bookMarks.Any(b => b.PostID == postId && b.UserID == userId);
            return bookmarkExists;

        }

        public void removeBookmark(int postId, string userId)
        {
            var bookMark = context.bookMarks.FirstOrDefault(b => b.PostID == postId && b.UserID == userId);

            if (bookMark != null)
            {
                context.bookMarks.Remove(bookMark);
                context.SaveChanges();
            }
        }

        void IBookMrakRepository.AddBookMark(BookMark bookMark)
        {
            bool alreadyBookmarked = context.bookMarks.Any(b => b.PostID == bookMark.PostID && b.UserID == bookMark.UserID);
            if (!alreadyBookmarked)
            {
                context.bookMarks.Add(bookMark);
                context.SaveChanges();

            }
        }
    }
}
