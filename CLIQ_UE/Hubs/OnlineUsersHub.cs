using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Hubs
{
    [Authorize]
    public class OnlineUsersHub : Hub
    {
        private readonly IOnlineUserServices onlineUserServices;
        private readonly IUserServices userServices;
        private readonly ILastSeenServices lastSeenServices;

        public OnlineUsersHub(IOnlineUserServices onlineUserServices, IUserServices userServices,ILastSeenServices lastSeenServices)
        {
            this.onlineUserServices = onlineUserServices;
            this.userServices = userServices;
            this.lastSeenServices = lastSeenServices;
        }
        [Authorize]
        public void SaveOnlineUser(string connectionId)
        {
            var userName = Context.User.Identity.Name;
            //var connectionId = Context.ConnectionId;
            if (userName != null)
            {
                string userId = userServices.GetUserByUserName(userName).Id;
                OnlineUser existingOnlineUser= onlineUserServices.GetByID(userId);
                if (existingOnlineUser != null)
                {
                    existingOnlineUser.ConnectionId = connectionId;
                    onlineUserServices.UpdateUser(existingOnlineUser);
                }
                else
                {
                    OnlineUser onlineUser = new OnlineUser()
                    {
                        UserId = userId,
                        ConnectionId = connectionId,
                    };
                    onlineUserServices.AddUser(onlineUser);
                }
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

        private void UpDateLastSeen()
        {
            var userName = Context.User.Identity.Name;
            if (userName != null)
            {
                string userId = userServices.GetUserByUserName(userName).Id;
                LastSeen lastSeen= lastSeenServices.GetByUserId(userId);
                if(lastSeen==null)
                {
                    lastSeen= new LastSeen();
                    lastSeen.LastSeenTime = DateTime.Now;
                    lastSeen.UserID= userId;
                    lastSeenServices.Add(lastSeen);
                }
                else
                {
                    lastSeen.LastSeenTime= DateTime.Now;
                    lastSeenServices.Update(lastSeen);
                }
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
            UpDateLastSeen();
            return base.OnDisconnectedAsync(exception);
        }

       
    }
}
