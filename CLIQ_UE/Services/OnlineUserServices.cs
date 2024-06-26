﻿using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class OnlineUserServices : IOnlineUserServices
    {
        private readonly IOnlineUserRepository onlineUserRepository;

        public OnlineUserServices(IOnlineUserRepository onlineUserRepository)
        {
            this.onlineUserRepository = onlineUserRepository;
        }
        public void AddUser(OnlineUser onlineUser)
        {
            onlineUserRepository.AddUser(onlineUser);
        }

        public void DeleteUser(OnlineUser onlineUser)
        {
            onlineUserRepository.DeleteUser(onlineUser);
        }

        public OnlineUser GetByConnectionId(string connectionId)
        {
            return onlineUserRepository.GetByConnectionId(connectionId);
        }

        public OnlineUser GetByID(string onlineUserId)
        {
            OnlineUser onlineUser= onlineUserRepository.GetOnlineUserByID(onlineUserId);
            return onlineUser;
        }

        public void UpdateUser(OnlineUser onlineUser)
        {
            onlineUserRepository.UpdateUser(onlineUser);
        }
    }
}
