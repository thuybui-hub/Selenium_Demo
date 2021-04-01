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
    public class PBI_24147 : TestBase
    {
        [Test]
        [Description("IO Log -Security Updates(Matrix): Team")]
        public void PBI_24147_AT_24173()
        {
            #region Test data
            #endregion Test data

            #region Main steps
            #region Team Admin user: sp-test51
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test51)");
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for all comunties.");

            Log.Info("Verify that: Admin member is able to add new team log entry for any community");

            Log.Info("Verify that: Admin member is able to edit an existing team log entry of any community");

            Log.Info("Verify that: Admin member is able to delete an existing team log entry of any communty");

            Log.Info("Verify that: Admin member is able to perform 'Initiate Bulk Insert' for all communities");
            #endregion Team Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Admin user: sp-test54
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test54)");
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for their commmunties.");

            Log.Info("Verify that: Community Admin member is able to add new team log entry for their communities");

            Log.Info("Verify that: Community Admin member is able to edit an existing team log entry for their communities");

            Log.Info("Verify that: Community Admin member is able to delete an existing team log entry for their communities");

            Log.Info("Verify that: Community Admin member is able to perform 'Initiate Bulk Insert' for their communities");
            #endregion Team Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Submitter user: sp-test57
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test57)");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for their commmunties.");

            Log.Info("Verify that: Community Submitter member is able to add new team log entry for their communities");

            Log.Info("Verify that: Community Submitter member is able to edit an existing team log entry for their communities");

            Log.Info("Verify that: Community Submitter member is unable to delete an existing team log entry for their communities");

            Log.Info("Verify that: Community Submitter member is able to perform 'Initiate Bulk Insert' for their communities");
            #endregion Team Community Submitter

            //DriverUtils.CloseDrivers();
            //DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Read Only user: TBD
            //Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            //DriverUtils.GoToUrl(Constants.Url);

            //Log.Info("2. Log in with account (TBD)");
            //LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            //Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for all comunties");

            //Log.Info("Verify that: Read only member is unable to add new team log entry");

            //Log.Info("Verify that: Read only member is unable to edit an existing team log entry");

            //Log.Info("Verify that: Read only member is unable to delete an existing team log entry");

            //Log.Info("Verify that: Read only member is unable to perform 'Initiate Bulk Insert'");
            #endregion Read Only user
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }

        [Test]
        [Description("IO Log - Security Updates (Matrix): Resident")]
        public void PBI_24147_AT_24174()
        {
            #region Test data
            #endregion Test data

            #region Main steps
            #region Resident Admin user: sp-test52
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test52)");
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for all comunties.");

            Log.Info("Verify that: Admin member is able to add new resident log entry for any community");

            Log.Info("Verify that: Admin member is able to edit an existing resident log entry of any community");

            Log.Info("Verify that: Admin member is able to delete an existing resident log entry of any communty");

            Log.Info("Verify that: Admin member is able to perform 'Initiate Bulk Insert' for all communities");

            Log.Info("Verify that: Admin member is able to view/add/edit and delete attachments for a resident log entry");

            Log.Info("Verify that: Admin member is able to View Case Log Report");
            #endregion Resident Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Community Admin user: sp-test55
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test55)");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for their comunties.");

            Log.Info("Verify that: Community Admin member is able to add new resident log entry for their communities");

            Log.Info("Verify that: Community Admin member is able to edit an existing resident log entry for their community");

            Log.Info("Verify that: Community Admin member is able to delete an existing resident log entry for their communties");

            Log.Info("Verify that: Community Admin member is able to view/add/edit and delete attachments for a resident log entry");

            Log.Info("Verify that: Community Admin member is unable to View Case Log Report");

            Log.Info("Verify that: Community Admin member is able to perform 'Initiate Bulk Insert' for THEiR ASSIGNED  communities (not all communities)");
            #endregion Resident Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Community Submitter user: sp-test54
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test54)");
            LoginPage.Login(Constants.ResidentCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for their comunties.");

            Log.Info("Verify that: Community submitter member is unable to add new resident log entry for their communities");

            Log.Info("Verify that: Community submitter member is unable to edit an existing resident log entry for their community");
            Assert.IsTrue(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "There is no field to attach a document.");

            Log.Info("Verify that: Community submitter member is unable to delete an existing resident log entry for their communties");
            HomePage.FilterATableColumn("Entry Type", "Resident");
            Assert.IsTrue(HomePage.IsUserUnableToDeleteLogEntry("unable"), "Able to delete log entries");

            Log.Info("Verify that: Community submitter member is unable to view/add/edit and delete attachments for a resident log entry");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("unable"), "There is no field to attach a document.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("unable"), "Able to delete a document.");

            Log.Info("Verify that: Community submitter member is unable to View Case Log Report");

            Log.Info("Verify that: Community submitter member is able to perform 'Initiate Bulk Insert' for their communities AND COMMUNITY SUBMITTER CAN VIEW LOG ENTRIES");
            #endregion Resident Community Submitter

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Read Only user: sp-test58
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test58)");
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that: Read only member is able to view log entries: Dashboard table on Home page shows list of resident log entries for all comunties.");

            Log.Info("Verify that: Read only member is unable to add new resident log entry for all communities");
            Assert.IsTrue(HomePage.IsReadOnlyUserAbleToAddNewRecords("New Log Entry"), "Able to add new records.");

            Log.Info("Verify that: Read only member is unable to edit an existing resident log entry for all community");
            Assert.IsTrue(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "There is no field to attach a document.");

            Log.Info("Verify that: Read only member is unable to delete an existing resident log entry for all communties");
            Assert.IsTrue(HomePage.IsUserUnableToDeleteLogEntry("unable"), "Able to delete log entries");

            Log.Info("Verify that: Read only member is unable to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsTrue(HomePage.IsReadOnlyUserAbleToAddNewRecords("Bulk Processing"), "Able to add new records.");

            Log.Info("Verify that: Read only member is able to view attachments for a resident log entry");

            Log.Info("Verify that: Read only member is unable to add/edit and delete attachments for a resident log entry");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("unable"), "There is no field to attach a document.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("unable"), "Able to delete a document.");

            Log.Info("Verify that: Read only member is able to View Case Log Report");
            HomePage.SelectMenuItem("Reports");
            Assert.IsTrue(HomePage.GetSubMenuItems("Reports ").Contains("Resident Case Log"), "Read Only member is unable to view Resident Case Log report");
            #endregion Resident Read Only user
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
