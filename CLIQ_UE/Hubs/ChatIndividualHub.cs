using CLIQ_UE.Helpers;
using CLIQ_UE.Models;
using CLIQ_UE.Services;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace CLIQ_UE.Hubs
{
    public class ChatIndividualHub : Hub
    {
        private readonly IChatIndividualServices chatIndividualServices;
        private readonly ILastMessageServices lastMessageServices;
        private readonly IFollowersServices followersServices;
        private readonly IUserServices userServices;
        private readonly IOnlineUserServices onlineUserServices;
        private readonly INotificationService notificationService;

        public ChatIndividualHub(IChatIndividualServices chatIndividualServices
                                , ILastMessageServices lastMessageServices
                                , IFollowersServices followersServices
                                , IUserServices userServices
                                , IOnlineUserServices onlineUserServices,
            INotificationService notificationService)
        {
            this.chatIndividualServices = chatIndividualServices;
            this.lastMessageServices = lastMessageServices;
            this.followersServices = followersServices;
            this.userServices = userServices;
            this.onlineUserServices = onlineUserServices;
            this.notificationService = notificationService;
        }

        public async void SaveMessage(string message, string currentId, string otherUserId)
        {
            if (!string.IsNullOrEmpty(message))
            {
                chatIndividualServices.AddMessageToChat(message, currentId, otherUserId);
                var messageResav = chatIndividualServices.GetOneMessage(currentId, otherUserId);
                //await Clients.Clients([Context.ConnectionId]).SendAsync("displayMessage", messageResav);
                LastMessage lastMessage = lastMessageServices.Get(currentId, otherUserId);
                if (lastMessage == null)
                {
                    lastMessage = new LastMessage()
                    {
                        SendId = currentId,
                        ReseverID = otherUserId,
                        Time = DateTime.Now,
                        Message = message,
                    };
                    lastMessageServices.Add(lastMessage);

                }
                else
                {
                    lastMessage.Message = message;
                    lastMessage.Time = DateTime.Now;
                    lastMessageServices.Update(lastMessage);
                }
                //OnlineUser onlineUser = onlineUserServices.GetByID(lastMessage.ReseverID);
                //string connectionID = null;
                //if (onlineUser != null)
                //    connectionID = onlineUser.ConnectionId;

                //if (!string.IsNullOrEmpty(connectionID))
                //{
                //    await Clients.Clients([connectionID, Context.ConnectionId]).SendAsync("displayMessage", messageResav);
                //}
                //else
                //{
                //    await Clients.Clients([Context.ConnectionId]).SendAsync("displayMessage", messageResav);
                //}
                ApplicationUser applicationUser = userServices.GetByID(otherUserId);
                await Clients.Caller.SendAsync("desplayMessageForCaller", messageResav , applicationUser.PersonalImage);
                await Clients.Others.SendAsync("displayMessage", messageResav, applicationUser.PersonalImage);

                await Clients.User(currentId).SendAsync("NewMessageNotification", currentId);
                notificationService.AddNotification(otherUserId, currentId, "sent you a message");


                await UpdateListAsync(lastMessage, currentId, otherUserId);
            }
        }
        private async Task UpdateListAsync(LastMessage lastMessage, string currentId, string otherUserId)
        {

            //Followers follower = followersServices.GetByFollowerId(lastMessage.ReseverID, lastMessage.SendId);
            ApplicationUser otherUser = userServices.GetByID(otherUserId);
            UserConntactViewModel userConntactViewModel = new UserConntactViewModel();
            userConntactViewModel.LastMessage = lastMessage;
            userConntactViewModel.UserId = otherUser.Id;
            userConntactViewModel.UserName = otherUser.FirstName + " " + otherUser.LastName;
            userConntactViewModel.ImageUrl = otherUser.PersonalImage;
            userConntactViewModel.FormatedTime = FormatTimeForChat.CalculateLastSeenForUserList(lastMessage.Time.ToString("yyyy-MM-dd HH:mm"));

            //Followers Otherfollower = followersServices.GetByFollowerId(lastMessage.SendId, lastMessage.ReseverID);
            UserConntactViewModel otherOserConntactViewModel = new UserConntactViewModel();
            ApplicationUser applicationUser = userServices.GetByID(currentId);
            otherOserConntactViewModel.LastMessage = lastMessage;
            otherOserConntactViewModel.UserId = applicationUser.Id;
            otherOserConntactViewModel.UserName = applicationUser.FirstName + " " + applicationUser.LastName;
            otherOserConntactViewModel.ImageUrl = applicationUser.PersonalImage;
            otherOserConntactViewModel.FormatedTime = FormatTimeForChat.CalculateLastSeenForUserList(lastMessage.Time.ToString("yyyy-MM-dd HH:mm"));

            await Clients.Caller.SendAsync("upDateListUsers", userConntactViewModel);
            //if (connectionID != null)
            //{
            //    await Clients.Client(connectionID).SendAsync("upDateListUsersInOTherUser", otherOserConntactViewModel);
            //}
            await Clients.Others.SendAsync("upDateListUsersInOTherUser", otherOserConntactViewModel, otherUserId);

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
