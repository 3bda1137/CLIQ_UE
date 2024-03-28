using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private readonly ApplicationContext context;

        public ViewRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void AddView(View view)
        {
            throw new NotImplementedException();
        }

        public List<View> GetViewsByPost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
