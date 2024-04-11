using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public interface IOnlineUserRepository
    {
        public void AddUser(OnlineUser onlineUser);
        public void UpdateUser(OnlineUser onlineUser);
        public OnlineUser GetOnlineUserByID(string olineUserId);
        public void DeleteUser(OnlineUser onlineUser);
        public OnlineUser GetByConnectionId(string connectionId);
    }
}
