using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class BookMarkRepository : IBookMrakRepository
    {
        private readonly ApplicationContext context;

        public BookMarkRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<int> AddBookMark(BookMark bookMark)
        {
            context.bookMarks.Add(bookMark);
            return await context.SaveChangesAsync();
        }

        public List<BookMark> GetAllBookMarkList(String userId)
        {
           return context.bookMarks.Where(b => b.UserID == userId).ToList(); 
            
        }
    }
}
