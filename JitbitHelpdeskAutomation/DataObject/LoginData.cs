using Newtonsoft.Json;

namespace JitbitHelpdeskAutomation.DataObject
{
    public class LoginData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        [JsonProperty("validUser")] public string ValidUser { get; set; }
        [JsonProperty("validPassword")] public string ValidPassword { get; set; }
        [JsonProperty("serviceNowUser")] public string ServiceNowUser { get; set; }
        [JsonProperty("ITILUser")] public string ITILUser { get; set; }
    }
}