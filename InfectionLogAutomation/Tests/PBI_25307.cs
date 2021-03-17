using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System;
using InfectionLogAutomation.DataObject;
using SeleniumCSharp.Core.Utilities;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_25307: TestBase
    {
        [Test]
        [Description("IO Log: All Record Search Updates: Search page UI")]
        public void PBI_25307_AT_25312()
        {
            #region Test Data
            string from = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy");
            List<string> community = new List<string>() { "Rio Las Palmas" };
            List<string> filters = new List<string>() { "Active", "Inactive" };            
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);                      

            Log.Info("Verify that Home page displays correctly");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("Verify that default search values are correct");
            Assert.IsTrue(HomePage.AreDefaultSearchValuesCorrect(), "The default search values display incorrectly");

            Log.Info("Verify that users can search with one or more communities");
            Assert.IsTrue(HomePage.IsCommunityMultiselect(), "Unable to select more communities");

            Log.Info("Verify that Filters field is correct");
            Assert.IsTrue(HomePage.IsFiltersFieldCorrect(), "The Filters field is incorrect");

            Log.Info("Verify that Last Updated \"from - to\" date range and selected from a calendar");
            Assert.IsTrue(HomePage.AreDateSelectedFromCalendar(), "Last Updated dates are not selected from calendar");            

            Log.Info("3. Enter Last Updated dates and From  is later than To");
            HomePage.FillSearchCriterias("", from);

            Log.Info("Verify that an alert message shows to tell that Last Updated From should not be later than To");
            Assert.IsTrue(HomePage.GetAlertWinText().Contains("Last Updated From Date should not be later than To Date"));
            HomePage.CloseAlertPopup();

            Log.Info("4. Perform search criteria");
            from = DateTime.Now.AddDays(-5).ToString("MM/dd/yyyy");
            HomePage.PerformASearchCriteria(community, from, null, filters);

            Log.Info("5. Click on Reset button");
            HomePage.ResetSearchCriteria();

            Log.Info("Verify that Reset button restores the search criteria to default settings");            
            Assert.IsTrue(HomePage.AreDefaultSearchValuesCorrect(), "The search criterias are not set to default setting");
            #endregion
        }

        [Test]
        [Description("IO Log: All Record Search Updates: New and Edit a log entry")]
        public void PBI_25307_AT_25320()
        {
            #region Test Data
            LogEntryData logEntryData = JsonParser.Get<LogEntryData>();
            LogEntryData logEntryDataEdit = JsonParser.Get<LogEntryData>();
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page displays correctly");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("3. Go to new log entry form");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4. Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the user is back to Search page after submitting a log entry");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("Verify that Search page displays correctly");
            Assert.IsTrue(HomePage.AreDefaultSearchValuesCorrect(), "The default search values display incorrectly");

            Log.Info("5. Open the log entry created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            HomePage.OpenALogEntry(0);

            Log.Info("6. Edit log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryDataEdit, "Edit");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the user is back to Search page after updating a log entry.");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("Verify that Search page displays correctly");
            Assert.IsTrue(HomePage.AreDefaultSearchValuesCorrect(), "The default search values display incorrectly");

            Log.Info("Clean up: Delete the log entry created");
            HomePage.DeleteALogEntry(logEntryData.MRN);
            #endregion
        }
    }
}
