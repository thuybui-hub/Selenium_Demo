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
    public class PBI_23868 : TestBase
    {
        [Test]
        [Description("Home Screen Display - Dashboard")]
        public void PBI_23868_AT_23882()
        {
            #region Test Data
            List<string> expectedListOfColumnsHeader;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page does not display");
            Assert.IsTrue(HomePage.tblDashboardTableHeader.IsDisplayed(), "Home page does not contain dashboard/table of infections");

            Log.Info("Get list of columns on Dashboard table");
            expectedListOfColumnsHeader = HomePage.tblDashboardTableHeader.GetAllColumnsHeader();
            expectedListOfColumnsHeader.Sort();
            Constants.DashboardColumnsHeader.Sort();

            Log.Info("Verify that the dashboard/table displays correct columns");
            Assert.IsTrue(expectedListOfColumnsHeader.SequenceEqual(Constants.DashboardColumnsHeader), "The dashboard table displays incorrect columns");
            #endregion
        }
    }
}
