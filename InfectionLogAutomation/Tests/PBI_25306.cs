using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_25306 : TestBase
    {
        [Test]
        [Description("IO Log: Bulk Insert Record Selection & Processing: Team")]
        public void PBI_25306_AT_25362()
        {
            #region Test data
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");

            Log.Info("2. Log in with account (e.g. gnguyen)");

            Log.Info("3. Go to Bulk Insert -> Team");

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Members' opens.");

            Log.Info("4. Fill Region and Community");

            Log.Info("5.Click on button 'Select Team Member' next to text box 'Team Member'");

            Log.Info("Verify that a pop-up (Select Team Member) appears.");

            Log.Info("6. Perfom a search on the pop-up");

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table");

            Log.Info("7. Sort data by a column on the pop-up");

            Log.Info("Verify that the data on the pop-up is sorted by the column correctly");

            Log.Info("8. Check one or more record on the pop-up");

            Log.Info("Verify that corresponding one or more records are checked");

            Log.Info("9. Uncheck the records");

            Log.Info("Verify that corresponding records are unchecked");

            Log.Info("10. Try to check/select all records");

            Log.Info("Verify that all records are checked");

            Log.Info("11. Filter records");

            Log.Info("Verify that the records are filtered correctly");

            Log.Info("12. Select some records on the pop-up");

            Log.Info("13. Click on 'Select' button on the pop-up");

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");

            Log.Info("14. Fill Testing Date and Comments fields");

            Log.Info("Click on button 'Insert' at the bottom");

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }

        [Test]
        [Description("IO Log: Bulk Insert Record Selection & Processing: Resident")]
        public void PBI_25306_AT_25363()
        {
            #region Test data
            #endregion Test data

            #region Pre-condition
            #endregion Pre-condition

            #region Main steps
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/ ");

            Log.Info("2. Log in with account (e.g. gnguyen)");

            Log.Info("3. Go to Bulk Insert -> Resident");

            Log.Info("Verify that the page 'Bulk Infection Log Entry for Team Residents' opens.");

            Log.Info("4. Fill Region and Community");

            Log.Info("5.Click on button 'Select Resident' next to text box 'Team Member'");

            Log.Info("Verify that a pop-up (Select Resident) appears.");

            Log.Info("6. Perfom a search on the pop-up");

            Log.Info("Verify that records matching partly Last Name/First Name show on the result table");

            Log.Info("7. Sort data by a column on the pop-up");

            Log.Info("Verify that the data on the pop-up is sorted by the column correctly");

            Log.Info("8. Check one or more record on the pop-up");

            Log.Info("Verify that corresponding one or more records are checked");

            Log.Info("9. Uncheck the records");

            Log.Info("Verify that corresponding records are unchecked");

            Log.Info("10. Try to check/select all records");

            Log.Info("Verify that all records are checked");

            Log.Info("11. Filter records");

            Log.Info("Verify that the records are filtered correctly");

            Log.Info("12. Select some records on the pop-up");

            Log.Info("13. Click on 'Select' button on the pop-up");

            Log.Info("Verify that: The pop-up is closed and All selected members/records redisplay in text box 'Team Member' on the page 'Bulk Infection Log Entry for Team Members'.");

            Log.Info("14. Fill Testing Date and Comments fields");

            Log.Info("Click on button 'Insert' at the bottom");

            Log.Info("Verify that all log entries for bulk insert at step #12 show on the Dashboard.");
            #endregion Main steps

            #region Clean up
            #endregion Clean up
        }
    }
}
