using CLIQ_UE.Models;
using CLIQ_UE.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CLIQ_UE.Hubs
{
	public class ChatIndividualHub : Hub
	{
        private readonly IChatIndividualServices chatIndividualServices;
        private readonly ILastMessageServices lastMessageServices;

        public ChatIndividualHub(IChatIndividualServices chatIndividualServices,ILastMessageServices lastMessageServices)
        {
            this.chatIndividualServices = chatIndividualServices;
            this.lastMessageServices = lastMessageServices;
        }

        public async void SaveMessage(string message,string currentId,string otherUserId)
        {
            if (!string.IsNullOrEmpty(message))
            {
                chatIndividualServices.AddMessageToChat(message, currentId, otherUserId);
                var messageResav = chatIndividualServices.GetOneMessage(currentId, otherUserId);
                //await Clients.Clients([Context.ConnectionId]).SendAsync("displayMessage", messageResav);
                 LastMessage lastMessage= lastMessageServices.Get(currentId, otherUserId);
                if (lastMessage == null)
                {
                    LastMessage last = new LastMessage()
                    {
                        SendId = currentId,
                        ReseverID = currentId,
                        Time = DateTime.Now,
                        Message = message,
                    };
                    lastMessageServices.Add(last);

                }
                else
                {
                    lastMessageServices.Update(lastMessage);
                }

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
