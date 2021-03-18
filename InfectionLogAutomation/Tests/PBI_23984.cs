using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

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
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4. Fill all required fields with Current Disposition= 'Expired'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", teamLogEntryData.CurrentDisposition);

            Log.Info("5. Submit the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("6. Open the log entry at step #5");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is able to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all: RDO, RDHR, Senior RDO (sp-test54)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (DVP, DDHR: sp-test51)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(outLstResult[3]);
            #endregion Clean up
        }

        [Test]
        [Description("IO Log - Implement Read-only Once Current Disposition= 'Expired': Edit log entry")]
        public void PBI_23984_AT_23995()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryData = new TeamLogEntryInfo();
            teamLogEntryData.CurrentDisposition = "Quarantined";
            List<string> outLstResult = new List<string> { };
            #endregion Test data

            #region Pre-condition
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out outLstResult);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", teamLogEntryData.CurrentDisposition);
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

            Log.Info("4. Try to edit Current Disposition field with all status, except 'Expired'");
            teamLogEntryData.CurrentDisposition = "Hospitalized";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", teamLogEntryData.CurrentDisposition);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("5. Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("6. Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("7. Update lof entry with Current Disposition 'Expired'");
            teamLogEntryData.CurrentDisposition = "Expired";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", teamLogEntryData.CurrentDisposition);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("8. Update the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("9. Reopen the log entry at step #4");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can no longer be updated");
            Assert.IsTrue(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is able to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all: RDO, RDHR, Senior RDO (sp-test54)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (DVP, DDHR: sp-test51)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");

            Log.Info("Logout and Login with users who are members of the application role V/E/D all (Admin Role: gnguyen)");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open the log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("Verify that Current Disposition field can be updated");
            Assert.IsFalse(LogEntryDetailPage.IsTestStatusOrDispositionUnableToBeEditted("Disposition"), "Disposition field is unable to be editted.");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(outLstResult[3]);
            #endregion Clean up
        }

    }
}
