using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.DataObject;
using System;
using SeleniumCSharp.Core.Utilities;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23877: TestBase
    {
        [Test]
        [Description("Log Form Save and Cancel - New log entry form")]
        public void PBI_23877_AT_23893()
        {
            #region Test Data
            LogEntryData logEntryData;
            int beforeCount, afterCount, resultRows;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team            
            Log.Info("Get all current records on the Dashboard");            
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("4. Fill log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);

            Log.Info("5. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("Verify that new log entry is not saved");            
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(beforeCount == afterCount, "New log entry is saved");            
                        
            Log.Info("6. Go to new log entry page");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("7. Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("8. Filter log entry is just created above");            
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);

            Log.Info("Verify that new log entry is saved");
            resultRows = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(resultRows > 0, "New log entry is saved");

            Log.Info("Clean up: Delete the log entry created above");
            HomePage.DeleteLogEntries(1, "top");
            #endregion Team

            #region Resident
            Log.Info("Get all current records on the Dashboard");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("9. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("10. Fill log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);

            Log.Info("11. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("Verify that new log entry is not saved");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();
            System.Console.WriteLine(beforeCount);
            System.Console.WriteLine(afterCount);
            Assert.IsTrue(beforeCount == afterCount, "New log entry is saved");

            Log.Info("12. Go to new log entry page");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("13. Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("14. Filter log entry is just created above");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);

            Log.Info("Verify that new log entry is saved");
            resultRows = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(resultRows > 0, "New log entry is saved");

            Log.Info("Clean up: Delete the log entry created above");
            HomePage.DeleteLogEntries(1, "top");
            #endregion Resident

            #region Client
            Log.Info("Get all current records on the Dashboard");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            beforeCount = HomePage.tblDashboard.RowCount();

            Log.Info("15. Go to New Log Entry -> Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("16. Fill log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out logEntryData);

            Log.Info("17. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("Verify that new log entry is not saved");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.ShowAllILogRecords();
            afterCount = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(beforeCount == afterCount, "New log entry is saved");

            Log.Info("18. Go to new log entry page");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("19. Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out logEntryData);
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("20. Filter log entry is just created above");
            HomePage.ClearAllFilters();
            HomePage.ShowBothActiveAndInactiveRecords();
            HomePage.FilterATableColumn("ID", logEntryData.MRN);

            Log.Info("Verify that new log entry is saved");
            resultRows = HomePage.tblDashboard.RowCount();
            Assert.IsTrue(resultRows > 0, "New log entry is saved");

            Log.Info("Clean up: Delete the log entry created above");
            HomePage.DeleteLogEntries(1, "top");
            #endregion Client
            #endregion
        }

        [Test]
        [Description("Log Form Save and Cancel - Existing log entry form")]
        public void PBI_23877_AT_23894()
        {
            #region Test Data
            LogEntryData originalLogEntryData;
            LogEntryData newLogEntryData = JsonParser.Get<LogEntryData>();
            Random rd = new Random();
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            #region Team            
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Team", out originalLogEntryData);
            LogEntryDetailPage.SaveLogEntry();            
            #endregion Pre-condition            

            Log.Info("3. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("4. Make the changes");
            newLogEntryData.Symptoms = newLogEntryData.Symptoms + " changed";
            newLogEntryData.Comments = newLogEntryData.Comments + " changed";
            newLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            newLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(newLogEntryData, "Team", "Edit");

            Log.Info("5. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("6. Open above log entry");
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that the changes are not saved");            
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Team"), "The change is saved");

            Log.Info("7. Make the changes");
            originalLogEntryData.Symptoms = originalLogEntryData.Symptoms + " changed";
            originalLogEntryData.Comments = originalLogEntryData.Comments + " changed";
            originalLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            originalLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(originalLogEntryData, "Team", "Edit");            

            Log.Info("8. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("9. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Team"), "The change is NOT saved");
            #endregion Team

            #region Resident
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Resident", out originalLogEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition            

            Log.Info("10. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("11. Make the changes");
            newLogEntryData.Symptoms = newLogEntryData.Symptoms + " changed";
            newLogEntryData.Comments = newLogEntryData.Comments + " changed";
            newLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            newLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(newLogEntryData, "Resident", "Edit");

            Log.Info("5. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("12. Open above log entry");
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that the changes are not saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Resident"), "The change is saved");

            Log.Info("13. Make the changes");
            originalLogEntryData.Symptoms = originalLogEntryData.Symptoms + " changed";
            originalLogEntryData.Comments = originalLogEntryData.Comments + " changed";
            originalLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            originalLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(originalLogEntryData, "Resident", "Edit");

            Log.Info("14. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("15. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Resident"), "The change is NOT saved");
            #endregion Resident

            #region Client
            #region Pre-condition: Create a new log entry
            Log.Info("Go to New Log Entry -> Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);
            Log.Info("Submit a new log entry");
            LogEntryDetailPage.FillLogEntryInfoRandomly("Client", out originalLogEntryData);
            LogEntryDetailPage.SaveLogEntry();
            #endregion Pre-condition            

            Log.Info("16. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("17. Make the changes");
            newLogEntryData.Symptoms = newLogEntryData.Symptoms + " changed";
            newLogEntryData.Comments = newLogEntryData.Comments + " changed";
            newLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            newLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(newLogEntryData, "Client", "Edit");

            Log.Info("18. Cancel saving");
            LogEntryDetailPage.CancelLogEntry();

            Log.Info("6. Open above log entry");
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that the changes are not saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Client"), "The change is saved");

            Log.Info("19. Make the changes");
            originalLogEntryData.Symptoms = originalLogEntryData.Symptoms + " changed";
            originalLogEntryData.Comments = originalLogEntryData.Comments + " changed";
            originalLogEntryData.CurrentTestStatus = Constants.TestStatus[rd.Next(0, Constants.TestStatus.Count - 1)];
            originalLogEntryData.CurrentDisposition = Constants.Disposition[rd.Next(0, Constants.Disposition.Count - 1)];
            LogEntryDetailPage.FillLogEntryInfo(originalLogEntryData, "Client", "Edit");

            Log.Info("20. Save the changes");
            LogEntryDetailPage.SaveLogEntry();

            Log.Info("21. Click on ID to open a log entry created above");
            System.Console.WriteLine(originalLogEntryData.MRN);
            HomePage.OpenALogEntry(originalLogEntryData.MRN);

            Log.Info("Verify that change is saved");
            Assert.IsTrue(LogEntryDetailPage.DoesDataOnEditPageDisplayCorrectly(originalLogEntryData, "Client"), "The change is NOT saved");
            #endregion Client

            Log.Info("Clean up: Delete log entries created");
            LogEntryDetailPage.SelectMenuItem(Constants.DashboardPath);
            HomePage.DeleteLogEntries(3, "top");
            #endregion
        }
    }
}
