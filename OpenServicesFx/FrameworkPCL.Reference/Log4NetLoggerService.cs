using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using log4net;
using log4net.Core;

namespace CTOnline.OpenServicesFx.Reference
{
    public class Log4NetLoggerService : ILoggerService
    {
        #region Private Data Members

        protected readonly IActionExecutionContext ExecutionContextTyped;
        protected readonly dynamic ExecutionContext;
        protected ILog _log;
        protected readonly IStatusNotificationService _statusNotificationService;

        protected const string LogHeaderFormat = "{0}.{1}\nSessionIdentifier: {2}\nClientAppKey: {3}\nIdentity: {4}\n";
        protected const string LogMessageFormat = "Message: {0}";
        protected const string ErrorMsgFormat = "Code: {0}\nDescription: {1}";

        #endregion

        #region Constructors and Destructors

        public Log4NetLoggerService(
            IStatusNotificationService statusNotificationService,
            IActionExecutionContext executionContext)
        {
            _statusNotificationService = statusNotificationService;
            ExecutionContext = executionContext;
            ExecutionContextTyped = (IActionExecutionContext)ExecutionContext;
        }

        #endregion

        #region ILoggerService Members

        public bool HasBackingService
        {
            get { return _log != null; }
        }

        public void SetBackingService(object service)
        {
            _log = service as ILog;
        }

        public virtual void LogInfo(string message,
            [CallerMemberName]string callerMember = "",
            [CallerFilePath]string callerFile = "")
        {
            EnsureILog();
            var fileName = Path.GetFileNameWithoutExtension(callerFile);
            var logMessage = new StringBuilder();
            logMessage.AppendFormat(LogHeaderFormat, fileName, callerMember, ExecutionContext.SessionIdentifier,
                ExecutionContext.ApiKey, ExecutionContext.IdentityName);
            logMessage.AppendFormat(LogMessageFormat, message);

            _log.Info(logMessage.ToString());
            _statusNotificationService.SendNotification(Level.Info.Name, message, ExecutionContext, fileName, callerMember);
        }

        public virtual void LogError(string error,
            [CallerMemberNameAttribute]string callerMember = "",
            [CallerFilePath]string callerFile = "")
        {
            EnsureILog();
            var fileName = Path.GetFileNameWithoutExtension(callerFile);

            var logMessage = new StringBuilder();
            logMessage.AppendFormat(LogHeaderFormat, fileName, callerMember, ExecutionContext.SessionIdentifier,
                ExecutionContext.ApiKey, ExecutionContext.IdentityName);
            logMessage.AppendFormat(ErrorMsgFormat, error, string.Empty);

            _log.Error(logMessage.ToString());
            _statusNotificationService.SendNotification(Level.Error.Name, error, ExecutionContext, fileName, callerMember);
        }

        public virtual void LogError(IServiceResponse response,
            [CallerMemberNameAttribute]string callerMember = "",
            [CallerFilePath]string callerFile = "")
        {
            EnsureILog();
            var fileName = Path.GetFileNameWithoutExtension(callerFile);

            var logMessage = new StringBuilder();
            logMessage.AppendFormat(LogHeaderFormat, fileName, callerMember, ExecutionContext.SessionIdentifier,
                ExecutionContext.ApiKey, ExecutionContext.IdentityName);

            var notifyMessage = new StringBuilder();
            logMessage.AppendFormat(LogMessageFormat, response.Message);
            notifyMessage.AppendFormat("Message: {0}.", response.Message);
            foreach (var error in response.Errors)
            {
                logMessage.AppendFormat(ErrorMsgFormat, error.Code, error.Description);
                notifyMessage.AppendFormat("Error: {0}. {1}", error.Code, error.Description);
            }

            _log.Error(logMessage.ToString());
            _statusNotificationService.SendNotification(Level.Error.Name, notifyMessage.ToString(), ExecutionContext, fileName, callerMember);
        }

        public virtual void LogException(Exception exception,
            [CallerMemberNameAttribute]string callerMember = "",
            [CallerFilePath]string callerFile = "")
        {
            EnsureILog();
            var fileName = Path.GetFileNameWithoutExtension(callerFile);
            var logMessage = new StringBuilder();
            logMessage.AppendFormat(LogHeaderFormat, fileName, callerMember, ExecutionContext.SessionIdentifier,
                ExecutionContext.ApiKey, ExecutionContext.IdentityName);
            logMessage.AppendFormat(LogMessageFormat, exception.Message);
            logMessage.AppendFormat(ErrorMsgFormat, exception, string.Empty);

            _log.Error(TrimExtraCharsForDatadog(logMessage.ToString()));
            _statusNotificationService.SendNotification(Level.Error.Name, exception.Message, ExecutionContext, fileName, callerMember);
        }

        #endregion

        #region Helper Members

        private string TrimExtraCharsForDatadog(string value)
        {
            // This is a limitation in Datadog
            var tmp = value;
            if (value.Length >= 8000)
            {
                tmp = value.Substring(0, 7999);
            }

            return tmp;
        }

        private void EnsureILog()
        {
            if (_log == null) _log = LogManager.GetLogger(typeof(Log4NetLoggerService));
        }

        #endregion
    }
}
