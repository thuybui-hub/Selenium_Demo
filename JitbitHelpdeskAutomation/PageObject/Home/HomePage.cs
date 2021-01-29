using JitbitHelpdeskAutomation.PageObject.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;

namespace JitbitHelpdeskAutomation.PageObject.Home
{
    public class HomePage : CommonPage
    {
        private readonly Button btnNewTicket;
        private readonly TextBox txtSearch;
        private readonly Link lnkTicket;
        private readonly Link lstTicket;
        private readonly Label lblCategory;
        private readonly Link lnkIncident;
        //private OpenQA.Selenium.IWebElement IwebLnkIncident;
        private readonly CommonPage commonPage = new CommonPage();
        public HomePage()
        {
            btnNewTicket = new Button(By.Id("newTicket"));
            txtSearch = new TextBox(By.XPath("//div[@class='divSearch']//input"));
            lnkTicket = new Link("//table[@id='tblTickets']//a[@class='ticketLink' and text()='{0}']");
            lstTicket = new Link(By.XPath("//table[@id='tblTickets']//a[@class='ticketLink']"));
            lblCategory = new Label("//table[@id='tblTickets']//a[@class='ticketLink' and text()='{0}']//following-sibling::div//span[@class='categoryName']");
            lnkIncident = new Link("//table[@id='tblTickets']//a[@class='ticketLink' and text()='{0}']//following-sibling::div//span[@class='categoryName']//a[text()='{1}']");

        }

        public void OpenATicket(string ticketName)
        {
            lnkTicket.Format(ticketName);
            lnkTicket.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void OpenAnOriginalIncident(string ticketName, string incidentNumber)
        {
            lnkIncident.Format(ticketName, incidentNumber);
            lnkIncident.ClickByJs();
            DriverUtils.wait();
            DriverUtils.SwitchToNewWindow();
            DriverUtils.Maximize();
            commonPage.SwitchToMainIFrame();
            DriverUtils.WaitForPageLoad();
        }

        public int GetTicketNumber()
        {
            return lstTicket.GetElements().Count;
        }

        public bool IsTicketCreatedWithSelectedCategory(string ticketName, string category)
        {
            lnkTicket.Format(ticketName);
            lblCategory.Format(ticketName);
            return lnkTicket.IsDisplayed() && lblCategory.GetText().Contains(category);
        }

        public bool IsOriginalIncidentNumberDisplayedUnderStarSupportTicket(string ticketName, string incidentNumber)
        {
            lnkIncident.Format(ticketName);
            return lnkIncident.GetText().Equals(incidentNumber);
        }
    }
}
