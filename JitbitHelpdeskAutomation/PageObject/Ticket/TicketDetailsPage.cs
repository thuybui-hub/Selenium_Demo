using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using JitbitHelpdeskAutomation.DataObject;
using JitbitHelpdeskAutomation.PageObject.Common;
using System.Threading;

namespace JitbitHelpdeskAutomation.PageObject.Ticket
{
    public class TicketDetailsPage: CommonPage
    {
        // Properties
        public Label lblTicketSubject;
        public Label lblTicketDetail;
        public Label lblTicketID;
        public Label lblTicketPriority;
        public Link lnkSubmitter;
        public Label lblLocation;
        public Label lblDepartment;
        public Label lblTitle;
        public Label lblTicketCategory;

        // User popup
        public Label lblUserSubmiter;
        public Label lblUserLocation;
        public Label lblUserDepartment;
        public Label lblUserJobTitle;

        // More... option
        public Button btnMore;
        public Link lnkDelete;
        public Link lnkPrint;
        public Link lnkCloneTicket;

        //private readonly CommonPage commonPage;

        public TicketDetailsPage()
        {
            lblTicketSubject = new Label(By.XPath("//div/h2[@id='subject']"));
            lblTicketDetail = new Label(By.XPath("//div/div[@id='body']"));
            lblTicketID = new Label(By.Id("h2TicketId"));
            lblTicketPriority = new Label(By.XPath("//div[@id='lblPriority']"));
            lnkSubmitter = new Link(By.XPath("//div/a[@id='lnkFrom']"));
            lblLocation = new Label(By.XPath("//div[@class='rightsidebar']//td[span[text()='Location']]//following-sibling::td/div/span"));
            lblDepartment = new Label(By.XPath("//div[@class='rightsidebar']//td[span[contains(text(),'Department')]]//following-sibling::td/div/span"));
            lblTitle = new Label(By.XPath("//div[@class='rightsidebar']//td[span[contains(text(),'Title')]]//following-sibling::td/div/span"));
            lblTicketCategory =  new Label(By.XPath("//div[@class='rightsidebar']//td[span[contains(text(),'Category')]]//following-sibling::td/div/div"));

            // User popup
            lblUserSubmiter = new Label(By.XPath("//div[@id='userPopup']/h3"));
            lblUserLocation = new Label(By.XPath("//ul[@class='userInfo grey']/li[1]/b"));
            lblUserDepartment = new Label(By.XPath("//ul[@class='userInfo grey']/li[2]/b"));
            lblUserJobTitle = new Label(By.XPath("//ul[@class='userInfo grey']/li[3]/b"));

            // More... option
            btnMore = new Button(By.XPath("//button[@id='toolsbtn']"));
            lnkDelete = new Link(By.XPath("//a[text() = 'Delete']"));            
            lnkPrint = new Link(By.XPath("//a[text() = 'Print...']"));
            lnkCloneTicket = new Link(By.XPath("//a[text() = 'Clone ticket']"));

            //commonPage = new CommonPage();
        }

        #region Actions

        /// <summary>
        /// Get TicketID after creating
        /// </summary>
        /// <returns></returns>
        public string GetTicketID()
        {
            return lblTicketID.GetText().Replace("#", "");
        }


        /// <summary>
        /// Get ticket infor on the Ticket Details page
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="ticketSubject"></param>
        /// <param name="ticketDetails"></param>
        /// <param name="ticketCategory"></param>
        /// <param name="ticketPriority"></param>
        /// <param name="ticketSubmitter"></param>
        public void GetTicketInfo(out string ticketID, out string ticketSubject, out string ticketDetails, out string ticketCategory, out string ticketPriority, out string ticketSubmitter)
        {
            ticketID = GetTicketID();
            ticketSubject = lblTicketSubject.GetText();
            ticketDetails = lblTicketDetail.GetText();
            ticketCategory = lblTicketCategory.GetText();
            ticketPriority = lblTicketPriority.GetText();
            ticketSubmitter = lnkSubmitter.GetText();
        }

        /// <summary>
        /// Open the submiter pop-up info on Tickets Detail page
        /// </summary>
        public void OpenSubmiterPopup()
        {
            string text = lnkSubmitter.GetText();
            lnkSubmitter.ClickAndHold();
            DriverUtils.WaitForPageLoad();
        }

        public void OpenUserDetailsPage()
        {
            lblUserSubmiter.ClickAndHold();
            DriverUtils.WaitForPageLoad();
        }

        /// <summary>
        /// Select an option: Delete, Print, Clone ticket under More...
        /// </summary>
        /// <param name="optionName"></param>
        /// <example Delete, Print, Clone ticket/>
        public void SelectAnOptionUnderMore(string optionName)
        {
            btnMore.ClickAndHold();

            switch (optionName)
                {
                    case "Delete":
                    lnkDelete.ClickByJs();
                    break;

                    case "Print":
                    lnkPrint.ClickByJs();
                    break;

                    case "Clone ticket":
                    lnkCloneTicket.ClickByJs();
                    break;
                }

            Thread.Sleep(100);
        }

        /// <summary>
        /// Delete a ticket on the Ticket Details page
        /// </summary>
        /// <param name="submiter"></param>
        /// <returns></returns>
        public void DeleteATicket()
        {
            SelectAnOptionUnderMore("Delete");            
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            DriverUtils.WaitForPageLoad();
        }
        #endregion

        #region Checkpoints
        public bool IsIncidentInfoPopulated(string incidentName, string incidentDesc)
        {
            return lblTicketSubject.GetText().Equals(incidentName)
                && lblTicketDetail.GetText().Equals(incidentDesc);
        }

        public bool IsSubmiterInfoPopulated(SubmiterData submiter)
        {
            return lnkSubmitter.GetText().Equals(submiter.FullName)
                && lblLocation.GetText().Contains(submiter.Location)
                && lblDepartment.GetText().Equals(submiter.Department)
                && lblTitle.GetText().Equals(submiter.JobTitle);
        }
        #endregion
    }
}
