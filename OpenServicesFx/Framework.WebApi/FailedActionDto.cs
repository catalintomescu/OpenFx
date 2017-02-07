using System.Collections.Generic;
using Newtonsoft.Json;

namespace CTOnline.OpenServicesFx.WebApi
{
    public class FailedActionDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<ActionErrorDto> Errors { get; set; }

        public bool ShouldSerializeErrors()
        {
            return Errors == null || Errors.Count > 0;
        }
    }
}
