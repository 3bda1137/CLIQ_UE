using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IViewService
    {
        void AddView(View view);
        List<View> GetViewsByPost(int postId);
    }
}
