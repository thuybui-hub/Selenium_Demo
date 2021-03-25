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
            List<List<string>> outListTeamBulkInsert;
            expectedCommunitiesList = ExcelActions.GetCellValuesInColumn(excelPathStaff, "INT049_RPT_MyFVE Communities", "Name", "[INT049_CCF_Division] <> 'Divested Division' AND [INT049_CCF_Division] <> '(Blanks)' AND [Inactive] is null AND [Divestiture Date] is null").Distinct().ToList();
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
            //Assert.IsTrue(HomePage.DoesCreatedBulkInsertRecordsShowCorrectInformation(outListTeamBulkInsert, "Team Bulk"), "Data of Bulk Insert records displays incorrectly.");
            #endregion Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Admin user: sp-test51
            Log.Info("5.1. Logout and login with Team Admin account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamAdminUser, Constants.CommonPassword);

            #endregion Team Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Admin user: sp-test54
            Log.Info("5.2. Logout and login with ​Team Community Admin / Resident Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            #endregion Team Community Admin user

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Team Community Submittor user: sp-test57
            Log.Info("5.3. Logout and login with Team Community Submittor account");
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

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
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
