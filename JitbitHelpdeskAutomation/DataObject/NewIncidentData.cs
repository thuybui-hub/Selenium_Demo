using Newtonsoft.Json;

namespace JitbitHelpdeskAutomation.DataObject
{
    public class NewIncidentData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        [JsonProperty("caller")] public string Caller { get; set; }
        [JsonProperty("serviceApplication")] public string ServiceApplication { get; set; }
        [JsonProperty("impact")] public string Impact { get; set; }
        [JsonProperty("issueType")] public string IssueType { get; set; }
        [JsonProperty("shortDescription")] public string ShortDescription { get; set; }
        [JsonProperty("description")] public string description { get; set; }
        [JsonProperty("assignmentGroup")] public string AssignmentGroup { get; set; }
    }
}
