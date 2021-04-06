using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using InfectionLogAutomation.DataObject;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23873: TestBase
    {
        [Test]
        [Description("Home Screen Functionality - Dashboard")]
        public void PBI_23873_AT_23883()
        {
            #region Test Data
            string divisionHeader = "Division";
            string regionHeader = "Region";
            string businessUnitHeader = "BU";
            string communityHeader = "Community";
            string IDHeader = "ID";
            string nameHeader = "Name";
            string residentLOBHeader = "Resident LOB";
            string infectionTypeHeader = "Infection Type";
            string testStatusHeader = "Test Status";
            string dispositionHeader = "Disposition";
            string entryTypeHeader = "Entry Type";
            string pageTitle = "Infection Log Entry for Team Member";
            LogEntryData logEntryData;
            //List<string> entryInfo;            
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page does not display");
            Assert.IsTrue(HomePage.tblDashboardHeader.IsDisplayed(), "Home page does not contain dashboard/table of infections");

            #region Pre-condition: Create a log entry
            // Go to New Log Entry
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            // Fill Log Entry info randomly
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            // Save new team log entry
            LogEntryDetailPage.SaveLogEntry();            
            HomePage.ShowBothActiveAndInactiveRecords();
            #endregion

            Log.Info("Verify that Division column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, divisionHeader), "Division is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(divisionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, divisionHeader), "Division is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(divisionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, divisionHeader), "Division is not sorted by descending order");

            Log.Info("Verify that Region column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, regionHeader), "Region is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(regionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, regionHeader), "Region is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(regionHeader);            

            Log.Info("Verify that BU column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, businessUnitHeader), "BU is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(businessUnitHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, businessUnitHeader), "BU is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(businessUnitHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, businessUnitHeader), "BU is not sorted by descending order");

            Log.Info("Verify that Community column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, communityHeader), "Community is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(communityHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, communityHeader), "Community is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(communityHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, communityHeader), "Community is not sorted by descending order");

            Log.Info("Verify that ID column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, IDHeader), "ID is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(IDHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, IDHeader), "ID is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(IDHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, IDHeader), "ID is not sorted by descending order");

            Log.Info("Verify that Name column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, nameHeader), "Name is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(nameHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, nameHeader), "Name is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(nameHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, nameHeader), "Name is not sorted by descending order");

            Log.Info("Verify that Resident LOB column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, residentLOBHeader), "Resident LOB is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(residentLOBHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, residentLOBHeader), "Resident LOB is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(residentLOBHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, residentLOBHeader), "Resident LOB is not sorted by descending order");

            Log.Info("Verify that Infection Type column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, infectionTypeHeader), "Infection Type is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(infectionTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, infectionTypeHeader), "Infection Type is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(infectionTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, infectionTypeHeader), "Infection Type is not sorted by descending order");

            Log.Info("Verify that Test Status column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, testStatusHeader), "Test Status is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(testStatusHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, testStatusHeader), "Test Status is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(testStatusHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, testStatusHeader), "Test Status is not sorted by descending order");

            Log.Info("Verify that Disposition column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, dispositionHeader), "Disposition is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(dispositionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, dispositionHeader), "Disposition is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(dispositionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, dispositionHeader), "Disposition is not sorted by descending order");

            Log.Info("Verify that Entry Type column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardHeader, entryTypeHeader), "Entry Type is not sortable");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(entryTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardHeader, entryTypeHeader), "Entry Type is not sorted by ascending order");
            HomePage.tblDashboardHeader.ClickOnHeaderColumn(entryTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardHeader, entryTypeHeader), "Entry Type is not sorted by descending order");

            Log.Info("Filter by Region column");
            HomePage.ClearAllFilters();
            HomePage.FilterATableColumn(regionHeader, logEntryData.Region);            
            Log.Info("Verify that Region column is filtered correctly");            
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(regionHeader, logEntryData.Region), "Filtered data for Region is incorrect");

            Log.Info("Filter by Community column");
            HomePage.FilterATableColumn(communityHeader, logEntryData.Community);            
            Log.Info("Verify that Community column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(communityHeader, logEntryData.Community), "Filtered data for Community is incorrect");

            Log.Info("Filter by Test Status column");
            HomePage.FilterATableColumn(testStatusHeader, logEntryData.CurrentTestStatus);            
            Log.Info("Verify that Test Status column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(testStatusHeader, logEntryData.CurrentTestStatus), "Filtered data for Test Status is incorrect");

            Log.Info("Filter by Disposition column");
            HomePage.FilterATableColumn(dispositionHeader, logEntryData.CurrentDisposition);            
            Log.Info("Verify that Disposition column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(dispositionHeader, logEntryData.CurrentDisposition), "Filtered data for Disposition is incorrect");

            Log.Info("Filter by Entry Type");
            HomePage.FilterATableColumn("Entry Type", "Team Member");
            Log.Info("Verify that Entry Type column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Filtered data for Entry Type is incorrect");

            Log.Info("Filter by Name column");
            HomePage.FilterATableColumn(nameHeader, logEntryData.Name);            
            Log.Info("Verify that Name column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(nameHeader, logEntryData.Name), "Filtered data for Name is incorrect");

            Log.Info("Filter by ID column");
            HomePage.FilterATableColumn(IDHeader, logEntryData.MRN);            
            Log.Info("Verify that ID column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(IDHeader, logEntryData.MRN), "Filtered data for ID is incorrect");            

            Log.Info("Click on ID");
            HomePage.OpenALogEntry(0);

            Log.Info("Verify that existing Log entries are opened");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "Existing Log entry is not opened");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "Existing log entry's data is correct");

            Log.Info("Delete log entry created");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            HomePage.DeleteALogEntry(logEntryData.MRN);
            #endregion
        }
    }
}
