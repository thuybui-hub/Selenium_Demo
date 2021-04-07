using Newtonsoft.Json;

namespace InfectionLogAutomation.DataObject
{
    class LoginData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        [JsonProperty("validUser")]
        public string ValidUser { get; set; }
        [JsonProperty("validPassword")]
        public string ValidPassword { get; set; }
    }
}
