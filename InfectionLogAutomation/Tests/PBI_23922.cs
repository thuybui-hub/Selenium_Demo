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
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23922 : TestBase
    {
        [Test]
        [Description("IO Log - Attachment Tab: New Log Entry")]
        public void PBI_23922_AT_24016()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryData = new TeamLogEntryInfo();
            List<string> outLstResult = new List<string> { };
            string fileName = "LGG testing.txt";
            string uncommonFileName = "LGGTesting.iaa";
            string filePath = Constants.DataPath + fileName;
            string uncommonFilePath = Constants.DataPath + uncommonFileName;
            string uploadedBy = Constants.ResidentAdminUser;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            #endregion Test data

            #region Main steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("3. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("4. Fill all required fields");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out outLstResult);

            Log.Info("5. Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field to attach a document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("7. Attach a document that is common format (e.g word or pdf or excel)");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the document is attached successfully");
            Assert.IsTrue(LogEntryDetailPage.IsUploadedDocumentDisplayed(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");

            Log.Info("8. Try to attached new document with same name document at step #6");
            LogEntryDetailPage.SelectAnAttachment(filePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that user can overwrite documents of the same name when attaching");
            Assert.IsTrue(LogEntryDetailPage.IsUploadedDocumentDisplayed(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");

            Log.Info("6. Attach a document that is NOT common format (e.g. IAA file) ");
            LogEntryDetailPage.SelectAnAttachment(uncommonFilePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the docment is NOT attached successfully");
            Assert.IsFalse(LogEntryDetailPage.IsUploadedDocumentDisplayed(uncommonFilePath, uploadedBy, uploadedDate), "The uncommon format document is uploaded successfully.");

            Log.Info("9. Click on Save button");
            LogEntryDetailPage.SelectResidentFormTab("Log Entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the log entry is saved successfully with attachments");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(outLstResult, "Resident"), "Uploaded records is unable to be editted.");
            Assert.IsTrue(LogEntryDetailPage.IsUploadedDocumentDisplayed(fileName, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");
            #endregion Main steps
        }

        [Test]
        [Description("IO Log - Attachment Tab: Edit Log Entry")]
        public void PBI_23922_AT_24017()
        {
            #region Test data
            TeamLogEntryInfo teamLogEntryData = new TeamLogEntryInfo();
            List<string> outLstResult = new List<string> { };
            string file1Name = "LGG testing.txt";
            string file2Name = "LGG testing1.txt";
            string uncommonFileName = "LGGTesting.iaa";
            string file1Path = Constants.DataPath + file1Name;
            string file2Path = Constants.DataPath + file2Name;
            string uncommonFilePath = Constants.DataPath + uncommonFileName;
            string uploadedBy = Constants.ResidentAdminUser;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            teamLogEntryData.CurrentTestStatus = "Tested - Pending";
            teamLogEntryData.CurrentDisposition = "Hospitalized";
            #endregion Test data

            #region Pre-condition: There are existing resident log entries with attachments
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out outLstResult);
            LogEntryDetailPage.SaveLogEntry();
            DriverUtils.CloseDrivers();
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Launch the site");
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("3. Open an existing log entry at pre-condition");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);

            Log.Info("4. Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field for attached document");
            Assert.IsTrue(LogEntryDetailPage.btnSelectFiles.IsDisplayed() && LogEntryDetailPage.btnSelectFiles.IsEnabled(), "There is no field to attach a document.");

            Log.Info("5. Try to delete attached document");
            LogEntryDetailPage.DeleteAnAttachment(file1Name, "OK");

            Log.Info("Verify that resident admin is able to delete attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAttachedDeleted(file1Name), "Admin is unable to delete attached document.");

            Log.Info("7. Attach a document that is common format (e.g word or pdf or excel)");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the document is attached successfully");
            Assert.IsFalse(LogEntryDetailPage.IsUploadedDocumentDisplayed(file2Name, uploadedBy, uploadedDate));

            Log.Info("8. Try to attached new document with same name document at step #7");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that user can overwrite documents of the same name when attaching");
            Assert.IsFalse(LogEntryDetailPage.IsUploadedDocumentDisplayed(file2Name, uploadedBy, uploadedDate));

            Log.Info("6. Attach a document that is NOT common format (e.g. IAA file) ");
            LogEntryDetailPage.SelectAnAttachment(uncommonFilePath);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that the docment is NOT attached successfully");
            Assert.IsFalse(LogEntryDetailPage.IsUploadedDocumentDisplayed(uncommonFilePath, uploadedBy, uploadedDate));

            Log.Info("9. Make some changes");
            LogEntryDetailPage.SelectResidentFormTab("Log Entry");
            LogEntryDetailPage.SelectATestStatusOrDisposition("Test Status", teamLogEntryData.CurrentTestStatus);
            LogEntryDetailPage.SelectATestStatusOrDisposition("Disposition", teamLogEntryData.CurrentDisposition);

            Log.Info("10. Click on save button");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the log entry is saved successfully with new attachments");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.OpenALogEntry(outLstResult[3]);
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(outLstResult, "Resident"), "Uploaded records is unable to be editted.");
            Assert.IsTrue(LogEntryDetailPage.IsUploadedDocumentDisplayed(file2Name, uploadedBy, uploadedDate), "The document is uploaded unsuccessfully.");
            #endregion Main steps
        }
    }
}
