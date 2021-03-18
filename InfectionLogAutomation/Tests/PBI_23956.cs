using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23956: TestBase
    {
        [Test]
        [Description("IO Log - Current Disposition - Add Recovered-Transferred")]
        public void PBI_23956_AT_23969_23970()
        {
            #region Test Data
            List<string> lstdisposition, entryInfo;
            string disposition = "Transferred/Discharged";
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team Memeber
            #region AT_23969: New Log Entry forms
            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4.Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("5. Fill all required fields with Current Disposition = 'Transferred/Discharged'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out entryInfo);
            LogEntryDetailPage.spnDisposition.Click();
            LogEntryDetailPage.lstBoxDisposition.SelectOptionByText(disposition);

            Log.Info("6. Save log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("7. Show all records on the Dashboard page");
            HomePage.ShowBothActiveAndInactiveRecords();

            Log.Info("8. Filter the log entry just created");
            HomePage.FilterATableColumn("ID", entryInfo[3]);

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved successfully");
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");
            #endregion AT_23969: New Log Entry forms

            #region AT_23970: Edit Log Entry forms
            Log.Info("9. Open the log entry created");
            HomePage.OpenALogEntry(0);

            Log.Info("10. Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("11. Save the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", entryInfo[3]);
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");

            Log.Info("Delete log entry created");
            HomePage.DeleteALogEntry(entryInfo[3]);
            #endregion AT_23970: Edit Log Entry forms
            #endregion Team Member

            #region Resident
            #region AT_23969: New Log Entry forms
            Log.Info("3. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("4.Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("5. Fill all required fields with Current Disposition = 'Transferred/Discharged'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out entryInfo);
            LogEntryDetailPage.spnDisposition.Click();
            LogEntryDetailPage.lstBoxDisposition.SelectOptionByText(disposition);

            Log.Info("6. Save log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("7. Show all records on the Dashboard page");
            HomePage.ShowBothActiveAndInactiveRecords();

            Log.Info("8. Filter the log entry just created");
            HomePage.FilterATableColumn("ID", entryInfo[3]);

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved successfully");
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");
            #endregion AT_23969: New Log Entry forms

            #region AT_23970: Edit Log Entry forms
            Log.Info("9. Open the log entry created");
            HomePage.OpenALogEntry(0);

            Log.Info("10. Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("Save the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", entryInfo[3]);
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");

            Log.Info("Delete log entry created");
            HomePage.DeleteALogEntry(entryInfo[3]);
            #endregion AT_23970: Edit Log Entry forms
            #endregion Resident

            #region Client
            #region AT_23969: New Log Entry forms
            Log.Info("3. Go to New Log Entry -> Ageility Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("4.Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("5. Fill all required fields with Current Disposition = 'Transferred/Discharged'");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out entryInfo);
            LogEntryDetailPage.spnDisposition.Click();
            LogEntryDetailPage.lstBoxDisposition.SelectOptionByText(disposition);

            Log.Info("6. Save log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("7. Show all records on the Dashboard page");
            HomePage.ShowBothActiveAndInactiveRecords();

            Log.Info("8. Filter the log entry just created");
            HomePage.FilterATableColumn("ID", entryInfo[3]);

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            Assert.IsTrue(HomePage.tblDashboard.RowCount() > 0, "The log entry is not saved successfully");
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");
            #endregion AT_23969: New Log Entry forms

            #region AT_23970: Edit Log Entry forms
            Log.Info("9. Open the log entry created");
            HomePage.OpenALogEntry(0);

            Log.Info("10. Get list of dispositions");
            lstdisposition = LogEntryDetailPage.GetItemsFromControlList(Fields.disposition);

            Log.Info("Verify that Current disposition drop down includes option \"Transferred\"");
            Assert.IsTrue(lstdisposition.Contains(disposition), "The disposition drop down does not include option 'Transferred/Discharged'");

            Log.Info("Save the log entry");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("Verify that the new log entry is saved successfully and displayed in the Dashboard table with value 'Transferred' in column 'Diposition''");
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", entryInfo[3]);
            Assert.IsTrue(HomePage.tblDashboard.GetTableCellValue(11, 0).Equals(disposition), "Incorrect disposition shows on Dashboard");

            Log.Info("Delete log entry created");
            HomePage.DeleteALogEntry(entryInfo[3]);
            #endregion AT_23970: Edit Log Entry forms
            #endregion Client
            #endregion
        }
    }
}
