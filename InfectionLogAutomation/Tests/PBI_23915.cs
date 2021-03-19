using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System.Linq;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23915: TestBase
    {
        [Test]
        [Description("Capture/Display LOB on Home Screen")]
        public void PBI_23915_AT_23921()
        {
            #region Test Data
            List<string> logEntriesList;
            List<string> lstNull = new List<string>() { "null"};
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page displays correctly");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("3. Show both active & inactive records");
            HomePage.ShowBothActiveAndInactiveRecords();

            Log.Info("4. Filter log entries by type = Resident");
            HomePage.FilterATableColumn("Entry Type", "Resident");

            Log.Info("Verify that Resident's LOB is displayed for each resident records");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB").Distinct().ToList();
            Assert.IsTrue(HomePage.DoAllLogEntriesHaveCorrectLOB(logEntriesList), "Some records do not show Resident LOB");

            Log.Info("Verify that the data is able to sort by LOB");
            HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, "Resident LOB");

            Log.Info("Filter data by LOB = AL");
            HomePage.FilterATableColumn("Resident LOB", "AL");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB");
            Log.Info("Verify that all resident records show Resident LOB = AL");
            Assert.IsTrue(HomePage.DoAllListItemEqualValue(logEntriesList, "AL"), "There is any record with Resident LOB that is not AL");

            Log.Info("Filter data by LOB = IL");
            HomePage.FilterATableColumn("Resident LOB", "IL");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB");
            Log.Info("Verify that all resident records show Resident LOB = IL");
            Assert.IsTrue(HomePage.DoAllListItemEqualValue(logEntriesList, "IL"), "There is any record with Resident LOB that is not IL");

            Log.Info("Filter data by LOB = ALZ");
            HomePage.FilterATableColumn("Resident LOB", "ALZ");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB");
            Log.Info("Verify that all resident records show Resident LOB = ALZ");
            Assert.IsTrue(HomePage.DoAllListItemEqualValue(logEntriesList, "ALZ"), "There is any record with Resident LOB that is not ALZ");

            Log.Info("Filter data by LOB = SNF");
            HomePage.FilterATableColumn("Resident LOB", "SNF");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB");
            Log.Info("Verify that all resident records show Resident LOB = SNF");
            Assert.IsTrue(HomePage.DoAllListItemEqualValue(logEntriesList, "SNF"), "There is any record with Resident LOB that is not SNF");

            Log.Info("Filter log entries by type = Team");
            HomePage.ClearAllFilters();
            HomePage.FilterATableColumn("Entry Type", "Team Member");

            Log.Info("Verify that Team log entries have no LOB information");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB").Distinct().ToList();
            //Assert.IsTrue(string.IsNullOrEmpty(string.Join(",", logEntriesList)), "There is LOB information for Team Log Entries");
            Assert.IsTrue(logEntriesList.SequenceEqual(lstNull), "There is LOB information for Team Log Entries");

            Log.Info("Filter log entries by type = Ageility Client");
            HomePage.ClearAllFilters();
            HomePage.FilterATableColumn("Entry Type", "Ageility Client");

            Log.Info("Verify that Team log entries have no LOB information");
            logEntriesList = HomePage.tblDashboard.GetTableAllCellValueInColumn("Resident LOB").Distinct().ToList();            
            Assert.IsTrue(string.IsNullOrEmpty(string.Join(",", logEntriesList)), "There is LOB information for Team Log Entries");
            #endregion
        }
    }
}
