using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Hubs
{
    public class OnlineUsersHub : Hub
    {
        private readonly IOnlineUserServices onlineUserServices;
        private readonly IUserServices userServices;

        public OnlineUsersHub(IOnlineUserServices onlineUserServices, IUserServices userServices)
        {
            this.onlineUserServices = onlineUserServices;
            this.userServices = userServices;
        }
        [Authorize]
        public void SaveOnlineUser(string connectionId)
        {
            var userName = Context.User.Identity.Name;
            //var connectionId = Context.ConnectionId;
            if (userName != null)
            {
                string userId = userServices.GetUserByUserName(userName).Id;

                OnlineUser onlineUser = new OnlineUser()
                {
                    UserId = userId,
                    ConnectionId = connectionId,
                };
                onlineUserServices.AddUser(onlineUser);
            }
        }
        public void DeleteOnlineUser(string connectionId)
        {
            OnlineUser onlineUser = onlineUserServices.GetByConnectionId(connectionId);
            if (onlineUser != null)
            {
                onlineUserServices.DeleteUser(onlineUser);
            }
        }



        public override Task OnConnectedAsync()
        {
            SaveOnlineUser(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            DeleteOnlineUser(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
