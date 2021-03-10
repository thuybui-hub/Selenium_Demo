using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

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

            List<string> entryInfo;            
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page does not display");
            Assert.IsTrue(HomePage.tblDashboardTableHeader.IsDisplayed(), "Home page does not contain dashboard/table of infections");            

            #region Pre-condition: Create a log entry
            // Go to New Log Entry
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            // Fill Log Entry info randomly
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out entryInfo);
            // Save new team log entry
            LogEntryDetailPage.SaveLogEntry();            
            HomePage.ShowBothActiveAndInactiveRecords();
            #endregion

            Log.Info("Verify that Division column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, divisionHeader), "Division is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(divisionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, divisionHeader), "Division is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(divisionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, divisionHeader), "Division is not sorted by descending order");

            Log.Info("Verify that Region column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, regionHeader), "Region is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(regionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, regionHeader), "Region is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(regionHeader);            

            Log.Info("Verify that BU column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, businessUnitHeader), "BU is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(businessUnitHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, businessUnitHeader), "BU is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(businessUnitHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, businessUnitHeader), "BU is not sorted by descending order");

            Log.Info("Verify that Community column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, communityHeader), "Community is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(communityHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, communityHeader), "Community is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(communityHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, communityHeader), "Community is not sorted by descending order");

            Log.Info("Verify that ID column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, IDHeader), "ID is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(IDHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, IDHeader), "ID is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(IDHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, IDHeader), "ID is not sorted by descending order");

            Log.Info("Verify that Name column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, nameHeader), "Name is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(nameHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, nameHeader), "Name is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(nameHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, nameHeader), "Name is not sorted by descending order");

            Log.Info("Verify that Resident LOB column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, residentLOBHeader), "Resident LOB is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(residentLOBHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, residentLOBHeader), "Resident LOB is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(residentLOBHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, residentLOBHeader), "Resident LOB is not sorted by descending order");

            Log.Info("Verify that Infection Type column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, infectionTypeHeader), "Infection Type is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(infectionTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, infectionTypeHeader), "Infection Type is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(infectionTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, infectionTypeHeader), "Infection Type is not sorted by descending order");

            Log.Info("Verify that Test Status column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, testStatusHeader), "Test Status is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(testStatusHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, testStatusHeader), "Test Status is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(testStatusHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, testStatusHeader), "Test Status is not sorted by descending order");

            Log.Info("Verify that Disposition column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, dispositionHeader), "Disposition is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(dispositionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, dispositionHeader), "Disposition is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(dispositionHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, dispositionHeader), "Disposition is not sorted by descending order");

            Log.Info("Verify that Entry Type column sort correctly");
            Assert.IsTrue(HomePage.IsColumnHeaderSortable(HomePage.tblDashboardTableHeader, entryTypeHeader), "Entry Type is not sortable");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(entryTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByAscendingOrder(HomePage.tblDashboardTableHeader, entryTypeHeader), "Entry Type is not sorted by ascending order");
            HomePage.tblDashboardTableHeader.ClickOnHeaderColumn(entryTypeHeader);
            Assert.IsTrue(HomePage.IsColumnHeaderSortedByDescendingOrder(HomePage.tblDashboardTableHeader, entryTypeHeader), "Entry Type is not sorted by descending order");

            Log.Info("Filter by Region column");
            HomePage.ClearAllFilters();
            HomePage.FilterATableColumn(regionHeader, entryInfo[0]);            
            Log.Info("Verify that Region column is filtered correctly");            
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(regionHeader, entryInfo[0]), "Filtered data for Region is incorrect");

            Log.Info("Filter by Community column");
            HomePage.FilterATableColumn(communityHeader, entryInfo[1]);            
            Log.Info("Verify that Community column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(communityHeader, entryInfo[1]), "Filtered data for Community is incorrect");

            Log.Info("Filter by Test Status column");
            HomePage.FilterATableColumn(testStatusHeader, entryInfo[6]);            
            Log.Info("Verify that Test Status column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(testStatusHeader, entryInfo[6]), "Filtered data for Test Status is incorrect");

            Log.Info("Filter by Disposition column");
            HomePage.FilterATableColumn(dispositionHeader, entryInfo[8]);            
            Log.Info("Verify that Disposition column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(dispositionHeader, entryInfo[8]), "Filtered data for Disposition is incorrect");

            Log.Info("Filter by Entry Type");
            HomePage.FilterATableColumn("Entry Type", "Team Member");
            Log.Info("Verify that Entry Type column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Filtered data for Entry Type is incorrect");

            Log.Info("Filter by Name column");
            HomePage.FilterATableColumn(nameHeader, entryInfo[2]);            
            Log.Info("Verify that Name column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(nameHeader, entryInfo[2]), "Filtered data for Name is incorrect");

            Log.Info("Filter by ID column");
            HomePage.FilterATableColumn(IDHeader, entryInfo[3]);            
            Log.Info("Verify that ID column is filtered correctly");
            Assert.IsTrue(HomePage.DoesFilterDataDisplayCorrectly(IDHeader, entryInfo[3]), "Filtered data for ID is incorrect");

            //Log.Info("Get entry's info");
            entryInfo = HomePage.tblDashboardTable.GetTableAllCellValueInRow(0);

            Log.Info("Click on ID");
            HomePage.OpenALogEntry(0);

            Log.Info("Verify that existing Log entries are opened");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "Existing Log entry is not opened");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(entryInfo, "Team"), "Existing log entry's data is correct");
            #endregion
        }
    }
}
