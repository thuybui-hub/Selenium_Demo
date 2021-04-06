using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.DataObject;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]    
    public class PBI_23875: TestBase
    {
        [Test]
        [Description("Edit Log Functionality")]
        public void PBI_23875_AT_23892()
        {
            #region Test Data
            LogEntryData logEntryData;
            string pageTitle = "Infection Log Entry for Team Member";
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

            Log.Info("3. Open above log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that Edit Log Entry displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that format on edit page displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesEditUIDisplayCorrectly(), "Format on the page is incorrect");
            #endregion Team

            #region Resident
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("3. Open above log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that Edit Log Entry displays");
            pageTitle = "Infection Log Entry for Resident";
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that format on edit page displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesEditUIDisplayCorrectly(), "Format on the page is incorrect");
            #endregion Resident

            #region Client
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("3. Open above log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that Edit Log Entry displays");
            pageTitle = "Infection Log Entry for Ageility Client";
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that format on edit page displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesEditUIDisplayCorrectly(), "Format on the page is incorrect");
            #endregion Client

            Log.Info("Clean up: Delete log entries created");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteLogEntries(3, "top");
            #endregion
        }
    }
}
