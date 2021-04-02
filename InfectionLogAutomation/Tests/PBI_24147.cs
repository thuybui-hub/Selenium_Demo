using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.Utilities;
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
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            List<string> actualCommunitiesList, expectedCommunitiesList;
            expectedCommunitiesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "INT049_RPT_MyFVE Communities", "Name", "[INT049_CCF_Division] <> 'Divested Division' AND [INT049_CCF_Division] <> '(Blanks)' AND [INT049_CCF_Division] <> 'Region' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            expectedCommunitiesList.Sort();
            #endregion Test data

            #region Main steps
            #region Team Admin user: sp-test51
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test51)");
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for all communities.");
            HomePage.txtCommunity.Click();
            actualCommunitiesList = HomePage.lstCommunity.GetOptions();
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Admin member is able to add new team log entry for any community");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "New Log Entry", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Team Members"));
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to edit an existing team log entry of any community");
            HomePage.OpenALogEntry(0);
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Unable to Update Log Entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to delete an existing team log entry of any communty");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "Able to delete log entries");

            Log.Info("Verify that: Admin member is able to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));
            HomePage.SelectMenuItem(Constants.BulkEditTeamPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Edit for Team Members"));
            #endregion Team Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Admin user: sp-test54
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test54)");
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for their commmunties.");
            HomePage.txtCommunity.Click();
            actualCommunitiesList = HomePage.lstCommunity.GetOptions();
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Community Admin member is able to add new team log entry for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "New Log Entry", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Team Members"));
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Admin member is able to edit an existing team log entry for their communities");
            HomePage.FilterATableColumn("Entry Type", "Team Member");
            HomePage.OpenALogEntry(0);
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Unable to Update Log Entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Admin member is able to delete an existing team log entry for their communities");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "Able to delete log entries");

            Log.Info("Verify that: Community Admin member is able to perform 'Initiate Bulk Insert' for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));
            HomePage.SelectMenuItem(Constants.BulkEditTeamPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Edit for Team Members"));
            #endregion Team Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Submitter user: sp-test57
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test57)");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of team log entries for their commmunties.");
            HomePage.txtCommunity.Click();
            actualCommunitiesList = HomePage.lstCommunity.GetOptions();
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Community Submitter member is able to add new team log entry for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "New Log Entry", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Team Members"));
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Submitter member is able to edit an existing team log entry for their communities");
            HomePage.OpenALogEntry(0);
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Unable to Update Log Entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Submitter member is unable to delete an existing team log entry for their communities");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("unable"), "Able to delete log entries");

            Log.Info("Verify that: Community Submitter member is able to perform 'Initiate Bulk Insert' for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Team"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));
            HomePage.SelectMenuItem(Constants.BulkEditTeamPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Edit for Team Members"));
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
        }

        [Test]
        [Description("IO Log - Security Updates (Matrix): Resident")]
        public void PBI_24147_AT_24174()
        {
            #region Test data
            LogEntryData logEntryData = new LogEntryData();
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            string excelPathResident = Constants.DataPath + "ResidentsInfo.xls";
            List<string> actualCommunitiesList, expectedCommunitiesList;
            expectedCommunitiesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "INT049_RPT_MyFVE Communities", "Name", "[INT049_CCF_Division] <> 'Divested Division' AND [INT049_CCF_Division] <> '(Blanks)' AND [INT049_CCF_Division] <> 'Region' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            expectedCommunitiesList.Sort();
            string fileName = "LGG testing.txt";
            string filePath = Constants.DataPath + fileName;
            #endregion Test data

            #region Main steps
            #region Resident Admin user: sp-test52
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test52)");
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for all comunties.");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Admin member is able to add new resident log entry for any community");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "New Log Entry", "Resident"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Residents"));
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to edit an existing resident log entry of any community");
            HomePage.OpenALogEntry(0);
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Unable to Update Log Entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to delete an existing resident log entry of any communty");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "Unable to delete log entries");

            Log.Info("Verify that: Admin member is able to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Resident"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            HomePage.SelectMenuItem(Constants.BulkEditResidentPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            BulkInsertPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to view/add/edit and delete attachments for a resident log entry");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            HomePage.OpenALogEntry(0);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("able"), "There is no field to attach a document.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("able"), "Able to delete a document.");
            BulkInsertPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Admin member is able to View Case Log Report");
            Assert.IsTrue(HomePage.IsUserAbleToViewResidentCaseLogReport("able"), "Read Only member is unable to view Resident Case Log report");
            #endregion Resident Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Community Admin user: sp-test55
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test55)");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for their comunties.");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Community Admin member is able to add new resident log entry for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "New Log Entry", "Resident"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("New Infection Log Entry for Residents"));
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Admin member is able to edit an existing resident log entry for their community");
            HomePage.OpenALogEntry(0);
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Unable to Update Log Entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Admin member is able to delete an existing resident log entry for their communties");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "Unable to delete log entries");

            Log.Info("Verify that: Community Admin member is able to view/add/edit and delete attachments for a resident log entry");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            HomePage.OpenALogEntry(0);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("able"), "There is no field to attach a document.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("able"), "Able to delete a document.");
            BulkInsertPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Community Admin member is unable to View Case Log Report");
            Assert.IsTrue(HomePage.IsUserAbleToViewResidentCaseLogReport("unable"), "Read Only member is able to view Resident Case Log report");

            Log.Info("Verify that: Community Admin member is able to perform 'Initiate Bulk Insert' for THEiR ASSIGNED  communities (not all communities)");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Resident"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            HomePage.SelectMenuItem(Constants.BulkEditResidentPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            #endregion Resident Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Community Submitter user: sp-test54
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test54)");
            LoginPage.Login(Constants.ResidentCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that: Dashboard table on Home page shows list of resident log entries for their comunties.");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Community submitter member is unable to add new resident log entry for their communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("unable", "New Log Entry", "Resident"), "Able to add new records.");

            Log.Info("Verify that: Community submitter member is unable to edit an existing resident log entry for their community");
            HomePage.FilterATableColumn("Entry Type", "Resident");
            Assert.IsTrue(HomePage.IsUserUnableToOpenLogEntry(), "Able to open log entry.");

            Log.Info("Verify that: Community submitter member is unable to delete an existing resident log entry for their communties");
            HomePage.FilterATableColumn("Entry Type", "Resident");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("unable"), "Able to delete log entries");

            Log.Info("Verify that: Community submitter member is unable to view/add/edit and delete attachments for a resident log entry");
            // Unable to add new / open existing log entry => unable to view/add/edit/delete attachments

            Log.Info("Verify that: Community submitter member is unable to View Case Log Report");
            Assert.IsTrue(HomePage.IsUserAbleToViewResidentCaseLogReport("unable"), "Read Only member is able to view Resident Case Log report");

            Log.Info("Verify that: Community submitter member is able to perform 'Initiate Bulk Insert' for their communities AND COMMUNITY SUBMITTER CAN VIEW LOG ENTRIES");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("able", "Bulk Processing", "Resident"), "Unable to add new records.");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            HomePage.SelectMenuItem(Constants.BulkEditResidentPath);
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist("Bulk Infection Log Entry for Residents"));
            #endregion Resident Community Submitter

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Resident Read Only user: sp-test58
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. sp-test58)");
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that: Read only member is able to view log entries: Dashboard table on Home page shows list of resident log entries for all comunties.");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("Verify that: Read only member is unable to add new resident log entry for all communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("unable", "New Log Entry", "Resident"), "Able to add new records.");

            Log.Info("Verify that: Read only member is unable to edit an existing resident log entry for all community");
            HomePage.OpenALogEntry(0);
            Assert.IsTrue(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Able to update log entry.");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that: Read only member is unable to delete an existing resident log entry for all communties");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("unable"), "Able to delete log entries");

            Log.Info("Verify that: Read only member is unable to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsTrue(HomePage.IsUserAbleToAddNewRecords("unable", "Bulk Processing", "Resident"), "Able to add new records.");

            Log.Info("Verify that: Read only member is unable to add/edit and delete attachments for a resident log entry");
            HomePage.OpenALogEntry(0);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("unable"), "There is no field to attach a document.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("unable"), "Able to delete a document.");

            Log.Info("Verify that: Read only member is able to View Case Log Report");
            Assert.IsTrue(HomePage.IsUserAbleToViewResidentCaseLogReport("able"), "Read Only member is unable to view Resident Case Log report");
            #endregion Resident Read Only user
            #endregion Main steps
        }
    }
}
