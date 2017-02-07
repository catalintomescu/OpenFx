
namespace CTOnline.OpenServicesFx
{
    public interface IStatusNotificationService
    {
        void SendNotification(string level, string message, dynamic context, string callerFileName, string callerMember);
    }
}
