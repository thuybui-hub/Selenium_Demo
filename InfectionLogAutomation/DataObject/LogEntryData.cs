using Newtonsoft.Json;

namespace InfectionLogAutomation.DataObject
{
    public class LogEntryData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        public string Region { get; set; }
        public string Community { get; set; }
        public string Name { get; set; }        
        public string MRN { get; set; }
        public string InfectionType { get; set; }
        public string OnsetDate { get; set; }
        [JsonProperty("Symptoms")]
        public string Symptoms { get; set; }
        [JsonProperty("CurrentTestStatus")]
        public string CurrentTestStatus { get; set; }
        public string TestStatusDate { get; set; }
        [JsonProperty("CurrentDisposition")]
        public string CurrentDisposition { get; set; }
        public string DispositionDate { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
    }
}
