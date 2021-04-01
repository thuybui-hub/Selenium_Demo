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
    public class PBI_24066 : TestBase
    {
        #region Test data
        string searchValue = "test";
        #endregion Test data

        [Test]
        [Description("IO Log - clear the filters")]
        public void PBI_24066_AT_24108()
        {
            #region Admin user: gnguyen
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account who have access to multiple entry types: - Admin role: gnguyen");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Home screen with all log entries displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page is not shown as default.");

            Log.Info("Verify that there is a button Clear Filters on tool bar of Dashboard table");
            Assert.IsTrue(HomePage.btnClearFilters.IsDisplayed(), "Clear button does not exist on Home screen.");

            Log.Info("4.1. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Team Member");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Table shows incorrect records.");

            Log.Info("5.1. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            DriverUtils.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");
            Assert.IsTrue(HomePage.IsFilterDataPreservedAfterMovingToAnotherForm("Entry Type", "Team Member"), "Filter data is not preserved after moving to another form.");

            Log.Info("6.1. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");
            Assert.IsTrue(HomePage.IsAllFilterDataCleared(), "Filter data are not cleared.");

            Log.Info("4.2. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Resident");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Resident"), "Table shows incorrect records.");

            Log.Info("5.2. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            DriverUtils.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");
            Assert.IsTrue(HomePage.IsFilterDataPreservedAfterMovingToAnotherForm("Entry Type", "Resident"), "Filter data is not preserved after moving to another form.");

            Log.Info("6.2. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");
            Assert.IsTrue(HomePage.IsAllFilterDataCleared(), "Filter data are not cleared.");

            Log.Info("4.3. Filter Dashboard table on Home page by Team Member/Resident/Client");
            HomePage.FilterATableColumn("Entry Type", "Ageility Client");

            Log.Info("Verify that Dashboard table shows records for Team Member/Resident/Client only");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Ageility Client"), "Table shows incorrect records.");

            Log.Info("5.3. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            DriverUtils.BackToPreviousPage();

            Log.Info("Verify that filter data is preserved when moving to another form.");
            Assert.IsTrue(HomePage.IsFilterDataPreservedAfterMovingToAnotherForm("Entry Type", "Ageility Client"), "Filter data is not preserved after moving to another form.");

            Log.Info("6.3. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the filtering data is cleared and Dashboard table shows all records for Team Members, Resident and Client");
            Assert.IsTrue(HomePage.IsAllFilterDataCleared(), "Filter data are not cleared.");

            Log.Info("7. Perform a search");
            HomePage.PerformASearchOnDashboardTable(searchValue);

            Log.Info("Verify that Dashboard table shows records for searching data only");
            Assert.IsTrue(HomePage.DoesSearchResultDisplaysCorrectly(searchValue), "Dashboard table show incorrect records.");

            Log.Info("8. Go to New Log Entry form and press on browser back button");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            DriverUtils.BackToPreviousPage();

            Log.Info("Verify that Search box is not preserved when moving to another form.");
            Assert.IsTrue(HomePage.IsSearchDataonDashboardTableCleared(), "Search box is not cleared. ");

            Log.Info("9. Perform a search");
            HomePage.PerformASearchOnDashboardTable(searchValue);

            Log.Info("10. Click on Clear Filters button");
            HomePage.ClearAllFilters();

            Log.Info("Verify that the searching data is cleared and Dashboard table shows all records for Team Members, Resident and Client");
            Assert.IsTrue(HomePage.IsSearchDataonDashboardTableCleared(), "Search box is not cleared. ");
            #endregion Admin user: gnguyen

            #region ​Team Admin: sp-test51
            Log.Info("6.1. Logout and login with Team Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​Team Admin: sp-test51

            #region ​Resident Admin: sp-test52
            Log.Info("6.2. Logout and login with Resident Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​Resident Admin: sp-test52

            #region ​​​Client Admin: sp-test53
            //Log.Info("6.3. Logout and login with Client Admin account");
            //DriverUtils.CloseDrivers();
            //DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            //DriverUtils.GoToUrl(Constants.Url);
            //LoginPage.Login(Constants.ClientAdminUser, Constants.CommonPassword);

            //Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            //Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​​Client Admin: sp-test53

            #region ​Team Community Admin / Resident Community Submittor: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​Team Community Admin / ​Resident Community Submittor: sp-test54

            #region Resident Community Admin: sp-test55
            Log.Info("6.5. Logout and login with Resident Community Admin account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion Resident Community Admin: sp-test55

            #region ​Client Submittor: sp-test56
            Log.Info("6.6. Logout and login with Client Submittor");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ClientSubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion Client Submittor: sp-test56

            #region ​Team Community Submittor: sp-test57
            Log.Info("6.7. Logout and login with Team Community Submittor account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​Team Community Submittor: sp-test57

            #region ​Resident Read Only: sp-test58
            Log.Info("6.8. Logout and login with ​Resident Read Only account");
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that Clear Filters button exists on Dashboard table.");
            Assert.IsFalse(HomePage.btnClearFilters.IsDisplayed(), "Clear Filters button does not exist on Dashboard table.");
            #endregion ​Resident Read Only: sp-test58
        }
    }
}
