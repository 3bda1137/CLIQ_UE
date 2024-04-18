using CLIQ_UE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Hubs
{
    public class PostsHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsHub(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                string userId = user.Id;
                string connectionId = Context.ConnectionId;

                ConnectionManager.AddConnection(userId, connectionId);
            }

            await base.OnConnectedAsync();
        }
    }
}
