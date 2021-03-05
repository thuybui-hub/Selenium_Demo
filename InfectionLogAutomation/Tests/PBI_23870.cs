using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23870: TestBase
    {
        [Test]
        [Description("IO Log - New Log Entry Functionality")]
        public void PBI_23870_AT_23885()
        {
            #region Test Data

            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to New Log Entry -> Team page");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Team Members displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Team Members"), "New Infection Log Entry for Team Members form does not display");

            Log.Info("Verify that UI displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesUIWithFormatDisplayCorrectly("Team"), "New Infection Log Entry UI displays incorrectly");

            Log.Info("Go to New Log Entry -> Resident page");
            LogEntryDetailPage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Residents displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Residents"), "New Infection Log Entry for Residents form does not display");

            Log.Info("Verify that UI displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesUIWithFormatDisplayCorrectly("Resident"), "New Resident Log Entry UI displays incorrectly");

            Log.Info("Go to New Log Entry -> Ageility Client page");
            LogEntryDetailPage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Ageility Clients displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Ageility Clients"), "New Infection Log Entry for Ageility Clients form does not display");

            Log.Info("Verify that UI displays correctly");
            Assert.IsTrue(LogEntryDetailPage.DoesUIWithFormatDisplayCorrectly("Client"), "New Infection Log Entry for Ageility Clients UI displays incorrectly");

            #endregion
        }
    }
}
