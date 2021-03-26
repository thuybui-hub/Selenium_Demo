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
    public class PBI_25305 : TestBase
    {
        [Test]
        [Description("IO Log: Bulk Insert Record UI Enhancements: Team")]
        public void PBI_25305_AT_25310()
        {
            #region Test data
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format are incorrect.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }

        [Test]
        [Description("IO Log: Bulk Insert Record UI Enhancements: Resident")]
        public void PBI_25305_AT_25311()
        {
            #region Test data
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Residents"));

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format are incorrect.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
