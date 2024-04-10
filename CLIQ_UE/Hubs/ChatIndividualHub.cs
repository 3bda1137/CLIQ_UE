using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CLIQ_UE.Hubs
{
	public class ChatIndividualHub : Hub
	{
        private readonly IChatIndividualServices chatIndividualServices;

        public ChatIndividualHub(IChatIndividualServices chatIndividualServices)
        {
            this.chatIndividualServices = chatIndividualServices;
        }

        public async void SaveMessage(string message,string currentId,string otherUserId)
        {
            if (!string.IsNullOrEmpty(message))
            {
                chatIndividualServices.AddMessageToChat(message, currentId, otherUserId);
                var messageResav = chatIndividualServices.GetOneMessage(currentId, otherUserId);
                //await Clients.Clients([Context.ConnectionId]).SendAsync("displayMessage", messageResav);
                await Clients.All.SendAsync("displayMessage", messageResav);
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
