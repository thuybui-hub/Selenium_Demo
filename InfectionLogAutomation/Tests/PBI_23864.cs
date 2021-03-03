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
    public class PBI_23864 : TestBase
    {
        [Test]
        [Description("New Log Entry Display")]
        public void PBI_23864_AT_23886()
        {
            #region Test Data
            List<string> acTestStatusList, acDispositionList;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region New Team Log Entry
            Log.Info("3.1. Go to New Log Entry -> Team page");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Team Members displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Team Members"), "New Infection Log Entry for Team Members form does not display");

            Log.Info("Get items from Test Status dropdown list");
            acTestStatusList = LogEntryDetailPage.GetItemsFromControlList(Fields.testStatus);
            acTestStatusList.Sort();
            Constants.TestStatus.Sort();

            Log.Info("Verify that Test Status drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");

            Log.Info("Get items from Disposition dropdown list");
            acDispositionList = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);
            acDispositionList.Sort();
            Constants.Disposition.Sort();

            Log.Info("Verify that Disposition drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");
            #endregion New Team Log Entry

            #region Resident
            Log.Info("3.1. Go to New Log Entry -> Resident page");
            LogEntryDetailPage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Resident displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Residents"), "New Infection Log Entry for Residents form does not display");

            Log.Info("Get items from Test Status dropdown list");
            acTestStatusList = LogEntryDetailPage.GetItemsFromControlList(Fields.testStatus);
            acTestStatusList.Sort();
            Constants.TestStatus.Sort();

            Log.Info("Verify that Test Status drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");

            Log.Info("Get items from Disposition dropdown list");
            acDispositionList = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);
            acDispositionList.Sort();
            Constants.Disposition.Sort();

            Log.Info("Verify that Disposition drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");
            #endregion

            #region Ageility Client
            Log.Info("3.1. Go to New Log Entry -> Resident page");
            LogEntryDetailPage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Client displays");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Ageility Clients"), "New Infection Log Entry for Ageility Clients form does not display");

            Log.Info("Get items from Test Status dropdown list");
            acTestStatusList = LogEntryDetailPage.GetItemsFromControlList(Fields.testStatus);
            acTestStatusList.Sort();
            Constants.TestStatus.Sort();

            Log.Info("Verify that Test Status drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");

            Log.Info("Get items from Disposition dropdown list");
            acDispositionList = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);
            acDispositionList.Sort();
            Constants.Disposition.Sort();

            Log.Info("Verify that Disposition drop down list displays items correctly");
            Assert.IsTrue(acTestStatusList.SequenceEqual(Constants.TestStatus), "Test Status drop down list displays incorrectly");
            #endregion
            #endregion
        }
    }
}
