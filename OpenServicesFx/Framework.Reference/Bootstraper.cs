using System.Reflection;
using log4net;
using StatsdClient;
using StructureMap.Configuration.DSL;

namespace CTOnline.OpenServicesFx.Reference
{
    public class Bootstraper
    {
        public static void InitializeIoCWithStructuremap(Registry registry)
        {
            registry.For<IStatusNotificationService>().Use<EmptyStatusNotificationService>();
            registry.For<ILog>().Use(log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType()));
            registry.For<ILoggerService>().Use<Log4NetLoggerService>();
            registry.For<IActionExecutionContext>().Use<ActionExecutionContext>();
            registry.For<IMetricsService>().Use<DatadogMetricsService>();
        }

        public static void InitializeLoggingAndDatadog(string datadogPrefix)
        {
            log4net.Config.XmlConfigurator.Configure();
            // TODO: Initialize a log4net appender for Datadog

            // Initialize 
            var dogstatsdConfig = new StatsdConfig
            {
                StatsdServerName = "127.0.0.1",
                StatsdPort = 8125,
                Prefix = datadogPrefix
            };
            DogStatsd.Configure(dogstatsdConfig);
        }
    }
}
