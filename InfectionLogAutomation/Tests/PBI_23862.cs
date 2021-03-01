using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System.Linq;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23862 : TestBase
    {
        #region Test data
        private readonly LoginData loginData = JsonParser.Get<LoginData>();
        List<string> expectedNewLogEntrySubMenuList = new List<string> { "Team", "Resident", "Ageility Client" };
        List<string> expectedReportsSubMenuList = new List<string> { "Resident Case Log" };
        List<string> actualNewLogEntrySubMenuList, actualReportsSubMenuList;
        #endregion Test data

        [Test]
        [Description("Navigation: General UI")]
        public void PBI_23862_AT_23878()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("Verify that Infectious Outbreak page shows");
            Assert.IsTrue(CommonPage.IsInfectionLogPageDiplayed(), "Infectious Outbreak page does not display.");

            Log.Info("Verify that there are 3 tabs on the top menu bar: Home, New Log Entry, Reports; Home is default tab");
            Assert.IsTrue(CommonPage.AreThere5TabsOnMainMenu(), "Menu does not includes these 3 tabs: Home, New Log Entry, Reports");
        }

        [Test]
        [Description("Navigation: Home tab")]
        public void PBI_23862_AT_23879()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("Home page is shown as default");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that there is dashboard/table of all infections on Home page.");
            Assert.IsTrue(HomePage.IsILogTableDisplayed(), "Dashboard table does not display.");
        }

        [Test]
        [Description("Navigation: New Log Entry tab")]
        public void PBI_23862_AT_23880()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("3. Click on New Log Entry tab on the top menu bar");
            CommonPage.SelectMenuItem("New Log Entry");
            DriverUtils.WaitForPageLoad();

            Log.Info("Verify that New Log Entry has menu with 3 options: Team, Resident, and Ageility Client.");
            actualNewLogEntrySubMenuList = CommonPage.GetSubMenuItems("New Log Entry");
            Assert.IsTrue(actualNewLogEntrySubMenuList.SequenceEqual(expectedNewLogEntrySubMenuList), "New Log Entry menu does not include these 3 options: Team, Resident, Ageility Client");

            Log.Info("5. Select Team option");
            CommonPage.SelectMenuItem("Team");

            Log.Info("Verify that unfilled 'Infectious Outbreak Log Entry' form for completion, customized is opened for Team Members");
            //

            Log.Info("6. Select Resident option");
            CommonPage.SelectMenuItem("New Log Entry|Resident");

            Log.Info("Verify that unfilled 'Infectious Outbreak Log Entry' form for completion, customized is opened for Residents");
            //

            Log.Info("7. Select Ageility Client option");
            CommonPage.SelectMenuItem("New Log Entry|Ageility Client");

            Log.Info("Verify that unfilled 'Infectious Outbreak Log Entry' form for completion, customized is opened for Clients");
            //
        }

        [Test]
        [Description("Navigation: Report tab")]
        public void PBI_23862_AT_23881()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("3. Click on Reports tab on the top menu bar");
            CommonPage.SelectMenuItem("Reports");

            Log.Info("Verify that Reports has one option: Resident Case Log");
            actualReportsSubMenuList = CommonPage.GetSubMenuItems("Reports");
            Assert.IsTrue(actualReportsSubMenuList.SequenceEqual(expectedReportsSubMenuList), "Reports menu does not include Resident Case Log option.");

            Log.Info("7. Select Resident Case Log");
            //CommonPage.SelectMenuItem("Resident Case Log");

            Log.Info("Verify that Resident Case Log report opens");
            //Unable to check this on local machine
        }
    }
}
