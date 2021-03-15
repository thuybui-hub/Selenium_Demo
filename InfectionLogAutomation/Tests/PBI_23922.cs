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
    public class PBI_23922 : TestBase
    {
        [Test]
        [Description("IO Log - Attachment Tab: New Log Entry")]
        public void PBI_23922_AT_24016()
        {
            #region Test data
            LogEntryData logEntryData = JsonParser.Get<LogEntryData>();
            string fileName = "LGG testing.txt";
            string uncommonFileName = "LGGTesting.iaa";
            string filePath = Constants.DataPath + fileName;
            string uncommonFilePath = Constants.DataPath + uncommonFileName;
            string uploadedBy = Constants.ResidentAdminUser;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            #endregion Test data

            #region Pre-condition: There are existing resident log entries with attachments
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            #region Main steps
            Log.Info("3. Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("5. Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field to attach a document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("7. Attach a document that is common format (e.g word or pdf or excel)");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the document is attached successfully");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");

            Log.Info("8. Try to attached new document with same name document at step #6");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that user can overwrite documents of the same name when attaching");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");

            Log.Info("6. Attach a document that is NOT common format (e.g. IAA file) ");
            LogEntryDetailPage.SelectAnAttachment(uncommonFilePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the docment is NOT attached successfully");
            Assert.IsFalse(LogEntryDetailPage.IsDocumentUploadedSuccessfully(uncommonFilePath, uploadedBy, uploadedDate), "The uncommon format document is uploaded successfully.");

            Log.Info("9. Click on Save button");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("Verify that the log entry is saved successfully with attachments");
            logEntryData.InfectionType = "COVID-19";
            HomePage.OpenALogEntry(logEntryData.MRN);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "Uploaded records is unable to be editted.");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");
            #endregion Main steps

            #region Clean up
            DriverUtils.CloseDrivers();
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);
            HomePage.DeleteALogEntry(logEntryData.MRN);
            #endregion Clean up
        }

        [Test]
        [Description("IO Log - Attachment Tab: Edit Log Entry")]
        public void PBI_23922_AT_24017()
        {
            #region Test data
            LogEntryData logEntryData = new LogEntryData();
            string file1Name = "LGG testing.txt";
            string file2Name = "LGG testing1.txt";
            string uncommonFileName = "LGGTesting.iaa";
            string file1Path = Constants.DataPath + file1Name;
            string file2Path = Constants.DataPath + file2Name;
            string uncommonFilePath = Constants.DataPath + uncommonFileName;
            string uploadedBy = Constants.ResidentAdminUser;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            #endregion Test data

            #region Pre-condition: There are existing resident log entries with attachments
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            HomePage.OpenALogEntry(logEntryData.MRN);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            LogEntryDetailPage.SelectAnAttachment(file1Path);
            LogEntryDetailPage.UploadAttachment();
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            #endregion Pre-condition

            #region Main steps
            Log.Info("3. Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("4. Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field for attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("5. Try to delete attached document");
            LogEntryDetailPage.DeleteAnAttachment(file1Name, "OK");

            Log.Info("Verify that resident admin is able to delete attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAttachedDeleted(file1Name), "Admin is unable to delete attached document.");

            Log.Info("7. Attach a document that is common format (e.g word or pdf or excel)");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the document is attached successfully");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate));

            Log.Info("8. Try to attached new document with same name document at step #7");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that user can overwrite documents of the same name when attaching");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate));

            Log.Info("6. Attach a document that is NOT common format (e.g. IAA file) ");
            LogEntryDetailPage.SelectAnAttachment(uncommonFilePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the docment is NOT attached successfully");
            Assert.IsFalse(LogEntryDetailPage.IsDocumentUploadedSuccessfully(uncommonFilePath, uploadedBy, uploadedDate));

            Log.Info("9. Make some changes");
            LogEntryDetailPage.SelectResidentFormTab("Log Entry");
            logEntryData.CurrentTestStatus = "Tested - Pending";
            logEntryData.CurrentDisposition = "Hospitalized";
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", logEntryData.CurrentTestStatus);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", logEntryData.CurrentDisposition);

            Log.Info("10. Click on save button");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the log entry is saved successfully with new attachments");
            logEntryData.InfectionType = "COVID-19";
            HomePage.OpenALogEntry(logEntryData.MRN);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "Uploaded records is unable to be editted.");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");
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
