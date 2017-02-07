using System.Net.Http.Formatting;

namespace CTOnline.OpenServicesFx.WebApi
{
    public static class ActionResultHelpers
    {
        public static JsonMediaTypeFormatter JsonFormatter
        {
            get
            {
                return new JsonMediaTypeFormatter
                {
                    Indent = true,
                };
            }
        }
    }
}
