using Microsoft.AspNet.SignalR;

namespace Web.Services
{
    public class StatusNotificationHub : Hub
    {
        public string JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
            return string.Format("Added group {0} to Connection:{1}", groupName, Context.ConnectionId);
        }
    }
}