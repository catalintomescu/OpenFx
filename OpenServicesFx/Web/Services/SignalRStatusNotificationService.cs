using System;
using CTOnline.OpenServicesFx;
using Microsoft.AspNet.SignalR;

namespace Web.Services
{
    public class SignalRStatusNotificationService : IStatusNotificationService
    {
        #region IStatusNotificationService Members

        public void SendNotification(string level, string message, dynamic context, string callerFileName, string callerMember)
        {
            if (context.IsNotificationDisabled) return;

            try
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<StatusNotificationHub>();
                if (hub == null) return;

                var notification = new
                {
                    CodeLocation = callerFileName + "." + callerMember,
                    IdentityName = context.IdentityName,
                    Message = message,
                    SessionId = context.SessionIdentifier.ToString(),
                    Timestamp = DateTime.Now.ToString("G"),
                    Level = level,
                };

                hub.Clients.All.globalNotification(notification);
                hub.Clients.Group(string.Format("Session{0}", context.SessionIdentifier)).groupNotification(notification);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}
