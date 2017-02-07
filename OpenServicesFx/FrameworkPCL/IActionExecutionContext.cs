using System;

namespace CTOnline.OpenServicesFx
{
    public interface IActionExecutionContext
    {
        Guid SessionIdentifier { get; set; }
        string IdentityName { get; set; }
        string ApiKey { get; set; }
        bool IsVerboseLoggingDisabled { get; set; }
        bool IsNotificationDisabled { get; set; }
    }
}
