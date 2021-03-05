using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_24066 : TestBase
    {
        #region Test data
        #endregion Test data

        [Test]
        [Description("IO Log - clear the filters")]
        public void PBI_24066_AT_24108()
        {
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account who have access to multiple entry types: - Admin role: gnguyen");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Home screen with all log entries displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that there is a button Clear Filters on tool bar of Dashboard table");
            Assert.IsTrue(HomePage.btnClearFilters.IsDisplayed(), "Clear button does not exist on Home screen.");

            Log.Info("4.1. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Team Member");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Table shows incorrect records.");

            Log.Info("5.1. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");

            Log.Info("6.1. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");

            Log.Info("4.2. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Resident");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Resident"), "Table shows incorrect records.");

            Log.Info("5.2. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");

            Log.Info("6.2. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");

            Log.Info("4.3. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Ageility Client");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Ageility Client"), "Table shows incorrect records.");

            Log.Info("5.3. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");

            Log.Info("6.3. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");

            Log.Info("7. Perform a search");

            Log.Info("Verify that Dashboard table shows records for searching data only");

            Log.Info("8. Go to New Log Entry form and press on browser back button");

            Log.Info("Verify that Search box is not preserved when moving to another form.");

            Log.Info("9. Perform a search");

            Log.Info("10. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the searching data is cleared and Dashboard table shows all records for Team Members, Resident and Client");

            Log.Info("Log out and log in with account who does not have access to multiple entry types:");

            Log.Info("");
        }
    }
}
