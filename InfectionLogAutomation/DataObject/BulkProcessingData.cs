using Newtonsoft.Json;

namespace InfectionLogAutomation.DataObject
{
    public class BulkProcessingData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        public string Region { get; set; }
        public string Community { get; set; }
        public string Name { get; set; }
        public string MRN { get; set; }
        public string TestingDate { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
    }
}
