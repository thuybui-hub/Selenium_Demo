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
    public class PBI_24100 : TestBase
    {
        [Test]
        [Description("IO Log - Bulk Record Processing Security: Team Member")]
        public void PBI_24100_AT_24171()
        {
            #region Test data
            int expectedNumberOfEmployee = 3;
            int numberOfCreatedRecords;
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            List<string> actualTeamMemberList, teamMemberList, actualCommunitiesList, expectedCommunitiesList;
            List<List<string>> outListTeamBulkInsert, actualBulkInsertData;
            expectedCommunitiesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "INT049_RPT_MyFVE Communities", "Name", "[INT049_CCF_Division] <> 'Divested Division' AND [INT049_CCF_Division] <> '(Blanks)' AND [INT049_CCF_Division] <> 'Region' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            expectedCommunitiesList.Sort();
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            #region Admin user: gnguyen
            Log.Info("1. Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListTeamBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualTeamMemberList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualTeamMemberList.Sort();

            Log.Info("Get list of communities from Workday");
            teamMemberList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + outListTeamBulkInsert[0][1] + "'").Distinct().ToList();
            teamMemberList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualTeamMemberList.All(x => teamMemberList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListTeamBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Admin user: sp-test51
            Log.Info("5.1. Logout and login with Team Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListTeamBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualTeamMemberList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualTeamMemberList.Sort();

            Log.Info("Get list of communities from Workday");
            teamMemberList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + outListTeamBulkInsert[0][1] + "'").Distinct().ToList();
            teamMemberList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualTeamMemberList.All(x => teamMemberList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListTeamBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Team Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Admin user: sp-test54
            Log.Info("5.2. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListTeamBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualTeamMemberList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualTeamMemberList.Sort();

            Log.Info("Get list of communities from Workday");
            teamMemberList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + outListTeamBulkInsert[0][1] + "'").Distinct().ToList();
            teamMemberList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualTeamMemberList.All(x => teamMemberList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListTeamBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Team Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Submittor user: sp-test57
            Log.Info("5.3. Logout and login with Team Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListTeamBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualTeamMemberList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualTeamMemberList.Sort();

            Log.Info("Get list of communities from Workday");
            teamMemberList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + outListTeamBulkInsert[0][1] + "'").Distinct().ToList();
            teamMemberList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualTeamMemberList.All(x => teamMemberList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListTeamBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListTeamBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Team Community Submittor user
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }

        [Test]
        [Description("IO Log - Bulk Record Processing Security: Resident")]
        public void PBI_24100_AT_24172()
        {
            #region Test data
            int expectedNumberOfEmployee = 3;
            int numberOfCreatedRecords;
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            string excelPathResident = Constants.DataPath + "ResidentsInfo.xls";
            List<string> actualResidentList, residentList, actualCommunitiesList, expectedCommunitiesList;
            List<List<string>> outListResidentBulkInsert, actualBulkInsertData;
            expectedCommunitiesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "INT049_RPT_MyFVE Communities", "Name", "([INT049_CCF_Division] = 'Division A' OR [INT049_CCF_Division] = 'Division B' OR [INT049_CCF_Division] = 'Division C') AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            expectedCommunitiesList.Sort();
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            #region Admin user: gnguyen
            Log.Info("1. Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListResidentBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualResidentList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualResidentList.Sort();

            Log.Info("Get list of communities from Workday");
            residentList = ExcelActions.GetCellValuesInColumn(excelPathResident, "Residents", "ResidentName", "[LOB] <> 'IL' AND [DischargeDate] = 'NULL' AND [CommunityName] = '" + outListResidentBulkInsert[0][1] + "'").Distinct().ToList();
            residentList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualResidentList.All(x => residentList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListResidentBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region DDH: sp-test52
            Log.Info("6.2. Logout and login with Resident Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListResidentBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualResidentList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualResidentList.Sort();

            Log.Info("Get list of communities from Workday");
            residentList = ExcelActions.GetCellValuesInColumn(excelPathResident, "Residents", "ResidentName", "[LOB] <> 'IL' AND [DischargeDate] = 'NULL' AND [CommunityName] = '" + outListResidentBulkInsert[0][1] + "'").Distinct().ToList();
            residentList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualResidentList.All(x => residentList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListResidentBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion DDH: sp-test52

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDH: sp-test55
            Log.Info("6.5. Logout and login with Resident Community Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListResidentBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualResidentList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualResidentList.Sort();

            Log.Info("Get list of communities from Workday");
            residentList = ExcelActions.GetCellValuesInColumn(excelPathResident, "Residents", "ResidentName", "[LOB] <> 'IL' AND [DischargeDate] = 'NULL' AND [CommunityName] = '" + outListResidentBulkInsert[0][1] + "'").Distinct().ToList();
            residentList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualResidentList.All(x => residentList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            actualBulkInsertData = HomePage.GetAllCreatedBulkInsertRecordsData(outListResidentBulkInsert);
            Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(actualBulkInsertData, outListResidentBulkInsert), "Data of Bulk Insert records displays incorrectly.");
            #endregion RDH: sp-test55

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDO/RDHR/Senior RDO: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to Bulk Insert -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that 'Community Drop' show all communities");
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();
            Assert.IsTrue(actualCommunitiesList.All(x => expectedCommunitiesList.Contains(x)), "'Community Drop' does not show all communities");

            Log.Info("4. Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListResidentBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualResidentList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualResidentList.Sort();

            Log.Info("Get list of communities from Workday");
            residentList = ExcelActions.GetCellValuesInColumn(excelPathResident, "Residents", "ResidentName", "[LOB] <> 'IL' AND [DischargeDate] = 'NULL' AND [CommunityName] = '" + outListResidentBulkInsert[0][1] + "'").Distinct().ToList();
            residentList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            Assert.IsTrue(actualResidentList.All(x => residentList.Contains(x)), "Inactive Employees display on Dashboaed table.");
            #endregion RDO/RDHR/Senior RDO: sp-test54

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Read Only: sp-test58
            Log.Info("6.8. Logout and login with ​Resident Read Only account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that Read only member is unable to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsFalse(HomePage.IsUserAbleToAddNewRecords("unable", "Bulk Processing", "Resident"), "Read Only user is able to perform a bulk insert.");
            #endregion Read Only: sp-test58
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
