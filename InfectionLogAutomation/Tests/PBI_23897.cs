using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.DataObject;
using System.Collections.Generic;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23897: TestBase
    {
        [Test]
        [Description("Home Screen Dashboard Data Validation")]
        public void PBI_23897_AT_23899()
        {
            #region Test Data
            LogEntryData logEntryData;
            string pageTitle = "New Infection Log Entry for Team Members";
            
            List<string> acLogEntryData;
            int beforeDel, afterDel;

            // test
            //string title = "Infection Log Entry for Team Member Ginn, Cynthia (274131)";
            //int t1 = title.IndexOf(",");
            //string titl1 = title.Substring(0, t1);

            //int t2 = titl1.LastIndexOf(" ");
            //string fn = title.Substring(t2 + 1, t1 - t2 - 1);
            // end test
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team
            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The user does not remains on New Log Entry");

            Log.Info("4. Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("5. Filter log entry is just created above");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);

            Log.Info("Verify that the log entry is saved");            
            beforeDel = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(beforeDel > 0, "The log entry is not saved");
            acLogEntryData = HomePage.tblDashboard.GetTableAllCellValueInRow(0);

            Log.Info("Verify that new log entry above is shown in Dashboard table with contents like at the time the log entry is created");            
            Assert.IsTrue(HomePage.DoesLogEntryDataMatchDashboard("Team", acLogEntryData, logEntryData), "Saved log entry is shown in Dashboard table with incorrect contents");

            Log.Info("6. Open the log entry just created");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that line item content should match content in a form, and be in the same fields");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "Line item content in Dashboard and form do not match each other");

            Log.Info("7. Make the change");
            logEntryData.Symptoms = logEntryData.Symptoms + "changed";
            logEntryData.Comments = logEntryData.Comments + "changed";
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");

            Log.Info("8. Save the changes");
            LogEntryDetailPage.btnSaveLogEntry.Click();

            Log.Info("9. Get new information on the Dashboard");
            acLogEntryData = HomePage.tblDashboard.GetTableAllCellValueInRow(0);

            Log.Info("Verify that line item content should match content in a form, and be in the same fields");
            Assert.IsTrue(HomePage.DoesLogEntryDataMatchDashboard("Team", acLogEntryData, logEntryData), "Saved log entry is shown in Dashboard table with incorrect contents");

            Log.Info("10. Delete above log entry");
            HomePage.DeleteLogEntries(1, "top");
            afterDel = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that the log entry is deleted successfully");
            Assert.IsTrue(afterDel == beforeDel - 1, "The log entry is not deleted successfully");
            HomePage.ClearAllFilters();
            #endregion Team

            #region Resident

            #endregion Resident

            #region Client

            #endregion Client

            #endregion
        }
    }
}
