using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23873: TestBase
    {
        [Test]
        [Description("Home Screen Functionality - Dashboard")]
        public void PBI_23873_AT_23883()
        {
            #region Test Data
            string divisionHeader = "Division";
            string regionHeader = "Region";
            string businessUnitHeader = "BU";
            string communityHeader = "Community";
            string IDHeader = "ID";
            string nameHeader = "Name";
            string residentLOBHeader = "Resident LOB";
            string infectionTypeHeader = "Infection Type";
            string testStatusHeader = "Test Status";
            string dispositionHeader = "Disposition";
            string entryTypeHeader = "Entry Type";
            string pageTitle = "Infection Log Entry for Team Member";
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to New Log Entry -> Team page");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            //LogEntryDetailPage.spnTestingStatus.Click();
            List<string> test = LogEntryDetailPage.GetItemsFromControlList(Fields.testStatus);
            #endregion
        }
    }
}
