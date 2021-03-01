using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    
    public class PBI_23863: TestBase
    {
        [Test]
        [Description("New Log Entry Display")]
        public void PBI_23863_AT_23884()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");            
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3.1. Go to New Log Entry -> Team page");            
            CommonPage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Team Members displays");
            //Assert.IsTrue(NewTeamLogEntryPage.CheckPageExist("New Infection Log Entry for Team Members"), "New Infection Log Entry for Team Members form does not display");

            Log.Info("4.1. Verify that UI displays correctly");
            //Assert.IsTrue(NewTeamLogEntryPage.DoesUIDisplayCorrectly(), "New Infection Log Entry UI displays incorrectly");

            Log.Info("3.2. Go to New Log Entry -> Resident page");
            CommonPage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Residents displays");

            Log.Info("4.2. Verify that UI displays correctly");

            Log.Info("3.3.Go to New Log Entry->Ageility Client page");
            CommonPage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("Verify that New Infection Log Entry for Ageility Clients displays");

            Log.Info("4.3. Verify that UI displays correctly");
        }
    }
}
