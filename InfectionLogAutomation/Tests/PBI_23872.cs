using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23872 : TestBase
    {
        [TestCase("fve\\sp-test51", "Password1")]
        [Description("IO Log - Roles and Permission Updates: Team: DVP; DDHR is able to create, edit and delete Team Log entry")]
        public void PBI_23872_AT_23888_DVP_DDHR(string userName, string password)
        {
            #region Test Data
            LogEntryData logEntryData;
            List<string> exSubOptions = new List<string>() { "Team" };
            List<string> acSubOption;
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Team Member";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(userName, password);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            acSubOption = HomePage.GetSubMenuItems("New Log Entry");
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that only Team is visible under New Log Entry menu");
            Assert.IsTrue(acSubOption.SequenceEqual(exSubOptions), "Resident & Client are also included under New Log Entry");

            Log.Info("Verify that Delete link at the end of each record is enable");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "The Delete link is invisible");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "The change is NOT saved");

            Log.Info("10. Go to Home page");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("11. Delete a log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteALogEntry(logEntryData.MRN);

            Log.Info("12. Get number of current rows after a log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that the log entry is deleted successfully");
            Assert.IsTrue(afterCount < beforeCount, "The log entry is not deleted successfully");
            #endregion
        }

        [Test]
        [Description("IO Log - Roles and Permission Updates: Team: RDO, Senior RDO & RDHR are able to create, edit and delete Team Log entry for their assigned region")]
        public void PBI_23872_AT_23888_RDO_RDHR()
        {
            #region Test Data
            LogEntryData logEntryData;
            List<string> exSubOptions = new List<string>() { "Team" };
            List<string> acSubOption;
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Team Member";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.TeamCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            acSubOption = HomePage.GetSubMenuItems("New Log Entry");
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that only Team is visible under New Log Entry menu");
            Assert.IsTrue(acSubOption.SequenceEqual(exSubOptions), "Resident & Client are also included under New Log Entry");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "The change is NOT saved");

            Log.Info("10. Go to Home page");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("11. Delete a log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteALogEntry(logEntryData.MRN);

            Log.Info("12. Get number of current rows after a log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that the log entry is deleted successfully");
            Assert.IsTrue(afterCount < beforeCount, "The log entry is not deleted successfully");
            #endregion
        }

        [Test]
        [Description("IO Log - Roles and Permission Updates: Team: ED; HR is able to create, edit Team Log entry but not delete the log entry")]
        public void PBI_23872_AT_23888_ED_HR()
        {
            #region Test Data
            LogEntryData logEntryData;
            List<string> exSubOptions = new List<string>() { "Team" };
            List<string> acSubOption;
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Team Member";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.TeamCommunitySubmittorUser, Constants.CommonPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            acSubOption = HomePage.GetSubMenuItems("New Log Entry");
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that only Team is visible under New Log Entry menu");
            Assert.IsTrue(acSubOption.SequenceEqual(exSubOptions), "Resident & Client are also included under New Log Entry");

            Log.Info("Verify that Delete link at the end of each record is invisible to delete");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("unable"), "The Delete link is invisible");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(false), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";            
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Team", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Team"), "The change is NOT saved");
            #endregion
        }

        [TestCase("fve\\sp-test52", "Password1")]
        [TestCase("fve\\sp-test55", "Password1")]
        [Description("IO Log - Roles and Permission Updates: Team: DHR & RDH have no permission to access Team Log Entry")]
        public void PBI_23872_AT_23888_DHR_RDH(string userName, string pass)
        {
            #region Test Data
            string subOptions;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(userName, pass);

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            subOptions = string.Join(", ", HomePage.GetSubMenuItems("New Log Entry"));
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that Team option under New Log Entry is invisible for accessing");
            Assert.IsFalse(subOptions.Contains("Team"), "The users are able to access Team Log Entry");
            #endregion
        }

        [TestCase("fve\\sp-test52", "Password1")]
        [TestCase("fve\\gnguyen", "Welcome@0121")]
        [Description("IO Log - Roles and Permission Updates: Resident: DDH & admin are able to view/edit/delete Resident Log Entry")]
        public void PBI_2372_AT_23889_DDH_Admin(string userName, string password)
        {
            #region Test Data
            LogEntryData logEntryData;
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Resident";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(userName, password);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that Delete link at the end of each record is enable");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "The Delete link is invisible");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Resident", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "The change is NOT saved");

            Log.Info("10. Go to Home page");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("11. Delete a log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteALogEntry(logEntryData.MRN);

            Log.Info("12. Get number of current rows after a log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that the log entry is deleted successfully");
            Assert.IsTrue(afterCount < beforeCount, "The log entry is not deleted successfully");
            #endregion
        }

        [TestCase("fve\\sp-test51", "Password1")]
        [TestCase("fve\\sp-test54", "Password1")]
        [TestCase("fve\\sp-test57", "Password1")]
        [Description("IO Log - Roles and Permission Updates: Resident: Some roles have no permission to access Resident Log Entry")]
        public void PBI_2372_AT_23889(string userName, string pass)
        {
            #region Test Data
            string subOptions;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(userName, pass);

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            subOptions = string.Join(", ", HomePage.GetSubMenuItems("New Log Entry"));
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that Team option under New Log Entry is invisible for accessing");
            Assert.IsFalse(subOptions.Contains("Resident"), "The users are able to access Resident Log Entry");
            #endregion
        }

        [Test]
        [Description("IO Log - Roles and Permission Updates: Resident: RDH is able to view/edit Resident Log Entry and unable to delete a Resident Log Entry")]
        public void PBI_2372_AT_23889_RDH()
        {
            #region Test Data
            LogEntryData logEntryData;
            List<string> exSubOptions = new List<string>() { "Team" };            
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Resident";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.ResidentCommunityAdminUser, Constants.CommonPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that Delete link at the end of each record is invisible to delete");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("unable"), "The Delete link is invisible");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Resident", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Resident"), "The change is NOT saved");
            #endregion
        }

        [Test]
        [Description("IO Log - Roles and Permission Updates: Client: admin are able to view/edit/delete Resident Log Entry")]
        public void PBI_2372_AT_23890_Admin()
        {
            #region Test Data
            LogEntryData logEntryData;
            Random rd = new Random();
            string pageTitle = "Infection Log Entry for Ageility Client";
            int beforeCount, afterCount;
            #endregion

            #region Main Steps            
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page containing dashboard/table of infections displays");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "The Homepage does not display");

            Log.Info("Get current number of rows before a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that Delete link at the end of each record is enable");
            Assert.IsTrue(HomePage.CanUserDeleteLogEntry("able"), "The Delete link is invisible");

            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition

            Log.Info("4. Get number of rows after a new log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that new log entry is created successfully");
            Assert.IsTrue(afterCount > beforeCount, "The log entry is not created successfully");

            Log.Info("5. Click on ID to open a log entry created above");
            HomePage.FilterATableColumn("ID", logEntryData.MRN);
            beforeCount = HomePage.tblDashboard.RowCount();
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that new log entry page opens");
            Assert.IsTrue(LogEntryDetailPage.CheckPageExist(pageTitle), "The edit page does not display");

            Log.Info("Verify that fields are enable to edit");
            Assert.IsFalse(LogEntryDetailPage.AreFieldsUnableToUpdateLogEntryInfo(), "The fields are unable to edit");

            Log.Info("6. Make the changes");
            logEntryData.Symptoms = logEntryData.Symptoms + " changed";
            logEntryData.Comments = logEntryData.Comments + " changed";
            logEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            logEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(logEntryData, "Client", "Edit");

            Log.Info("7. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Get number of current rows again before a log entry is deleted");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Click on ID to open a log entry created above");
            HomePage.OpenALogEntry(logEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(logEntryData, "Client"), "The change is NOT saved");

            Log.Info("10. Go to Home page");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);

            Log.Info("11. Delete a log entry");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.DeleteALogEntry(logEntryData.MRN);

            Log.Info("12. Get number of current rows after a log entry is created");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();

            Log.Info("Verify that the log entry is deleted successfully");
            Assert.IsTrue(afterCount < beforeCount, "The log entry is not deleted successfully");
            #endregion
        }

        [TestCase("fve\\sp-test51", "Password1")]
        [TestCase("fve\\sp-test52", "Password1")]
        [TestCase("fve\\sp-test54", "Password1")]
        [TestCase("fve\\sp-test55", "Password1")]
        [TestCase("fve\\sp-test57", "Password1")]
        [Description("IO Log - Roles and Permission Updates: Client: Some roles have no permission to access Resident Log Entry")]
        public void PBI_2372_AT_23890(string userName, string pass)
        {
            #region Test Data
            string subOptions;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(userName, pass);

            Log.Info("3. Get all sub options under New Log Entry menu");
            HomePage.SelectMenuItem("New Log Entry");
            subOptions = string.Join(", ", HomePage.GetSubMenuItems("New Log Entry"));
            HomePage.SelectMenuItem("New Log Entry");

            Log.Info("Verify that Team option under New Log Entry is invisible for accessing");
            Assert.IsFalse(subOptions.Contains("Ageility Client"), "The users are able to access Resident Log Entry");
            #endregion
        }
    }
}

