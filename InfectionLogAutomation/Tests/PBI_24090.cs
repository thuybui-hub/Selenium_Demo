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
    public class PBI_24090 : TestBase
    {
        #region Test data
        List<string> expectedBulkProcessingSubMenuList = new List<string> { "Insert Team", "Insert Resident" };
        List<string> actualBulkProcessingSubMenuList;
        #endregion Test data

        [Test]
        [Description("IO Log - Bulk Processing UI")]
        public void PBI_24090_AT_24139()
        {
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account who have access to multiple entry types: - Admin role: gnguyen");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Notice to menu.");
            CommonPage.SelectMenuItem("Bulk Processing");
            DriverUtils.WaitForPageLoad();

            Log.Info("Verify that top menu includes Bulk Processing -> Insert Team/ Insert Resident");
            actualBulkProcessingSubMenuList = CommonPage.GetSubMenuItems("Bulk Processing");
            Assert.IsTrue(expectedBulkProcessingSubMenuList.All(subMenu => actualBulkProcessingSubMenuList.Contains(subMenu)), "Bulk Processing menu does not include these 2 options: Insert Team, Insert Resident");

            Log.Info("4. Go to Bulk Processing -> Insert Team");
            HomePage.SelectMenuItem("Insert Team");

            Log.Info("Verify that Bulk Insert page with title Bulk Infection Log Entry for Team Members includes: Region, Community, Testing Date, and some default data.");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format displays incorrectly.");

            Log.Info("5. Go to Bulk Insert -> Insert Resident");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that Bulk Insert page with title Bulk Infection Log Entry for Team Members includes: Region, Community, Testing Date, and some default data.");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format displays incorrectly.");
        }
    }
}
