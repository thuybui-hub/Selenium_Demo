using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using SeleniumCSharp.Core.Utilities;
using System;
using System.Linq;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23866: TestBase
    {
        [Test]
        [Description("Infectious Outbreak Log Entry: Pre-populated Data Validation")]
        public void PBI_23866_AT_23887()
        {
            #region Test Data
            List<string> actualRegionsList, actualCommunitiesList, actualEmployeesList, communitiesList, employeesList, BU;
            string excelPathStaff = Constants.DataPath + "INT_Staff_Report.xls";
            string excelPathCommunities = Constants.DataPath + "INT049_RPT_MyFVE_Communities.xls";
            List<string> regionList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Region", "[Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            regionList.Sort();
            regionList.Remove("Rehab/Wellness");

            Random rd = new Random();
            string region, community;
            #endregion

            #region Main  Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team
            Log.Info("3. Go to New Team Log Entry");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Get list of region");
            actualRegionsList = LogEntryDetailPage.GetItemsFromControlList(Fields.region);
            actualRegionsList.Sort();

            Log.Info("Verify that list of regions on form and Workday match each other");
            Assert.IsTrue(actualRegionsList.SequenceEqual(regionList), "List of regions on form and Workday do not match each other");

            Log.Info("Select a region");
            region = actualRegionsList[rd.Next(0, actualRegionsList.Count - 1)];
            LogEntryDetailPage.txtRegion.SendKeys(region);            

            Log.Info("Get list of communities basing on selected region");            
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);

            Log.Info("Get list of communities from Workday");
            communitiesList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Community Name", "[Inactive] is null AND [Divestiture Date] is null AND [Region] = '" + region + "'").Distinct().ToList();
            communitiesList.Sort();

            Log.Info("Verify that list of locations on form and Workday match each other");
            Assert.IsTrue(actualCommunitiesList.SequenceEqual(communitiesList), "List of communities on form and Workday do not match each other");

            Log.Info("Select a community");
            community = actualCommunitiesList[rd.Next(0, actualCommunitiesList.Count - 1)];
            LogEntryDetailPage.txtCommunity.SendKeys(community);
            DriverUtils.wait(4);
            BU = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Code", "[Community Name] = '" + community + "' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();

            Log.Info("Get list of employees basing on selected community");            
            actualEmployeesList = LogEntryDetailPage.GetItemsFromControlList(Fields.employee);
            actualEmployeesList.Sort();

            Log.Info("Get list of employees from Workday");
            employeesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "Staffs", "Employee", "[Currently Active] = 'Yes' AND [Cost Center - ID] = '" + BU[0] + "'").Distinct().ToList();
            employeesList.Sort();

            Log.Info("Verify that list of employees name and ID on form and Workday match each other");
            Assert.IsTrue(employeesList.SequenceEqual(actualEmployeesList), "list of employees name and ID on form and Workday do not match each other");
            #endregion Team

            #region Resident
            Log.Info("Get list of regions from Workday");
            regionList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Region", "[Division] = 'Division A' OR [Division] = 'Division B' OR [Division] = 'Division C' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            regionList.Sort();

            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("Get list of regions on the site");            
            actualRegionsList = LogEntryDetailPage.GetItemsFromControlList(Fields.region);

            Log.Info("Verify that list of regions on form and Workday match each other");
            actualRegionsList.Sort();
            regionList.Sort();
            Assert.IsTrue(actualRegionsList.SequenceEqual(regionList), "List of regions on form and Workday do not match each other");

            Log.Info("Select a region");
            region = actualRegionsList[rd.Next(0, actualRegionsList.Count - 1)];
            LogEntryDetailPage.txtRegion.SendKeys(region);

            Log.Info("Get list of communities basing on selected region");            
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();

            Log.Info("Get list of communities from Workday");
            communitiesList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Community Name", "[Inactive] is null AND [Divestiture Date] is null AND [Region] = '" + region + "'").Distinct().ToList();
            communitiesList.Sort();

            Log.Info("Verify that list of locations on form and Workday match each other");
            Assert.IsTrue(actualCommunitiesList.SequenceEqual(communitiesList), "List of communities on form and Workday do not match each other");

            //Logger.Info("Select a community");
            //community = actualCommunitiesList[rd.Next(0, actualCommunitiesList.Count - 1)];
            //NewResidentLogEntryPage.FillControl(NewResidentLogEntryPage.communityTxt, community);
            //RunSettings.Pause(4000);            

            //Logger.Info("Get list of resident basing on selected community");
            //NewResidentLogEntryPage.residentTxt.Click();
            //actualResidentsList = NewResidentLogEntryPage.GetItemsFromControlList(Fields.person);
            //actualResidentsList.Sort();  
            #endregion Resident

            #region Client
            Log.Info("Get list of regions from Workday");
            regionList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Region", "[Division] = 'Ageility Corporate' OR [Division] = 'Ageility Eastern' OR [Division] = 'Ageility Southeastern' OR [Division] = 'Ageility Western' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
            regionList.Sort();

            Log.Info("Go to New Log Entry -> Ageility Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("Get list of regions on the site");            
            actualRegionsList = LogEntryDetailPage.GetItemsFromControlList(Fields.region);

            Log.Info("Verify that list of regions on form and Workday match each other");
            actualRegionsList.Sort();
            regionList.Sort();
            Assert.IsTrue(actualRegionsList.SequenceEqual(regionList), "List of regions on form and Workday do not match each other");

            Log.Info("Select a region");
            region = actualRegionsList[rd.Next(0, actualRegionsList.Count - 1)];
            LogEntryDetailPage.txtRegion.SendKeys(region);

            Log.Info("Get list of communities basing on selected region");            
            actualCommunitiesList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            actualCommunitiesList.Sort();

            Log.Info("Get list of communities from Workday");
            communitiesList = ExcelActions.GetCellValuesInColumn(excelPathCommunities, "Communities", "Community Name", "[Inactive] is null AND [Divestiture Date] is null AND [Region] = '" + region + "'").Distinct().ToList();
            communitiesList.Sort();

            Log.Info("Verify that list of locations on form and Workday match each other");
            Assert.IsTrue(actualCommunitiesList.SequenceEqual(communitiesList), "List of communities on form and Workday do not match each other");
            #endregion Client
            #endregion
        }
    }
}
