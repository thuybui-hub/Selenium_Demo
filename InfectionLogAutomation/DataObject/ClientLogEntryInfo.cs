﻿using Newtonsoft.Json;

namespace InfectionLogAutomation.DataObject
{
    public class ClientLogEntryInfo
    {
        public string Path => $"\\Resources\\TestData\\LogEntryData.json";

        public string Region { get; set; }
        public string Community { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
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