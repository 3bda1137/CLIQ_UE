using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class OnlineUserRepository : IOnlineUserRepository
    {
        private readonly ApplicationContext context;

        public OnlineUserRepository(ApplicationContext _context)
        {
            context = _context;
        }
        public void AddUser(OnlineUser onlineUser)
        {
            context.OnlineUsers.Add(onlineUser);
            context.SaveChanges();
        }

        public void DeleteUser(OnlineUser onlineUser)
        {
            context.OnlineUsers.Remove(onlineUser);
            context.SaveChanges();
        }

        public OnlineUser GetByConnectionId(string connectionId)
        {
            return context.OnlineUsers.FirstOrDefault(x => x.ConnectionId == connectionId);
        }

        public OnlineUser GetOnlineUserByID(int onlineUserId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(OnlineUser onlineUser)
        {
            throw new NotImplementedException();
        }
    }
}
