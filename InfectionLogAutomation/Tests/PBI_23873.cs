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
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page does not display");
            Assert.IsTrue(HomePage.tblDashboardTableHeader.IsDisplayed(), "Home page does not contain dashboard/table of infections");

            string test = HomePage.tblDashboardTableHeader.GetColumnHeaderAttribute("Last Updated", "data-role");

            Log.Info("Verify that Division column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, divisionHeader), "Division is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, divisionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, divisionHeader), "Division is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, divisionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, divisionHeader), "Division is not sorted by descending order");

            //Logger.Info("Verify that Region column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, regionHeader), "Region is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, regionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, regionHeader), "Region is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, regionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, regionHeader), "Region is not sorted by descending order");

            //Logger.Info("Verify that BU column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, businessUnitHeader), "BU is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, businessUnitHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, businessUnitHeader), "BU is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, businessUnitHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, businessUnitHeader), "BU is not sorted by descending order");

            //Logger.Info("Verify that Community column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, communityHeader), "Community is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, communityHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, communityHeader), "Community is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, communityHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, communityHeader), "Community is not sorted by descending order");

            //Logger.Info("Verify that ID column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, IDHeader), "ID is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, IDHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, IDHeader), "ID is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, IDHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, IDHeader), "ID is not sorted by descending order");

            //Logger.Info("Verify that Name column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, nameHeader), "Name is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, nameHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, nameHeader), "Name is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, nameHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, nameHeader), "Name is not sorted by descending order");

            //Logger.Info("Verify that Resident LOB column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, residentLOBHeader), "Resident LOB is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, residentLOBHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, residentLOBHeader), "Resident LOB is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, residentLOBHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, residentLOBHeader), "Resident LOB is not sorted by descending order");

            //Logger.Info("Verify that Infection Type column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, infectionTypeHeader), "Infection Type is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, infectionTypeHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, infectionTypeHeader), "Infection Type is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, infectionTypeHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, infectionTypeHeader), "Infection Type is not sorted by descending order");

            //Logger.Info("Verify that Test Status column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, testStatusHeader), "Test Status is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, testStatusHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, testStatusHeader), "Test Status is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, testStatusHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, testStatusHeader), "Test Status is not sorted by descending order");

            //Logger.Info("Verify that Disposition column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, dispositionHeader), "Disposition is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, dispositionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, dispositionHeader), "Disposition is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, dispositionHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, dispositionHeader), "Disposition is not sorted by descending order");

            //Logger.Info("Verify that Entry Type column sort correctly");
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortable(DashboardPage.dashboardTblHeader, entryTypeHeader), "Entry Type is not sortable");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, entryTypeHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByAscendingOrder(DashboardPage.dashboardTblHeader, entryTypeHeader), "Entry Type is not sorted by ascending order");
            //TableExtension.ClickOnHeaderColumn(DashboardPage.dashboardTblHeader, entryTypeHeader);
            //Assert.IsTrue(DashboardPage.IsColumnHeaderSortedByDescendingOrder(DashboardPage.dashboardTblHeader, entryTypeHeader), "Entry Type is not sorted by descending order");

            //Logger.Info("Filter by Region column");
            //DashboardPage.ClearAllFilter();
            //DashboardPage.FilterATableColumn(regionHeader, outRegion);
            //Logger.Info("Verify that Region column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(regionHeader, outRegion), "Filtered data for Region is incorrect");

            //Logger.Info("Filter by Community column");
            //DashboardPage.FilterATableColumn(communityHeader, outCommunity);
            //Logger.Info("Verify that Community column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(communityHeader, outCommunity), "Filtered data for Community is incorrect");

            //Logger.Info("Filter by Test Status column");
            //DashboardPage.FilterATableColumn(testStatusHeader, outTestStatus);
            //Logger.Info("Verify that Test Status column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(testStatusHeader, outTestStatus), "Filtered data for Test Status is incorrect");

            //Logger.Info("Filter by Disposition column");
            //DashboardPage.FilterATableColumn(dispositionHeader, outDisposition);
            //Logger.Info("Verify that Disposition column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(dispositionHeader, outDisposition), "Filtered data for Disposition is incorrect");

            //Logger.Info("Filter by Entry Type");
            //DashboardPage.FilterATableColumn("Entry Type", "Team Member");
            //Logger.Info("Verify that Entry Type column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly("Entry Type", "Team Member"), "Filtered data for Entry Type is incorrect");

            //Logger.Info("Filter by Name column");
            //DashboardPage.FilterATableColumn(nameHeader, outEmployeeName);
            //Logger.Info("Verify that Name column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(nameHeader, outEmployeeName), "Filtered data for Name is incorrect");

            //Logger.Info("Filter by ID column");
            //DashboardPage.FilterATableColumn(IDHeader, outEmployeeID);
            //Logger.Info("Verify that ID column is filtered correctly");
            //Assert.IsTrue(DashboardPage.CheckFilterDataDisplayCorrectly(IDHeader, outEmployeeID), "Filtered data for ID is incorrect");

            //Logger.Info("Get entry's info");
            //entryInfo = DashboardPage.GetTableAllCellValueInRow(DashboardPage.dashboardTbl, 0);

            //Logger.Info("Click on ID");
            //DashboardPage.ClickTableCell(DashboardPage.dashboardTbl, 0, 5);

            //Logger.Info("Verify that existing Log entries are opened");
            //Assert.IsTrue(EditTeamLogEntryPage.CheckPageExist(pageTitle), "Existing Log entry is not opened");
            //Assert.IsTrue(EditTeamLogEntryPage.DoesDataOnEditPageDisplayCorrectly(entryInfo, "Team"), "Existing log entry's data is correct");


            #endregion
        }
    }
}
