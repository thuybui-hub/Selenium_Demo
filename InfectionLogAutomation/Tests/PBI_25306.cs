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
    public class PBI_25306 : TestBase
    {
        [Test]
        [Description("IO Log: Bulk Insert Record Selection & Processing: Team")]
        public void PBI_25306_AT_25362()
        {
            #region Test data
            string region = "Region A01";
            string community = "Rio Las Palmas";
            //string employeeName = "Beer, Herman";
            string testingDate = DateTime.Now.ToString("MM/dd/yyyy");
            string comments = "LGG Testing";
            List<string> selectedEmployee, idList, createdEmployee;
            string filterValue = "Beer";

            string lastNameHeader = "Last Name";
            string firstNameHeader = "First Name";
            string personIdHeader = "Person ID";
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Team Members"));

            Log.Info("4. Fill Region and Community");
            BulkInsertPage.FillLogEntryInfo(region, community, null, null, null);

            Log.Info("5.Click on button 'Select Team Member' next to text box 'Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("Verify that a pop-up (Select Team Member) appears.");
            Assert.IsTrue(BulkInsertPage.CheckSelectEmployeePopupExit(), "Select Employee Popup does not exist.");

            Log.Info("6. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee("a", null);

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table");
            Assert.IsTrue(BulkInsertPage.DoesSearchRecordsPartialMatchSearchCriteria("a", null), "Records do not partialy match the searcj criteria.");

            Log.Info("7. Sort data by a column on the pop-up and Verify that Last Name column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(lastNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(lastNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sorted by descending order");

            Log.Info("7. Sort data by a column on the pop-up and Verify that First Name column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(firstNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(firstNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sorted by descending order");

            Log.Info("7. Sort data by a column on the pop-up and Verify that Person ID column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sorted by descending order");

            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            idList = BulkInsertPage.tblSearchResultTable.GetTableAllCellValueInColumn(3);

            Log.Info("8. Check one or more record on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(idList);

            Log.Info("Verify that corresponding one or more records are checked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(idList, "Checked"), "These employee are not checked.");

            Log.Info("9. Uncheck the records");
            BulkInsertPage.CheckOnEmployeeCheckBox(idList);

            Log.Info("Verify that corresponding records are unchecked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(idList, "Unchecked"), "These employee are not checked.");

            Log.Info("10. Try to check/select all records");
            BulkInsertPage.ClearSearchCriteria();
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("Verify that all records are checked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(null, "CheckedAll"), "These employee are not checked.");
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("11. Filter records");
            BulkInsertPage.FilterATableColumn(lastNameHeader, filterValue);

            Log.Info("Verify that the records are filtered correctly");
            Assert.IsTrue(BulkInsertPage.DoesFilterDataDisplayCorrectly(lastNameHeader, filterValue), "Records are filtered incrrectly.");

            Log.Info("12. Select some records on the pop-up");
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("13. Click on 'Select' button on the pop-up");
            BulkInsertPage.SelectCheckedEmployee();

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            selectedEmployee.Sort();
            Assert.IsTrue(selectedEmployee.All(x => x.Contains(filterValue)), "The selected employee is not displayed on main screen.");

            Log.Info("14. Fill Testing Date and Comments fields");
            BulkInsertPage.FillLogEntryInfo(null, null, null, testingDate, comments);

            Log.Info("15. Click on button 'Insert' at the bottom");
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            createdEmployee = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", selectedEmployee.Count);
            createdEmployee.Sort();
            Assert.IsTrue(selectedEmployee.All(x => createdEmployee.All(y => x.Contains(y))), "Records for Bulk Insert shows incorrectly.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }

        [Test]
        [Description("IO Log: Bulk Insert Record Selection & Processing: Resident")]
        public void PBI_25306_AT_25363()
        {
            #region Test data
            string region = "Region A01";
            string community = "Rio Las Palmas";
            //string employeeName = "Beer, Herman";
            string testingDate = DateTime.Now.ToString("MM/dd/yyyy");
            string comments = "LGG Testing";
            List<string> selectedEmployee, idList, createdEmployee;
            string filterValue = "Day";

            string lastNameHeader = "Last Name";
            string firstNameHeader = "First Name";
            string personIdHeader = "Person ID";
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to Bulk Insert -> Residents");
            HomePage.SelectMenuItem(Constants.NewResidentBulkInsertPath);

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Residents' opens.");
            Assert.IsTrue(BulkInsertPage.CheckPageExist("Bulk Infection Log Entry for Residents"));

            Log.Info("4. Fill Region and Community");
            BulkInsertPage.FillLogEntryInfo(region, community, null, null, null);

            Log.Info("5.Click on button 'Select Resident' next to text box 'Team Member'");
            BulkInsertPage.OpenSearchEmployeePopup();

            Log.Info("Verify that a pop-up (Select Residents) appears.");
            Assert.IsTrue(BulkInsertPage.CheckSelectEmployeePopupExit(), "Select Employee Popup does not exist.");

            Log.Info("6. Perfom a search on the pop-up");
            BulkInsertPage.PerformASearchEmployee("a", null);

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table");
            Assert.IsTrue(BulkInsertPage.DoesSearchRecordsPartialMatchSearchCriteria("a", null), "Records do not partialy match the searcj criteria.");

            Log.Info("7. Sort data by a column on the pop-up and Verify that Last Name column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(lastNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(lastNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, lastNameHeader), "Division is not sorted by descending order");

            Log.Info("7. Sort data by a column on the pop-up and Verify that First Name column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(firstNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(firstNameHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, firstNameHeader), "Division is not sorted by descending order");

            Log.Info("7. Sort data by a column on the pop-up and Verify that Person ID column sort correctly");
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortable(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sortable");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByAscendingOrder(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sorted by ascending order");
            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            Assert.IsTrue(BulkInsertPage.IsColumnHeaderSortedByDescendingOrder(BulkInsertPage.tblSearchResultTableHeader, personIdHeader), "Division is not sorted by descending order");

            BulkInsertPage.tblSearchResultTableHeader.ClickOnHeaderColumn(personIdHeader);
            idList = BulkInsertPage.tblSearchResultTable.GetTableAllCellValueInColumn(3);

            Log.Info("8. Check one or more record on the pop-up");
            BulkInsertPage.CheckOnEmployeeCheckBox(idList);

            Log.Info("Verify that corresponding one or more records are checked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(idList, "Checked"), "These employee are not checked.");

            Log.Info("9. Uncheck the records");
            BulkInsertPage.CheckOnEmployeeCheckBox(idList);

            Log.Info("Verify that corresponding records are unchecked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(idList, "Unchecked"), "These employee are not checked.");

            Log.Info("10. Try to check/select all records");
            BulkInsertPage.ClearSearchCriteria();
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("Verify that all records are checked");
            Assert.IsTrue(BulkInsertPage.AreCorrespondingEmployeesCheckedOrUnchecked(null, "CheckedAll"), "These employee are not checked.");
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("11. Filter records");
            BulkInsertPage.FilterATableColumn(lastNameHeader, filterValue);

            Log.Info("Verify that the records are filtered correctly");
            Assert.IsTrue(BulkInsertPage.DoesFilterDataDisplayCorrectly(lastNameHeader, filterValue), "Records are filtered incrrectly.");

            Log.Info("12. Select some records on the pop-up");
            BulkInsertPage.CheckAllSearchRecords();

            Log.Info("13. Click on 'Select' button on the pop-up");
            BulkInsertPage.SelectCheckedEmployee();

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");
            Assert.IsFalse(BulkInsertPage.divSelectEmployeePopup.IsDisplayed(), "Select Employee popup is not closed.");
            BulkInsertPage.txtEmployee.Click();
            selectedEmployee = BulkInsertPage.lstEmployee.GetSelectedOptions();
            selectedEmployee.Sort();
            Assert.IsTrue(selectedEmployee.All(x => x.ToLower().Contains(filterValue.ToLower())), "The selected employee is not displayed on main screen.");

            Log.Info("14. Fill Testing Date and Comments fields");
            BulkInsertPage.FillLogEntryInfo(null, null, null, testingDate, comments);

            Log.Info("15. Click on button 'Insert' at the bottom");
            BulkInsertPage.SaveBulkInsert();

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            createdEmployee = HomePage.GetAllValueInColumnOfBulkInsertRecords("Name", selectedEmployee.Count);
            createdEmployee.Sort();
            Assert.IsTrue(selectedEmployee.SequenceEqual(createdEmployee), "Records for Bulk Insert shows incorrectly.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
