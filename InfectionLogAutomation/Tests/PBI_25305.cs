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
            string region = "Region A01";
            string community = "Rio Las Palmas";
            string employeeName = "Beer, Herman";
            string testingDate = DateTime.Now.ToString("MM/dd/yyyy");
            string comments = "LGG Testing";
            List<string> selectedEmployee = new List<string>();
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format are incorrect.");

            Log.Info("5. Fill Region and Community");
            BulkInsertPage.FillLogEntryInfo(region, community, null, null, null);

            Log.Info("6. Click on button 'Select Team Member' next to text box 'Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("Verify that a pop-up (Select Team Member) appears.");
            Assert.IsTrue(BulkInsertPage.CheckSelectEmployeePopupExit(), "Select Employee Popup does not exist.");

            Log.Info("Verify that the pop-up UI displaying correctly.");
            Assert.IsTrue(BulkInsertPage.DoesSelectPopupUIDisplayCorrectly(), "Popup UI displays incorrectly.");

            Log.Info("8. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee(employeeName);

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table.");
            Assert.IsTrue(BulkInsertPage.DoesSearchRecordsPartialMatchSearchCriteria("Beer", "Herman"), "Records do not partialy match the searcj criteria.");

            Log.Info("9. Select some members/records and click on Cancel button on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(0);
            BulkInsertPage.CancelSearchEmployee();

            Log.Info("Verify that: The pop-up is closed and no members/records show on main screen.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            Assert.IsTrue(selectedEmployee.Count.Equals(0), "The selected employee is displayed on main screen.");

            Log.Info("10. Re-open the pop-up 'Select Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("11. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee(employeeName);
            string id = BulkInsertPage.tblSearchResultTable.GetTableCellValue(3, 0);

            Log.Info("12. Select some members/records and click on Select button on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(0);
            BulkInsertPage.SelectCheckedEmployee();

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            Assert.IsTrue(selectedEmployee.Count.Equals(1) && selectedEmployee[0].Contains(employeeName), "The selected employee is not displayed on main screen.");

            Log.Info("13. Fill Testing Date and Comments fields");
            BulkInsertPage.FillLogEntryInfo(null, null, null, testingDate, comments);

            Log.Info("14. Click on button 'Insert' at the bottom");
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(5, 0).Equals(id) && HomePage.tblDashboard.GetTableCellValue(6, 0).Equals(employeeName), "created log entries for bulk insert do not shows on Dashboard.");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(id);
            #endregion Clean up
        }

        [Test]
        [Description("IO Log: Bulk Insert Record UI Enhancements: Resident")]
        public void PBI_25305_AT_25311()
        {
            #region Test data
            string region = "Region A01";
            string community = "Rio Las Palmas";
            string employeeName = "Day, Arthur";
            string testingDate = DateTime.Now.ToString("MM/dd/yyyy");
            string comments = "LGG Testing";
            List<string> selectedEmployee = new List<string>();
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Residents"));

            Log.Info("Verify that the file provide us with following information: Community, Date of Testing, Team Member/Resident");
            Assert.IsTrue(BulkInsertPage.DoesUIDisplayCorrectly(), "Page UI displays incorrectly.");
            Assert.IsTrue(BulkInsertPage.DoesUIWithFormatDisplayCorrectly(), "Fields format are incorrect.");

            Log.Info("5. Fill Region and Community");
            BulkInsertPage.FillLogEntryInfo(region, community, null, null, null);

            Log.Info("6. Click on button 'Select Team Member' next to text box 'Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("Verify that a pop-up (Select Team Member) appears.");
            Assert.IsTrue(BulkInsertPage.CheckSelectEmployeePopupExit(), "Select Employee Popup does not exist.");

            Log.Info("Verify that the pop-up UI displaying correctly.");
            Assert.IsTrue(BulkInsertPage.DoesSelectPopupUIDisplayCorrectly(), "Popup UI displays incorrectly.");

            Log.Info("8. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee(employeeName);

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table.");
            Assert.IsTrue(BulkInsertPage.DoesSearchRecordsPartialMatchSearchCriteria("Day", "Arthur"), "Records do not partialy match the searcj criteria.");

            Log.Info("9. Select some members/records and click on Cancel button on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(0);
            BulkInsertPage.CancelSearchEmployee();

            Log.Info("Verify that: The pop-up is closed and no members/records show on main screen.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            Assert.IsTrue(selectedEmployee.Count.Equals(0), "The selected employee is displayed on main screen.");

            Log.Info("10. Re-open the pop-up 'Select Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("11. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee(employeeName);
            DriverUtils.wait(1);
            string mrn = BulkInsertPage.tblSearchResultTable.GetTableCellValue(3, 0);

            Log.Info("12. Select some members/records and click on Select button on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(0);
            BulkInsertPage.SelectCheckedEmployee();

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            Assert.IsTrue(selectedEmployee.Count.Equals(1) && selectedEmployee[0].ToLower().Contains(employeeName.ToLower()), "The selected employee is not displayed on main screen.");

            Log.Info("13. Fill Testing Date and Comments fields");
            BulkInsertPage.FillLogEntryInfo(null, null, null, testingDate, comments);

            Log.Info("14. Click on button 'Insert' at the bottom");
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(5, 0).Equals(mrn) && HomePage.tblDashboard.GetTableCellValue(6, 0).ToLower().Equals(employeeName.ToLower()), "created log entries for bulk insert do not shows on Dashboard.");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(mrn);
            #endregion Clean up
        }
    }
}
