using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23924 : TestBase
    {

        #region Test data
        List<string> expectedEntryTypeFilterList = new List<string> { "Team Member", "Resident", "Ageility Client" };
        #endregion Test data

        [Test]
        [Description("IO Log - Home Screen - Filter by entry type")]
        public void PBI_23924_AT_23961()
        {
            #region Admin user: gnguyen
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account who have access to multiple entry types - Admin role: gnguyen");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home screen with all log entries displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that a filter exists on the home screen which filters the grid by Entry Type: Team Member, Resident, Ageility Client");
            Assert.IsTrue(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(expectedEntryTypeFilterList), "Entry Type filter does not exist or exist with wrong expected list options.");

            Log.Info("3. Filter Dashboard table on Home page by Team Member");
            HomePage.FilterATableColumn("Entry Type", "Team Member");

            Log.Info("Verify that Dashboard table shows records for Team Member only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Table shows incorrect records.");

            Log.Info("4. Filter Dashboard table on Home page by Resident");
            HomePage.FilterATableColumn("Entry Type", "Resident");

            Log.Info("Verify that Dashboard table shows records for Redient only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Resident"), "Table shows incorrect records.");

            Log.Info("5. Filter Dashboard table on Home page by Ageility Client");
            HomePage.FilterATableColumn("Entry Type", "Ageility Client");

            Log.Info("Verify that Dashboard table shows records for Ageility Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Ageility Client"), "Table shows incorrect records.");
            #endregion Admin user: gnguyen

            #region ​Team Admin: sp-test51
            Log.Info("6.1. Logout and login with Team Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion ​Team Admin: sp-test51

            #region ​Resident Admin: sp-test52
            Log.Info("6.2. Logout and login with Resident Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion ​Resident Admin: sp-test52

            #region ​​​Client Admin: sp-test53
            Log.Info("6.3. Logout and login with Client Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ClientAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion ​​Client Admin: sp-test53

            #region ​Team Community Admin / Resident Community Submittor: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that a filter exists on the home screen which filters the grid by Entry Type: Team Member, Resident, Ageility Client");
            Assert.IsTrue(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(expectedEntryTypeFilterList), "Entry Type filter does not exist or exist with wrong expected list options.");
            #endregion ​Team Community Admin / ​Resident Community Submittor: sp-test54

            #region Resident Community Admin: sp-test55
            Log.Info("6.5. Logout and login with Resident Community Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion Resident Community Admin: sp-test55

            #region ​Client Submittor: sp-test56
            Log.Info("6.6. Logout and login with Client Submittor");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ClientSubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion Client Submittor: sp-test56

            #region ​Team Community Submittor: sp-test57
            Log.Info("6.7. Logout and login with Team Community Submittor account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion ​Team Community Submittor: sp-test57

            #region ​Resident Read Only: sp-test58
            Log.Info("6.8. Logout and login with ​Resident Read Only account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that Home screen displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that Entry Type filter does not exist on the home screen.");
            Assert.IsFalse(HomePage.DoesEntryTypeFilterExistWithExpectedOptions(), "Entry Type filter exists on Home screen.");
            #endregion ​Resident Read Only: sp-test58
        }
    }
}
