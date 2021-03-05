using InfectionLogAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23982 : TestBase
    {
        #region Test data
        #endregion Test data

        [Test]
        [Description("IO Log - Implement Default/Read-only COVID-19 Infection type")]
        public void PBI_23982_AT_23991()
        {
            Log.Info("1. Navigate to Infectious Outbreak application http://qa.ilog.fve.ad.5ssl.com/");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Log in with valid account (e.g. gnguyen)");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to New Log Entry -> Team");
            HomePage.SelectMenuItem(Constants.NewTeamLogEntryPath);

            Log.Info("Verify that Infection Type is default to Covid-19");
            Assert.IsTrue(LogEntryDetailPage.spnInfectionType.GetText().Equals("COVID-19"), "Infection Type is not defaulted to Covid-19");

            Log.Info("Verify that Users can only select infection type COVID-19");
            Assert.IsTrue(LogEntryDetailPage.DoesInfectionTypeDisplayCorrectly("Team"), "Infection Type field displays incorrectly.");

            Log.Info("4. Go to New Log Entry -> Ageility Client");
            HomePage.SelectMenuItem(Constants.NewClientLogEntryPath);

            Log.Info("Verify that Infection Type is default to Covid-19");
            Assert.IsTrue(LogEntryDetailPage.spnInfectionType.GetText().Equals("COVID-19"), "Infection Type is not defaulted to Covid-19");

            Log.Info("Verify that Users can only select infection type COVID-19");
            Assert.IsTrue(LogEntryDetailPage.DoesInfectionTypeDisplayCorrectly("Client"), "Infection Type field displays incorrectly.");

            Log.Info("5. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("Verify that Infection Type is default to Covid-19");
            Assert.IsTrue(LogEntryDetailPage.spnInfectionType.GetText().Equals("COVID-19"), "Infection Type is not defaulted to Covid-19");

            Log.Info("Verify that Infection Type is default to Covid-19 and user is able to select another values");
            Assert.IsTrue(LogEntryDetailPage.DoesInfectionTypeDisplayCorrectly("Resident"), "Infection Type field displays incorrectly.");

        }
    }
}
