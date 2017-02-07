using Newtonsoft.Json;

namespace CTOnline.OpenServicesFx
{
    public class ServiceResponseError
    {
        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
