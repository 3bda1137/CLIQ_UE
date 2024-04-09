using CLIQ_UE.Models;

namespace CLIQ_UE.Services
{
    public interface IOnlineUserServices
    {
        public void AddUser(OnlineUser onlineUser);
        public void UpdateUser(OnlineUser onlineUser);
        public OnlineUser GetByID(string onlineUserId);
        public OnlineUser GetByConnectionId(string connectionId);
        public void DeleteUser(OnlineUser onlineUser);
    }
}
