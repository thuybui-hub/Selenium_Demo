using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23924 : TestBase
    {

        #region Test data

        #endregion Test data

        [Test]
        [Description("IO Log - Home Screen - Filter by entry type")]
        public void PBI_23924_AT_23961()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account who have access to multiple entry types - Admin role: gnguyen");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Home screen with all log entries displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that a filter exists on the home screen which filters the grid by Entry Type: Team Member, Resident, Ageility Client");

            Log.Info("Filter Dashboard table on Home page by Team Member");

            Log.Info("Verify that Dashboard table shows records for Team Member only");

            Log.Info("Filter Dashboard table on Home page by Resident");

            Log.Info("Verify that Dashboard table shows records for Redient only");

            Log.Info("Filter Dashboard table on Home page by Ageility Client");

            Log.Info("Verify that Dashboard table shows records for Ageility Client only");

            Log.Info("Log out and log in with account who does not have access to multiple entry types");

            Log.Info("Verify that there is not filter for Entry Type on home screen");

            Log.Info("");
        }
    }
}
