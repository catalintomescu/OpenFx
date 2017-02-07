using System;
using System.Globalization;
using System.Web.Http.Controllers;

namespace CTOnline.OpenServicesFx.WebApi
{
    public static class HttpActionContextExtensions
    {
        public static void ResolveActionExecutionContext(this HttpActionContext httpContext,
            ApiActionQueryOptions options, IActionExecutionContext context)
        {
            context.IdentityName = httpContext.RequestContext.Principal.Identity.Name;
            context.SessionIdentifier = Guid.NewGuid();
            if (options == null) return;

            context.ApiKey = options.apiKey;
            context.IsVerboseLoggingDisabled = !string.IsNullOrEmpty(options.disableVerboseLogging)
                                               && !string.IsNullOrWhiteSpace(options.disableVerboseLogging)
                                               &&
                                               string.Compare(options.disableVerboseLogging.ToLower(), "y",
                                                   CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0;
            context.IsNotificationDisabled = !string.IsNullOrEmpty(options.disableNotifications)
                                             && !string.IsNullOrWhiteSpace(options.disableNotifications)
                                             &&
                                             string.Compare(options.disableNotifications.ToLower(), "y",
                                                 CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0;
        }
    }
}
