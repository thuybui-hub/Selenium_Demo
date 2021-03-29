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
    public class PBI_24089 : TestBase
    {
        [Test]
        [Description("IO Log - Bulk Processing Functionality")]
        public void PBI_24089_AT_24107()
        {
            #region Test data
            int expectedNumberOfEmployee = 3;
            int numberOfCreatedRecords;
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            string excelPathResident = Constants.DataPath + "ResidentsInfo.xls";
            List<string> actualTeamMemberList, teamMemberList, actualResidentList, residentList, LOB;
            List<List<string>> outListTeamBulkInsert, outListResidentBulkInsert;
            LogEntryData logEntryData = new LogEntryData();
            #endregion Test data

            #region Main steps
            #region Bulk Processing - Insert Team
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");

            Log.Info("Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListTeamBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            List<string> listId = HomePage.GetAllValueInColumnOfBulkInsertRecords("ID", numberOfCreatedRecords);
            actualTeamMemberList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualTeamMemberList.Sort();

            Log.Info("Get list of communities from Workday");
            teamMemberList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + outListTeamBulkInsert[0][1] + "'").Distinct().ToList();
            teamMemberList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualTeamMemberList.All(x => teamMemberList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            List<List<string>> actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListTeamBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");

            Log.Info("Try to edit uploaded records");
            logEntryData.Symptoms = "LGG Testing";
            logEntryData.CurrentTestStatus = "Tested - Negative";
            logEntryData.CurrentDisposition = "Hospitalized";
            logEntryData.Comments = "LGG Testing";
            HomePage.OpenALogEntry(outListTeamBulkInsert[0][2]);
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that uploaded records are able to edit");
            logEntryData.Region = outListTeamBulkInsert[0][0];
            logEntryData.Community = outListTeamBulkInsert[0][1];
            logEntryData.MRN = outListTeamBulkInsert[0][2];
            logEntryData.Name = outListTeamBulkInsert[0][3];
            logEntryData.InfectionType = "COVID-19";
            logEntryData.OnsetDate = outListTeamBulkInsert[0][4];
            logEntryData.TestStatusDate = outListTeamBulkInsert[0][4];
            logEntryData.DispositionDate = outListTeamBulkInsert[0][4];
            HomePage.OpenALogEntry(outListTeamBulkInsert[0][2]);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team Bulk"), "Upload record are unable to edit.");

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region DVP/DDHR: sp-test51
            Log.Info("6.1. Logout and login with Team Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion DVP/DDHR: sp-test51

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDO/RDHR/Senior RDO: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion RDO/RDHR/Senior RDO: sp-test54

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region ED/HR Partners: sp-test57
            Log.Info("6.7. Logout and login with Team Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion ED/HR Partners: sp-test57
            #endregion Bulk Processing - Insert Team

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Bulk Processing - Insert Resident
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");

            Log.Info("Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListResidentBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualResidentList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualResidentList.Sort();

            Log.Info("Get list of communities from Workday");
            residentList = ExcelActions.GetCellValuesInColumn(excelPathResident, "Residents", "ResidentName", "[LOB] <> 'IL' AND [DischargeDate] = 'NULL' AND [CommunityName] = '" + outListResidentBulkInsert[0][1] + "'").Distinct().ToList();
            residentList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            LOB = HomePage.GetAllValueInColumnOfBulkInsertRecords("Resident LOB", numberOfCreatedRecords);
            Assert.IsTrue(actualResidentList.All(x => residentList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            Assert.IsTrue(HomePage.DoesResidentBulkInsertRecordsNotEqualIL(LOB), "Records for employee with LOB = IL is created.");
            List<List<string>> actualResidentBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListResidentBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualResidentBulkInsertData, outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");

            Log.Info("Try to edit uploaded records");
            logEntryData.Symptoms = "LGG Testing";
            logEntryData.CurrentTestStatus = "Tested - Negative";
            logEntryData.CurrentDisposition = "Hospitalized";
            logEntryData.Comments = "LGG Testing";
            HomePage.OpenALogEntry(outListResidentBulkInsert[0][2]);
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Resident", "Edit");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that uploaded records are able to edit");
            logEntryData.Region = outListResidentBulkInsert[0][0];
            logEntryData.Community = outListResidentBulkInsert[0][1];
            logEntryData.MRN = outListResidentBulkInsert[0][2];
            logEntryData.Name = outListResidentBulkInsert[0][3];
            logEntryData.InfectionType = "COVID-19";
            logEntryData.OnsetDate = outListResidentBulkInsert[0][4];
            logEntryData.TestStatusDate = outListResidentBulkInsert[0][4];
            logEntryData.DispositionDate = outListResidentBulkInsert[0][4];
            HomePage.OpenALogEntry(outListResidentBulkInsert[0][2]);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "Upload record are unable to edit.");

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region DDH: sp-test52
            Log.Info("6.2. Logout and login with Resident Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion DDH: sp-test52

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDH: sp-test55
            Log.Info("6.5. Logout and login with Resident Community Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion RDH: sp-test55

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDO/RDHR/Senior RDO: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion RDO/RDHR/Senior RDO: sp-test54

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Read Only: sp-test58
            Log.Info("6.8. Logout and login with ​Resident Read Only account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            Assert.IsTrue(HomePage.AreBulkInsertRecordsDisplayedAsPerBusinessRules(outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Read Only: sp-test58
            #endregion Bulk Processing - Insert Resident
            #endregion Main steps
        }
    }
}
