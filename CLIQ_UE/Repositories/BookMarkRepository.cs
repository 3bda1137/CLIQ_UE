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
            List<BookMark> bm= context.bookMarks
                .Where(b => b.UserID == userId)
                .Include(p => p.Posts)
                .ToList(); 
            
            return bm;
        }

      

        void IBookMrakRepository.AddBookMark(BookMark bookMark)
        {
            context.bookMarks.Add(bookMark);
            context.SaveChanges();
        }
    }
}
