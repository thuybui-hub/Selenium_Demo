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
            #region New page             
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
            LogEntryDetailPage.btnSaveLogEntry.Click();

            Log.Info("Verify that duplicate record pop-up message opens with correct content");            
            Assert.IsTrue(LogEntryDetailPage.CheckDuplicatePopup(logEntryData.Name + " " + logEntryData.MRN), "Duplicate popup content is incorrect");

            Log.Info("5. Close duplicate pop-up");
            LogEntryDetailPage.btnCancelEditting.Click();

            Log.Info("Verify that The Duplicate Record pop-up closes");
            Assert.IsFalse(LogEntryDetailPage.divDialogContent.IsDisplayed(), "The pop-up is not closed");

            Log.Info("Verify that the user still remains on New Log Entry");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The user does not remains on New Log Entry");

            Log.Info("6. Click on Save on New Log Entry form again");
            LogEntryDetailPage.btnSaveLogEntry.Click();

            Log.Info("Verify that The Duplicate Record pop-up opens");
            DriverUtils.wait(1);            
            Assert.IsTrue(LogEntryDetailPage.divDialogContent.IsDisplayed(), "The pop-up is not opened");

            Log.Info("7. Select on 'Save New Entry' button on the pop-up");
            LogEntryDetailPage.btnSaveNewEntry.Click();

            Log.Info("Verify that Save successfully pop-up shows");                        
            Assert.IsTrue(LogEntryDetailPage.GetAlertWinText().Equals("Saved successfully!"), "Save successfully pop-up does not show");

            Log.Info("8. Click 'OK' on the successful pop-up");
            LogEntryDetailPage.CloseAlertPopup();

            Log.Info("Verify that Save successfully pop-up closes");            
            Assert.IsFalse(HomePage.isAlertPresent(), "Save successfully pop-up shows");

            Log.Info("Verify that the log entry is saved");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved");

            Log.Info("Delete created log entried");
            //HomePage.DeleteALogEntry(logEntryData.MRN);
            //HomePage.DeleteALogEntry(logEntryData.MRN);
            HomePage.DeleteLogEntries(2, "top");
            #endregion New page

            #region Edit page
            Log.Info("9. Go to New Log Entry -> Team again");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Create new log entry for a team member that already had a log entry");
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team");
            LogEntryDetailPage.btnSaveLogEntry.Click();

            Log.Info("Verify that duplicate record pop-up message opens with correct content");
            Assert.IsTrue(LogEntryDetailPage.CheckDuplicatePopup(logEntryData.Name + " " + logEntryData.MRN), "Duplicate popup content is incorrect");

            Log.Info("Select Edit Existing");
            LogEntryDetailPage.btnEditExisting.Click();

            Log.Info("Verify that The Duplicate Record pop-up closes");
            Assert.IsFalse(LogEntryDetailPage.divDialogContent.IsDisplayed(), "The pop-up is not closed");

            Log.Info("Verify that the user is on Edit page");
            pageTitle = "Infection Log Entry for Team Member";            
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The user does not remains on New Log Entry");

            Log.Info("Make some change");
            logEntryData.Symptoms = logEntryData.Symptoms + "changed";
            logEntryData.Comments = logEntryData.Comments + "changed";
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");

            Log.Info("Save the changes");
            LogEntryDetailPage.btnSaveLogEntry.Click();

            Log.Info("Verify that the user is back to Home page");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The user is not back to Homepage");

            Log.Info("Verify that the corresponding existing log entry is saved successfully");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved");

            Log.Info("Delete created log entried");
            System.Console.WriteLine("MRN IS: " + logEntryData.MRN);
            HomePage.DeleteLogEntries(1, "top");
            #endregion Edit page

            #endregion Team

            #region Resident

            #endregion Resident

            #region Client

            #endregion
            #endregion
        }
    }
}
