using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.DataObject;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23874: TestBase
    {
        [Test]
        [Description("Edit Log Display")]
        public void PBI_23874_AT_23891()
        {
            #region Test Data
            LogEntryData logEntryData;            
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

            Log.Info("Verify that edit log entry page displays");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "The edit page does not display");
            #endregion Team

            #region Resident
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Open above log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that edit log entry page displays");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "The edit page does not display");
            #endregion Resident

            #region Client
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("5. Open above log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that edit log entry page displays");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Client"), "The edit page does not display");
            #endregion Client

            Log.Info("Clean up: Delete log entries created");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteLogEntries(3, "top");
            #endregion
        }
    }
}
