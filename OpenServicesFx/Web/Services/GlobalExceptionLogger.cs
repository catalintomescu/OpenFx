using System.Configuration;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace Web.Services
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var appName = ConfigurationManager.AppSettings["ApplicationName"];
            var log = LogManager.GetLogger(typeof(MvcApplication));
            log.Fatal(appName, context.Exception);
        }
    }
}