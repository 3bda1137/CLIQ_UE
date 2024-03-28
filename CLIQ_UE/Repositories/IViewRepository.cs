using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IViewRepository
    {
        void AddView(View view);
        List<View> GetViewsByPost(int postId);
    }
}
