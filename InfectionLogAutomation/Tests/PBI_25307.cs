using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_25307: TestBase
    {
        [Test]
        [Description("IO Log: All Record Search Updates: Search page UI")]
        public void PBI_25307_AT_25312()
        {
            #region Test Data
            string from = DateTime.Now.AddDays(2).Date.ToString();
            List<string> community = new List<string>() { "Rio Las Palmas" };
            List<string> filters = new List<string>() { "Active", "Inactive" };            
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            // Test section
            //HomePage.OpenALogEntry(0);
            //LogEntryDetailPage.Test();
            // end           

            Log.Info("Verify that Home page displays correctly");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("Verify that default search values are correct");
            Assert.IsTrue(HomePage.AreDefaultSearchValuesCorrect(), "The default search values display incorrectly");

            Log.Info("Verify that users can search with one or more communities");
            Assert.IsTrue(HomePage.IsCommunityMultiselect(), "Unable to select more communities");

            Log.Info("Verify that Filters field is correct");
            Assert.IsTrue(HomePage.IsFiltersFieldCorrect(), "The Filters field is incorrect");

            Log.Info("Verify that Last Updated \"from - to\" date range and selected from a calendar");
            Assert.IsTrue(HomePage.AreDateSelectedFromCalendar(), "Last Updated dates are not selected from calendar");            

            Log.Info("3. Enter Last Updated dates and From  is later than To");
            HomePage.FillSearchCriterias("", from);

            Log.Info("Verify that an alert message shows to tell that Last Updated From should not be later than To");
            Assert.IsTrue(HomePage.GetAlertWinText().Contains("Last Updated From Date should not be later than To Date"));
            HomePage.CloseAlertPopup();

            Log.Info("4. Perform search criteria");
            from = DateTime.Now.AddDays(-5).Date.ToString();
            HomePage.PerformASearchCriteria(community, from, null, filters);

            Log.Info("5. Click on Reset button");
            HomePage.ResetSearchCriteria();

            Log.Info("Verify that Reset button restores the search criteria to default settings");
            bool test = HomePage.AreSearchCriteriasResetted();
            Assert.IsTrue(HomePage.AreSearchCriteriasResetted(), "The search criterias are not set to default setting");
            #endregion
        }
    }
}
