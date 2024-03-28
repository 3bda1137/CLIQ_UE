using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class ViewService : IViewService
    {
        private readonly IViewRepository viewRepository;

        public ViewService(IViewRepository viewRepository)
        {
            this.viewRepository = viewRepository;
        }
        public void AddView(View view)
        {
            viewRepository.AddView(view);
        }

        public List<View> GetViewsByPost(int postId)
        {
            return viewRepository.GetViewsByPost(postId);
        }
    }
}
