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
    public class PBI_23935: TestBase
    {
        [Test]
        [Description("IO Log - Add Records of Terminated Employees to Drop Down List: New Log Entry")]
        public void PBI_23935_AT_24020()
        {
            #region Test Data
            LogEntryData logEntryData = JsonParser.Get<LogEntryData>();
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that Advanced Search button displays on New log entry form");
            Assert.IsTrue(LogEntryDetailPage.btnAdvancedSearch.IsDisplayed(), "Advanced Search button does not display");

            Log.Info("Leave all fields on new log entry form blank and verify that Advanced Search button is disabled for searching");
            Assert.IsFalse(LogEntryDetailPage.btnAdvancedSearch.IsEnabled(), "The button is enabled for searching");            

            Log.Info("4. Select a region only");

            Log.Info("Verify that Advanced Search button is disabled for searching");
            Assert.IsFalse(LogEntryDetailPage.btnAdvancedSearch.IsEnabled(), "The button is enabled for searching");

            Log.Info("5. Select a community");

            Log.Info("Verify that Advanced Search button is enable for searching");
            Assert.IsTrue(LogEntryDetailPage.btnAdvancedSearch.IsEnabled(), "The button is enabled for searching");
            #endregion
        }
    }
}
