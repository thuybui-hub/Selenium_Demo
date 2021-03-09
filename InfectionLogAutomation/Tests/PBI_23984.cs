using InfectionLogAutomation.DataObject;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23984 : TestBase
    {
        [Test]
        [Description("IO Log - Implement Read-only Once Current Disposition= 'Expired': New log entry")]
        public void PBI_23984_AT_23994()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryData = new TeamLogEntryInfo();
            teamLogEntryData.CurrentDisposition = "Expired";
            #endregion Test data

            #region Main steps
            #endregion Main steps
        }

        [Test]
        [Description("IO Log - Implement Read-only Once Current Disposition= 'Expired': Edit log entry")]
        public void PBI_23984_AT_23995()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryData = new TeamLogEntryInfo();
            teamLogEntryData.CurrentDisposition = "Expired";
            #endregion Test data

            #region Main steps
            #endregion Main steps
        }

    }
}
