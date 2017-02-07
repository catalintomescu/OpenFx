
namespace CTOnline.OpenServicesFx.Reference
{
    public class EmptyStatusNotificationService : IStatusNotificationService
    {
        public void SendNotification(string level, string message, dynamic context, string callerFileName, string callerMember)
        {            
        }
    }
}
