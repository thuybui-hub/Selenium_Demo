using Newtonsoft.Json;

namespace AvailableUnitsAutomation.DataObject
{
    public class UnitData
    {
        public string Path => $"\\Resources\\TestData\\{this.GetType().Name}.json";

        public string UnitNumber { get; set; }
        public string ServiceLine { get; set; }
        public string MonthlySellingRate { get; set; }        
        public string ChangeType { get; set; }        
        public string BusinessReason { get; set; }        
    }
}
