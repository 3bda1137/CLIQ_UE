using System.Collections.Concurrent;

namespace CLIQ_UE.Models
{
    public static class ConnectionManager
    {
        private static readonly ConcurrentDictionary<string, string> userConnectionMap = new ConcurrentDictionary<string, string>();

        public static void AddConnection(string userId, string connectionId)
        {
            userConnectionMap.AddOrUpdate(userId, connectionId, (key, oldValue) => connectionId);
        }

        public static void RemoveConnection(string connectionId)
        {
            var userId = userConnectionMap.FirstOrDefault(x => x.Value == connectionId).Key;
            if (userId != null)
            {
                userConnectionMap.TryRemove(userId, out _);
            }
        }

        public static string GetConnectionIdForUserId(string userId)
        {
            if (userConnectionMap.TryGetValue(userId, out var connectionId))
            {
                return connectionId;
            }
            return null;
        }
    }
}
