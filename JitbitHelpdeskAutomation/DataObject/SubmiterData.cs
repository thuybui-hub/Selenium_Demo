using Newtonsoft.Json;

namespace JitbitHelpdeskAutomation.DataObject
{
    public class SubmiterData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";
        [JsonProperty("fullName")] public string FullName { get; set; }
        [JsonProperty("location")] public string Location { get; set; }
        [JsonProperty("jobTitle")] public string JobTitle { get; set; }
        [JsonProperty("department")] public string Department { get; set; }
    }
}