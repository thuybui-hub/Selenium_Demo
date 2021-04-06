using InfectionLogAutomation.DataObject;
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
    public class PBI_25351 : TestBase
    {
        [Test]
        [Description("IO Log - Add Ageility Profile")]
        public void PBI_25351_AT_25389()
        {
            #region Test data
            List<string> communityList;
            List<string> expNewLogEntryOptions = new List<string>() { "Team" };
            List<string> expBulkOptions = new List<string>() { "Insert Team", "Edit Team" };
            LogEntryData logEntryData = new LogEntryData();
            Random rd = new Random();
            int resultRows, resultRowsAfterDelete;
            #endregion Test data

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with members of Ageility profile: (e.g. sp-test53)");
            LoginPage.Login(Constants.AgeilityProfileMembers, Constants.CommonPassword);

            Log.Info("Verify that these users are able to input new Ageility team member records only");
            HomePage.SelectMenuItem("New Log Entry");
            Assert.IsTrue(HomePage.GetSubMenuItems("New Log Entry").SequenceEqual(expNewLogEntryOptions), "Users are able to input new records for other type of entry.");
            HomePage.SelectMenuItem("Bulk Processing");
            Assert.IsTrue(HomePage.GetSubMenuItems("Bulk Processing").SequenceEqual(expBulkOptions), "Users are able to input new bulk insert for other type of entry.");

            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            communityList = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            Assert.IsTrue(communityList.All(x => x.Contains("Ageility")), "Users are able to input new Ageility team member records only.");

            HomePage.SelectMenuItem(Constants.NewTeamBulkInsertPath);
            communityList = BulkInsertPage.GetItemsFromControlList(Fields.community);
            Assert.IsTrue(communityList.All(x => x.Contains("Ageility")), "Users are able to input new Ageility team member records only.");

            Log.Info("3. Input a new log entry");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the log entry is created successfully.");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            resultRows = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(resultRows > 0, "New log entry is saved");
            HomePage.ClearAllFilters();

            Log.Info("4. Edit a log entry ");
            HomePage.OpenALogEntry(logEntryData.MRN);
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that these users are able to edit Ageility team member records only");
            HomePage.OpenALogEntry(logEntryData.MRN);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "The change is NOT saved");

            Log.Info("5. Delete a log entry ");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            HomePage.DeleteALogEntry(logEntryData.MRN);

            Log.Info("Verify that these users are able to delete Ageility team member records only");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            resultRowsAfterDelete = HomePage.tblDashboard.RowCount();
            Assert.IsTrue((resultRowsAfterDelete == resultRows - 1) || HomePage.divNoRecords.IsDisplayed(), "Log entry is delete");
            #endregion Main steps
        }
    }
}
