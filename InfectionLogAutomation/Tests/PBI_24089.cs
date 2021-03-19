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
            int expectedNumberOfEmployee = 20;
            int numberOfCreatedRecords;
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            List<string> actualEmployeesList, employeeList, actualResidentList, residentList, LOB, ID;
            List<List<string>> outListBulkInsert;
            string BU, outNumberOfRecords, employeeId, employeeName, residentId, residentName;
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
            BulkInsertPage.FillBulkInsertRandomly(out outListBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");
            actualEmployeesList = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", numberOfCreatedRecords);
            actualEmployeesList.Sort();

            Log.Info("Get list of communities from Workday");
            //employeeList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee Name", "[Currently Active] = 'Yes' AND [Location - Name] = '" + bulkProcessingData.Community + "'").Distinct().ToList();
            //employeeList.Sort();

            Log.Info("Verify that the records for bulk processing display correctly.");
            ID = HomePage.GetAllValueInColumnOfBulkInsertRecords("ID", numberOfCreatedRecords);
            //Assert.IsTrue(actualEmployeesList.SequenceEqual(employeeList), "Inactive Employees display on Dashboaed table.");
            //Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(ID, bulkProcessingData.TestingDate), "Data of Bulk Insert records displays incorrectly.");

            Log.Info("Try to edit uploaded records");

            Log.Info("Verify that uploaded records are able to edit");

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region DVP/DDHR: sp-test51
            Log.Info("6.1. Logout and login with Team Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualEmployeesList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualEmployeesList.Sort();
            //Assert.IsTrue(actualEmployeesList.SequenceEqual(employeeList), "Inactive Employees display on Dashboaed table.");
            #endregion DVP/DDHR: sp-test51

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDO/RDHR/Senior RDO: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualEmployeesList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualEmployeesList.Sort();
            //Assert.IsTrue(actualEmployeesList.SequenceEqual(employeeList), "Inactive Employees display on Dashboaed table.");
            #endregion RDO/RDHR/Senior RDO: sp-test54

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region ED/HR Partners: sp-test57
            Log.Info("6.7. Logout and login with Team Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualEmployeesList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualEmployeesList.Sort();
            //Assert.IsTrue(actualEmployeesList.SequenceEqual(employeeList), "Inactive Employees display on Dashboaed table.");
            #endregion ED/HR Partners: sp-test57
            #endregion Bulk Processing - Insert Team

            #region Bulk Processing - Insert Resident
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");

            Log.Info("Perform a bulk inserting for a community");
            BulkInsertPage.FillBulkInsertRandomly(out outListBulkInsert, out numberOfCreatedRecords, expectedNumberOfEmployee);
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Go to Home page and notice all uploaded records");

            Log.Info("");

            Log.Info("Try to edit uploaded records");

            Log.Info("Verify that uploaded records are able to edit");

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region DDH: sp-test52
            Log.Info("6.2. Logout and login with Resident Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualResidentList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualResidentList.Sort();
            //Assert.IsTrue(actualResidentList.SequenceEqual(residentList), "Inactive Employees display on Dashboaed table.");
            #endregion DDH: sp-test52

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDH: sp-test55
            Log.Info("6.5. Logout and login with Resident Community Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualResidentList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualResidentList.Sort();
            //Assert.IsTrue(actualResidentList.SequenceEqual(residentList), "Inactive Employees display on Dashboaed table.");
            #endregion RDH: sp-test55

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region RDO/RDHR/Senior RDO: sp-test54
            Log.Info("6.4. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            //Logger.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualResidentList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualResidentList.Sort();
            //Assert.IsTrue(actualResidentList.SequenceEqual(residentList), "Inactive Employees display on Dashboaed table.");
            #endregion RDO/RDHR/Senior RDO: sp-test54

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Read Only: sp-test58
            Log.Info("6.8. Logout and login with ​Resident Read Only account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that uploaded records appear on home screen as per business rules (follow IOT Security Matrix .xlsx)");
            //actualResidentList = DashboardPage.GetAllValueInColumnOfBulkInsertRecords("Name", outNumberOfRecords);
            //actualResidentList.Sort();
            //Assert.IsTrue(actualResidentList.SequenceEqual(residentList), "Inactive Employees display on Dashboaed table.");
            #endregion Read Only: sp-test58
            #endregion Bulk Processing - Insert Resident
            #endregion Main steps
        }
    }
}
