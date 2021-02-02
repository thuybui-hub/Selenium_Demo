using JitbitHelpdeskAutomation.PageObject.Common;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using JitbitHelpdeskAutomation.PageObject.Login;
using JitbitHelpdeskAutomation.DataObject;
using JitbitHelpdeskAutomation.PageObject.Ticket;
using JitbitHelpdeskAutomation.PageObject.UserDetails;
using JitbitHelpdeskAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;

namespace JitbitHelpdeskAutomation.UITests
{    
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    
    public class PBI_21010: TestBase
    {
        private readonly LoginData loginData = JsonParser.Get<LoginData>();
        private readonly SubmiterData submiterData = JsonParser.Get <SubmiterData>();
        string subject = Utils.GetRandomValue("subject");
        string detail = Utils.GetRandomValue("detail");
        string category = "HR - General";
        string priority = "Low";
        string strTicketID;

        [Test]       
        [Description("Enhancements to the Tickets Dashboard")]
        public void PBI_21010_AT_21175()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("3. Create new a ticket");
            TicketNewPage.CreateANewTicket(subject, detail, category, priority);            

            Log.Info("4. Get TicketID");
            strTicketID = TicketDetailsPage.GetTicketID();
            
            Log.Info("5. Click on submiter link to open submiter popup");
            TicketDetailsPage.OpenSubmiterPopup();

            Log.Info("6. Click on submiter link on the popup to open User Details page");
            TicketDetailsPage.OpenUserDetailsPage();

            Log.Info("Verify that User details page for the selected submitter displays with correct information");
            Assert.IsTrue(UserDetailsPage.IsSubmiterInfoCorrect(submiterData), "Submiter information shows incorrectly");

            Log.Info("Clean up");
            Log.Info("Delete the ticket created");
            CommonPage.NavigateToTicketDetailsPage(strTicketID);
            TicketDetailsPage.DeleteATicket();
        }
        
    }
}