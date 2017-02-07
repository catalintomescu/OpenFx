using System;
using System.Reflection;
using log4net;

namespace CTOnline.OpenServicesFx
{
    public abstract class ApplicationServiceBase
    {
        private const string MissingParameterMessage = "Invalid parameter value. Parameter is {0}.";

        protected dynamic ExecutionContext { get; private set; }
        protected ILoggerService Logger { get; set; }
        protected IActionExecutionContext ExecutionContextTyped { get; private set; }

        protected ApplicationServiceBase(IActionExecutionContext executionContext, ILoggerService logger)
        {
            ExecutionContext = executionContext;
            ExecutionContextTyped = (IActionExecutionContext)ExecutionContext;
            Logger = logger;
        }

        protected virtual void InitializeLogger()
        {
            Logger.SetBackingService(LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType()));

            //var ctx = (IActionExecutionContext)ExecutionContext;
            //if (!Logger.HasBackingService)
            //    Logger.SetBackingService(
            //        LogManager.GetLogger(ctx.IsVerboseLoggingDisabled
            //            ? Constants.AppSettings.Log4NetRegularLoggerName
            //            : Constants.AppSettings.Log4NetFullLoggerName));
        }

        protected void HandleSystemException(Exception ex, IServiceResponse response)
        {
            if (response != null)
            {
                response.Message = ex.Message;
                response.AddError(description: ex.ToString(), status: StatusCode.InternalServerError);
            }
            Logger.LogException(ex);
        }

        protected bool IsStringParameterNotNullOrWhiteSpace(string value, string parameterName, IServiceResponse response)
        {
            return IsParameterValid(() => !string.IsNullOrWhiteSpace(value), parameterName, response);
        }

        protected bool IsObjectParameterNotNull(object value, string parameterName, IServiceResponse response)
        {
            return IsParameterValid(() => value != null, parameterName, response);
        }

        protected bool IsNumericParameterGTZero(int value, string parameterName, IServiceResponse response)
        {
            return IsParameterValid(() => value > 0, parameterName, response);
        }

        protected bool IsParameterValid(Func<bool> validation, string parameterName, IServiceResponse response)
        {
            if (validation()) return true;
            response.AddError(
                description: string.Format(MissingParameterMessage, parameterName),
                status: StatusCode.BadRequest);
            Logger.LogError(response);
            return false;
        }
    }
}
