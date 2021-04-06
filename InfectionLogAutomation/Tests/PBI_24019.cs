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
    public class PBI_24019 : TestBase
    {
        [Test]
        [Description("IO Log - Attachment Tab Security")]
        public void PBI_24019_AT_24065()
        {
            #region Test data
            LogEntryData logEntryData = new LogEntryData();
            string file1Name = "LGG testing.txt";
            string file2Name = "LGG testing1.txt";
            string file1Path = Constants.DataPath + file1Name;
            string file2Path = Constants.DataPath + file2Name;
            string uploadedDate = DateTime.Now.ToString("MM/dd/yyyy");
            string uploadedBy;
            #endregion Test data

            #region Pre-condition: There are existing resident log entries with attachments
            DriverUtils.GoToUrl(Constants.Url);
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            HomePage.OpenALogEntry(logEntryData.MRN);
            LogEntryDetailPage.SelectResidentFormTab("Attachments");
            LogEntryDetailPage.SelectAnAttachment(file1Path);
            LogEntryDetailPage.UploadAttachment();
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            DriverUtils.CloseDrivers();
            #endregion Pre-condition

            #region Main steps
            #region Admin user: gnguyen
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field for attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("Verify that Admin user is able to delete attachment.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment(), "Admin user is unable to Delete attachments");

            Log.Info("Try to delete attached document");
            LogEntryDetailPage.DeleteAnAttachment(file1Name, "OK");

            Log.Info("Verify that attached document is deleted successfully.");
            Assert.IsTrue(LogEntryDetailPage.IsAttachedDeleted(file1Name), "Admin is unable to delete attached document.");

            Log.Info("Try to attach a document");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that Admin user is able to attach/view a document");
            uploadedBy = Constants.AdminUserName;
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate), "Unable to view the uploaded attachments.");
            #endregion Admin user: gnguyen

            DriverUtils.CloseDrivers();

            #region Resident Admin user: sp-test52
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. sp-test52)");
            LoginPage.Login(Constants.ResidentAdminUser, Constants.CommonPassword);

            Log.Info("Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field for attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("Verify that Resident Admin user is able to delete attachment.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment(), "Admin user is unable to Delete attachments");

            Log.Info("Try to delete attached document");
            LogEntryDetailPage.DeleteAnAttachment(file2Name, "OK");

            Log.Info("Verify that attached document is deleted successfully.");
            Assert.IsTrue(LogEntryDetailPage.IsAttachedDeleted(file2Name), "Admin is unable to delete attached document.");

            Log.Info("Try to attach a document");
            LogEntryDetailPage.SelectAnAttachment(file1Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that Resident Admin user is able to attach/view a document");
            uploadedBy = Constants.ResidentAdminUser;
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file1Name, uploadedBy, uploadedDate), "Unable to view the uploaded attachments.");
            #endregion Resident Admin user: sp-test52

            DriverUtils.CloseDrivers();

            #region Resident Community Admin user: sp-test54
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. sp-test55)");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that there is a field for attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment(), "There is no field to attach a document.");

            Log.Info("Verify that Resident Community Admin user is able to delete attachment.");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment(), "Admin user is unable to Delete attachments");

            Log.Info("Try to delete attached document");
            LogEntryDetailPage.DeleteAnAttachment(file1Name, "OK");

            Log.Info("Verify that attached document is deleted successfully.");
            Assert.IsTrue(LogEntryDetailPage.IsAttachedDeleted(file1Name), "Admin is unable to delete attached document.");

            Log.Info("Try to attach a document");
            LogEntryDetailPage.SelectAnAttachment(file2Path);
            LogEntryDetailPage.UploadAttachment();

            Log.Info("Verify that Resident Community user is able to attach/view a document");
            uploadedBy = Constants.ResidentCommunityAdminUser;
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate), "Unable to view the uploaded attachments.");
            #endregion Resident Submittor user: sp-test55

            DriverUtils.CloseDrivers();

            #region Resident Readonly user: sp-test58
            Log.Info("Navigate to Infectious Outbreak application (http://qa.ilog.fve.ad.5ssl.com/)");
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath, Constants.Driver));
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.ResidentReadOnlyUser, Constants.CommonPassword);

            Log.Info("Open an existing log entry at pre-condition");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Open Attachments tab");
            LogEntryDetailPage.SelectResidentFormTab("Attachments");

            Log.Info("Verify that Resident Readonly is unable to delete attached document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToDeleteAttachment("unable"), "Able to Delete attachments");

            Log.Info("Verify that Resident Readonly is unable to attach a document");
            Assert.IsTrue(LogEntryDetailPage.IsAbleToAddAttachment("unable"), "Able to Add attachments.");

            Log.Info("Verify that Resident Readonly is able to view the attached document");
            uploadedBy = Constants.ResidentCommunityAdminUser;
            Assert.IsTrue(LogEntryDetailPage.IsDocumentUploadedSuccessfully(file2Name, uploadedBy, uploadedDate), "Unable to view the uploaded attachments.");
            #endregion Resident Readonly user: sp-test58
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
