using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System.Linq;
using InfectionLogAutomation.DataObject;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23911 : TestBase
    {
        [Test]
        [Description("IOT - Duplication Checking")]
        public void PBI_23911_AT_23958()
        {
            #region Test Data
            LogEntryData logEntryData;
            string pageTitle = "New Infection Log Entry for Team Members";
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team 
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4. Create new log entry for a team member that already had a log entry");
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that duplicate record pop-up message opens with correct content");
            Assert.IsTrue(LogEntryDetailPage.CheckDuplicatePopup(logEntryData.Name), "Duplicate popup is incorrect");

            Log.Info("Close duplicate pop-up");
            LogEntryDetailPage.btnCancelEditting.Click();

            Log.Info("Verify that The Duplicate Record pop-up closes");
            Assert.IsFalse(LogEntryDetailPage.divDialogContent.IsDisplayed(), "The pop-up is not closed");

            Log.Info("Verify that the user still remains on New Log Entry");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The user does not remains on New Log Entry");

            Log.Info("Select on 'Save New Entry' button on the pop-up");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that Save successfully pop-up shows");
            Assert.IsTrue(LogEntryDetailPage.GetAlertWinText().Equals("Save successfully"), "Save successfully pop-up does not show");

            Log.Info("Click 'OK' on the successful pop-up");
            LogEntryDetailPage.CloseAlertPopup();

            Log.Info("Verify that Save successfully pop-up closes");
            Assert.IsTrue(string.IsNullOrEmpty(LogEntryDetailPage.GetAlertWinText()), "Save successfully pop-up shows");

            Log.Info("Verify that the log entry is saved");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved");

            Log.Info("Delete created log entried");
            HomePage.DeleteALogEntry(logEntryData.MRN);
            HomePage.DeleteALogEntry(logEntryData.MRN);
            #endregion Team

            #region Resident

            #endregion Resident

            #region Client

            #endregion
            #endregion
        }
    }
}
