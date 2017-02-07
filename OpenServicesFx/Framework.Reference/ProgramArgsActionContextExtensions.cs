using System;
using System.Security.Principal;

namespace CTOnline.OpenServicesFx.Reference
{
    public class ProgramArgsActionContextExtensions
    {
        public static void ResolveActionExecutionContext(string[] args, IActionExecutionContext context)
        {
            context.IdentityName = WindowsIdentity.GetCurrent().Name;
            context.SessionIdentifier = Guid.NewGuid();
            if (args == null) return;

            foreach (var arg in args)
            {
                var tmp = arg;
                if (arg.StartsWith("-", StringComparison.InvariantCultureIgnoreCase))
                    tmp = arg.Substring(1);

                context.IsNotificationDisabled = false;
                context.IsVerboseLoggingDisabled = false;

                string[] tokens = tmp.Split(':');
                if (string.CompareOrdinal(tokens[0], "disableNotifications") == 0)
                {
                    if (tokens.Length == 1)
                    {
                        context.IsNotificationDisabled = true;
                        continue;
                    }
                    context.IsNotificationDisabled = (string.CompareOrdinal(tokens[1].ToLower(), "y") == 0);
                }
                else if (string.CompareOrdinal(tokens[0], "disableNotifications") == 0)
                {
                    if (tokens.Length == 1)
                    {
                        context.IsVerboseLoggingDisabled = true;
                        continue;
                    }
                    context.IsVerboseLoggingDisabled = (string.CompareOrdinal(tokens[1].ToLower(), "y") == 0);
                }
            }
        }
    }
}
