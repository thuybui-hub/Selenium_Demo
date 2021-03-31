using InfectionLogAutomation.DataObject;
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
    public class PBI_23931 : TestBase
    {
        [Test]
        [Description("IO Log - Enable option to provide 'view only' access")]
        public void PBI_23931_AT_24018()
        {
            #region Test data
            LogEntryData logEntryData = JsonParser.Get<LogEntryData>();
            string fileName = "LGG testing.txt";
            string filePath = Constants.DataPath + fileName;
            string uploadedBy = Constants.AdminUserName;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            int totalRecords, totalReadOnlyRecords;
            #endregion Test data

            #region Pre-condition: Add new Resident log entry with an attachment
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Add a new resident log entry");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            HomePage.OpenALogEntry(logEntryData.MRN);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Get number of Resident records");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("Entry Type", "Resident");
            HomePage.ShowAllILogRecords();
            totalRecords = HomePage.tblDashboard.RowCount();
            #endregion Pre-condition: Add new Resident log entry with an attachment

            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));

            #region Main steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Verify that Home page displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Infectious Outbreak page does not display.");

            Log.Info("Verify that all Resident records display on Home Page");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            totalReadOnlyRecords = HomePage.tblDashboard.RowCount();
            Assert.IsTrue((totalRecords == totalReadOnlyRecords), "Homepage does not show all Resident records");

            Log.Info("Read only member is unable to add new resident log entry for all communities");
            Assert.IsTrue(HomePage.IsReadOnlyUserAbleToAddNewRecords("New Log Entry"), "Read Only user is able to perform a bulk insert.");

            Log.Info("Verify that Delete icon is invisible for selection to delete the existing Resident Log Entry");
            Assert.IsTrue(HomePage.IsUserUnableToDeleteLogEntry(), "User is able to delete log entry.");

            Log.Info("Click on ID of a Resident log entry");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that user is unable to edit resident entry type log entries");
            Assert.IsTrue(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "Read Only user is able to edit resident log entry");

            Log.Info("Verify that Read only member is able to view attachments for a resident log entry");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(fileName, uploadedBy, uploadedDate), "Unable to view the uploaded attachments.");

            Log.Info("Verify that Read only member is unable to add/edit and delete attachments for a resident log entry");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("unable"), "Able to Add attachments.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("unable"), "Able to Delete attachments");

            Log.Info("Verify that Read only member is unable to perform 'Initiate Bulk Insert' for all communities");
            Assert.IsTrue(HomePage.IsReadOnlyUserAbleToAddNewRecords("Bulk Processing"), "Read Only user is able to perform a bulk insert.");

            Log.Info("Verify that Read only member is able to View Case Log Report");
            HomePage.SelectMenuItem("Reports");
            Assert.IsTrue(HomePage.GetSubMenuItems("Reports ").Contains("Resident Case Log"), "Read Only member is unable to view Resident Case Log report");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(logEntryData.MRN);
            #endregion Clean up
        }
    }
}
