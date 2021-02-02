using JitbitHelpdeskAutomation.DataObject;
using JitbitHelpdeskAutomation.PageObject.Common;
using JitbitHelpdeskAutomation.PageObject.Home;
using JitbitHelpdeskAutomation.PageObject.Incident;
using JitbitHelpdeskAutomation.PageObject.Login;
using JitbitHelpdeskAutomation.PageObject.Ticket;
using JitbitHelpdeskAutomation.Utilities;
using NUnit.Framework;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.Utilities;

namespace JitbitHelpdeskAutomation.UITests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_20997: TestBase
    {
        private readonly LoginData loginData = JsonParser.Get<LoginData>();
        private readonly SubmiterData submiterData = JsonParser.Get<SubmiterData>();     
        private readonly NewIncidentData newIncidentData = JsonParser.Get<NewIncidentData>();     
        private string incidentName, incidentNumber , category = "HR - General";

        [Test]
        [Category("Regression")]
        [Category("Function")]
        [Description("Integration with Service-Now")]
        public void PBI_20997_AT_21192()
        {
            Log.Info("1. Go to Service Now site");
            DriverUtils.GoToUrl(Constants.SNUrl);

            Log.Info("2. Login with valid user");
            LoginPage.LoginToServiceNow(loginData.ServiceNowUser, loginData.ValidPassword);

            Log.Info("3. Impersonate to ITIL user");
            CommonPage.ImpersionateUser(loginData.ITILUser);

            Log.Info("4. Open new incident page");
            CommonPage.OpenNewIncidentPage();

            Log.Info("5. Create new incident");
            incidentName = Utils.GetRandomValue(newIncidentData.ShortDescription);
            IncidentPage.FillIncidentInformation(newIncidentData.Caller, newIncidentData.ServiceApplication, newIncidentData.Impact, newIncidentData.IssueType, incidentName, newIncidentData.description, newIncidentData.AssignmentGroup);

            Log.Info("6. Submit a new incident");
            IncidentPage.SubmitNewIncident();

            Log.Info("7. Open list incident page");
            CommonPage.OpenIncidentListPage();

            Log.Info("8. Search for an incident");
            IncidentPage.SearchForAnIncident("Short description", incidentName, false);

            Log.Info("9. Open the incident");
            IncidentPage.OpenAnIncident(incidentName);
            incidentNumber = IncidentPage.GetIncidentNumber();

            Log.Info("VP: Verify that A 'Transfer to Star Support' button is available");
            Assert.IsTrue(IncidentPage.IsTransferToStarSupportButtonDisplayed(), "A 'Transfer to Star Support' button is available");

            Log.Info("VP: Verify that A 'Transfer ticket to' dropdown is visible with a blank option as default");
            Assert.IsTrue(IncidentPage.IsTransferTicketToDropdownWithBlankOptionDisplayed(), "A 'Transfer ticket to' dropdown is visible with a blank option as default");

            Log.Info("VP: Verify that A 'Transfer ticket to' dropdown is populated with the star support categories (PBI 20995)");
            Assert.IsTrue(IncidentPage.AreStarSupportCategoriesDisplayedCorrectly(), "A 'Transfer ticket to' dropdown is populated with the star support categories (PBI 20995)");

            Log.Info("10. Tranfer an incident to star support");
            IncidentPage.TranferAnIncidentToStarSupport(category);

            Log.Info("11. Open Closure Information tab");
            CommonPage.SelectServiceNowTab("Closure Information");

            Log.Info("VP: Verify that The original ticket in service now is closed with a closed code 'Transferred to Star Support'");
            Assert.IsTrue(IncidentPage.IsIncidentClosedWithCorrectCloseCode(), "The original ticket in service now is closed with a closed code 'Transferred to Star Support'");

            Log.Info("12. Go to Star Support site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("13. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("14. Open Ticket tab");
            CommonPage.SelectTabMenu("Tickets");

            Log.Info("VP: Verify that A new ticket is created in the Star Support system and assigned to selected category");
            Assert.IsTrue(HomePage.IsTicketCreatedWithSelectedCategory(incidentName, category), "A new ticket is created in the Star Support system and assigned to selected category");

            Log.Info("VP: Verify that There is ServiceNow Ticket Reference Number underneath ticket title");
            Assert.IsTrue(HomePage.IsTicketCreatedWithSelectedCategory(incidentName, category), "There is ServiceNow Ticket Reference Number underneath ticket title");

            Log.Info("15. Open original incident under ticket title");
            HomePage.OpenAnOriginalIncident(incidentName, incidentNumber);

            Log.Info("VP: Verify that the original SN ticket is opened");
            Assert.IsTrue(IncidentPage.IsOriginalIncidentOpened(incidentNumber), "The original SN ticket is opened");

            Log.Info("16. Go to Star Support site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("17. Open created ticket");
            HomePage.OpenATicket(incidentName);

            Log.Info("VP: Verify that the ticket in ServiceNow and Star Support system match each other");
            Assert.IsTrue(TicketDetailsPage.IsIncidentInfoPopulated(incidentName, newIncidentData.description), "The ticket in ServiceNow and Star Support system match each other");
            Assert.IsTrue(TicketDetailsPage.IsSubmiterInfoPopulated(submiterData), "The submiter info in ServiceNow and Star Support system match each other");

            Log.Info("Post Condition: Delete created ticket");
            TicketDetailsPage.DeleteATicket();

        }
    }
}
