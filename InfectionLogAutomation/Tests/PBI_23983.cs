using InfectionLogAutomation.DataObject;
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
    public class PBI_23983 : TestBase
    {
        [Test]
        [Description("IO Log - Implement Read-only once Test Status = 'Confirmed': New log entry")]
        public void PBI_23983_AT_23992()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryInfo = new TeamLogEntryInfo();
            teamLogEntryInfo.CurrentTestStatus = "Tested - Confirmed";
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4. Fill all required fields with Current Test Status = 'Tested - Confirmed'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", teamLogEntryInfo.CurrentTestStatus);

            Log.Info("5. Submit the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("6. Open the log entry at step #5");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is able to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all: RDO, RDHR, Senior RDO (sp-test54)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (DVP, DDHR: sp-test51)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");
            #endregion Main steps
        }

        [Test]
        [Description("IO Log - Implement Read-only once Test Status = 'Confirmed': Edit log entry")]
        public void PBI_23983_AT_23993()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryInfo = new TeamLogEntryInfo();
            teamLogEntryInfo.CurrentTestStatus = "Not Tested";
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Main steps
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", teamLogEntryInfo.CurrentTestStatus);
            LogEntryDetailPage.SaveLogEntry();
            DriverUtils.CloseDrivers();
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("3. Open the Team log entry at precondition");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("4. Try to edit Current Test Status field with all status, except 'Tested - Confirmed'");
            teamLogEntryInfo.CurrentTestStatus = "Tested - Pending";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", teamLogEntryInfo.CurrentTestStatus);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("5. Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("6. Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("7. Update lof entry with Current Test Status 'Tested - Confirmed'");
            teamLogEntryInfo.CurrentTestStatus = "Tested - Confirmed";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", teamLogEntryInfo.CurrentTestStatus);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("8. Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("9. Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is able to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all: RDO, RDHR, Senior RDO (sp-test54)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (DVP, DDHR: sp-test51)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");
            #endregion Main steps
        }

        [Test]
        [Description("IO Log - Implement Read-only: Resident: Test Status = 'Confirmed': New log entry")]
        public void PBI_23983_AT_24218()
        {
            #region Test data
            ResidentLogEntryInfo residentLogEntryInfo = new ResidentLogEntryInfo();
            residentLogEntryInfo.CurrentTestStatus = "Tested - Confirmed";
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with Resident Submittor (sp-test55)");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("4. Fill all required fields with Current Test Status = 'Tested - Confirmed'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", residentLogEntryInfo.CurrentTestStatus);

            Log.Info("5. Submit the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("6. Open the log entry at step #5");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is able to be editted.");

            Log.Info("Logout and Login with users who are members of admin or community admin application groups (e.g. sp-test52)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");
            #endregion Main steps
        }

        [Test]
        [Description("IO Log - Implement Read-only: Resident: Test Status = 'Confirmed': Edit log entry")]
        public void PBI_23983_AT_24219()
        {
            #region Test data
            ResidentLogEntryInfo residentLogEntryInfo = new ResidentLogEntryInfo();
            residentLogEntryInfo.CurrentTestStatus = "Not Tested";
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Main steps
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", residentLogEntryInfo.CurrentTestStatus);
            LogEntryDetailPage.SaveLogEntry();
            DriverUtils.CloseDrivers();
            #endregion Pre-condition

            #region Main steps
            Log.Info("Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Login with Resident Submittor (sp-test55)");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open the Team log entry at precondition");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Try to edit Current Test Status field with all status, except 'Tested - Confirmed'");
            residentLogEntryInfo.CurrentTestStatus = "Tested - Pending";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", residentLogEntryInfo.CurrentTestStatus);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Update lof entry with Current Test Status 'Tested - Confirmed'");
            residentLogEntryInfo.CurrentTestStatus = "Tested - Confirmed";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", residentLogEntryInfo.CurrentTestStatus);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is able to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (DVP, DDHR: sp-test51)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Test Status field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Test Status"), "Test Status field is unable to be editted.");
            #endregion Main steps
        }
    }
}
