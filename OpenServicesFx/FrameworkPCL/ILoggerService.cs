using System;
using System.Runtime.CompilerServices;

namespace CTOnline.OpenServicesFx
{
    public interface ILoggerService
    {
        bool HasBackingService { get; }
        void SetBackingService(object service);

        void LogInfo(string message,
            [CallerMemberName] string callerMember = "",
            [CallerFilePath] string callerFile = "");

        void LogError(string error,
            [CallerMemberNameAttribute] string callerMember = "",
            [CallerFilePath] string callerFile = "");

        void LogError(IServiceResponse response,
            [CallerMemberNameAttribute] string callerMember = "",
            [CallerFilePath] string callerFile = "");

        void LogException(Exception exception,
            [CallerMemberNameAttribute] string callerMember = "",
            [CallerFilePath] string callerFile = "");
    }
}
