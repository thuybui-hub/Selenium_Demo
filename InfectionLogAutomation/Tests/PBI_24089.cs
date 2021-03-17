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
    public class PBI_24089 : TestBase
    {
        [Test]
        [Description("IO Log - Bulk Processing Functionality")]
        public void PBI_24089_AT_24107()
        {
            #region Test data
            #endregion Test data

            #region Main steps
            #region Bulk Processing - Insert Team
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that the file provide us with following information:: Community, Date of Testing, Team Member/Resident");

            Log.Info("Perform a bulk inserting for a community");

            Log.Info("Go to Home page and notice all uploaded records");

            Log.Info("Try to edit uploaded records");

            Log.Info("Verify that uploaded records are able to edit");

            Log.Info("Log out and log in with different business rules: DVP, DDHR, DDH, …");
            #endregion Bulk Processing - Insert Team

            #region Bulk Processing - Insert Resident
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the file provide us with following information:: Community, Date of Testing, Team Member/Resident");

            Log.Info("Perform a bulk inserting for a community");

            Log.Info("Go to Home page and notice all uploaded records");

            Log.Info("Try to edit uploaded records");

            Log.Info("Verify that uploaded records are able to edit");

            Log.Info("Log out and log in with different business rules: DVP, DDHR, DDH, …");
            #endregion Bulk Processing - Insert Resident
            #endregion Main steps
        }
    }
}
