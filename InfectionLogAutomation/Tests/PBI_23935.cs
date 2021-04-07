using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System;
using InfectionLogAutomation.DataObject;
using SeleniumCSharp.Core.Utilities;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]    
    public class PBI_23935: TestBase
    {
        [Test]
        [Description("IO Log - Add Records of Terminated Employees to Drop Down List: New Log Entry")]
        public void PBI_23935_AT_24020()
        {
            #region Test Data            
            string region = "Region A01";
            string community = "Rio Las Palmas";
            string noRecordMsg = "No employee matchs your search criteria";
            string firstName = "Julie";
            string lastName = "Basler";
            string employeeID = "306415";
            LogEntryData logEntryData = JsonParser.Get<LogEntryData>();
            logEntryData.OnsetDate = DateTime.Now.ToString("MM/dd/yyyy");
            logEntryData.TestStatusDate = DateTime.Now.ToString("MM/dd/yyyy");
            logEntryData.DispositionDate = DateTime.Now.ToString("MM/dd/yyyy");
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that Advanced Search button displays on New log entry form");
            Assert.IsTrue(LogEntryDetailPage.btnAdvancedSearch.IsDisplayed(), "Advanced Search button does not display");

            Log.Info("Leave all fields on new log entry form blank and verify that Advanced Search button is disabled for searching");
            Assert.IsFalse(LogEntryDetailPage.IsAdvancedSearchEnabled(), "The button is enabled for searching");            

            Log.Info("4. Select a region only");
            LogEntryDetailPage.FillValue(LogEntryDetailPage.txtRegion, region);

            Log.Info("Verify that Advanced Search button is disabled for searching");
            Assert.IsFalse(LogEntryDetailPage.IsAdvancedSearchEnabled(), "The button is enabled for searching");

            Log.Info("5. Select a community");
            LogEntryDetailPage.FillValue(LogEntryDetailPage.txtCommunity, community);

            Log.Info("Verify that Advanced Search button is enable for searching");
            Assert.IsTrue(LogEntryDetailPage.IsAdvancedSearchEnabled(), "The button is enabled for searching");

            Log.Info("6. Click on Advancecd Search button");
            LogEntryDetailPage.OpenAdvancedSearch();

            Log.Info("Verify that the pop-up opens with correct content");
            Assert.IsTrue(LogEntryDetailPage.CheckAdvancedSearchPopupContent(), "The search pop-up shows with incorrect content");

            Log.Info("7. Leave all search fields blank and click on Search button");
            LogEntryDetailPage.SearchTeamMember();

            Log.Info("Verify that error message displays");
            Assert.IsTrue(LogEntryDetailPage.GetAlertWinText().Equals("Please enter at least one of search fields LastName, FirstName, EmployeeID"), "No error message displays");
            LogEntryDetailPage.CloseAlertPopup();

            Log.Info("8. Fill advanced search info to search");
            LogEntryDetailPage.DoAdvancedSearch(lastName, firstName, "");
        
            Log.Info("Verify that the user can search by first name/last name for a patial match");
            Assert.IsTrue(LogEntryDetailPage.tbladvancedSearchResult.RowCount() > 0, "No result returns");
            Assert.IsTrue(LogEntryDetailPage.DoAllListItemEqualValue(LogEntryDetailPage.tbladvancedSearchResult.GetTableAllCellValueInColumn(1), lastName), "Incorrect result returns");
            Assert.IsTrue(LogEntryDetailPage.DoAllListItemEqualValue(LogEntryDetailPage.tbladvancedSearchResult.GetTableAllCellValueInColumn(2), firstName), "Incorrect result returns");

            Log.Info("9. Uncheck 'Include Terminated Employee' checkbox");
            LogEntryDetailPage.chbncludeTerminatedEmployee.Uncheck();

            Log.Info("10. Search a terminated employee using Fisrt Name, Last Name or Employee ID");
            LogEntryDetailPage.DoAdvancedSearch(lastName, firstName, "");

            Log.Info("Verify that the user is unable to search terminated employee when 'Include Terminated Employee' checkbox is unchecked");
            Assert.IsTrue(LogEntryDetailPage.dvemployeeSearchResult.GetText().Contains(noRecordMsg), "User is able to search terminated employee when the checkbox is unchecked");

            Log.Info("11. Check 'Include Terminated Employee'");
            LogEntryDetailPage.chbncludeTerminatedEmployee.Check();

            Log.Info("12. Search a terminated employee using Employee ID");
            LogEntryDetailPage.DoAdvancedSearch("", "", employeeID);

            Log.Info("Verify that user is able to search by Employe ID for exact match");
            Assert.IsTrue(LogEntryDetailPage.tbladvancedSearchResult.RowCount() > 0, "No result returns");
            Assert.IsTrue(LogEntryDetailPage.DoAllListItemEqualValue(LogEntryDetailPage.tbladvancedSearchResult.GetTableAllCellValueInColumn(3), employeeID), "Incorrect result returns");

            Log.Info("13. Select a record of terminated employee");
            LogEntryDetailPage.SelectFirstTerminatedEmployee();

            Log.Info("14. Fill all required fields");
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team");

            Log.Info("15. Save new log entry");
            LogEntryDetailPage.SaveLogEntry();            

            Log.Info("Verify that the log entry is saved successfully");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", employeeID);
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved successfully");

            Log.Info("Clean up: Delete the log entry created");
            HomePage.DeleteALogEntry(employeeID);
            #endregion
        }
    }
}
