using Newtonsoft.Json;

namespace CTOnline.OpenServicesFx.WebApi
{
    public class ActionErrorDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
