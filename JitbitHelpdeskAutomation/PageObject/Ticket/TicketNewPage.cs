using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using JitbitHelpdeskAutomation.PageObject.Common;

namespace JitbitHelpdeskAutomation.PageObject.Ticket
{
    public class TicketNewPage: CommonPage
    {
        // Properties
        public TextBox txtTicketSubject;
        public TextBox txtTicketDetail;
        public ComboBox cbCategory;
        public ComboBox cbPriority;
        public Link lnkAttachFile;
        public Button btnSubmit;

        //private readonly CommonPage commonPage;

        public TicketNewPage()
        {
            txtTicketSubject = new TextBox(By.XPath("//input[@id='Subject']"));
            txtTicketDetail = new TextBox(By.XPath("//body[@contenteditable='true']"));
            cbCategory = new ComboBox(By.Id("CategoryID"));
            cbPriority = new ComboBox(By.Id("PriorityID"));
            btnSubmit = new Button(By.XPath("//input[@id='btnAdd']"));
            lnkAttachFile = new Link(By.XPath("//label[@class='linkBrowse']"));

            //commonPage = new CommonPage();
        }

        #region Actions
        /// <summary>
        /// Fill ticket information
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="detail"></param>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        public void FillTicketInfo(string subject=null, string detail=null, string category=null, string priority=null)
        {
            // Fill Subject
            if (!string.IsNullOrEmpty(subject))
            {
                txtTicketSubject.SendKeys(subject);
            }
            
            // Fill Description
            if (!string.IsNullOrEmpty(detail))
            {
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
                System.Windows.Forms.SendKeys.SendWait(detail);                
            }

            // Select Category
            if(!string.IsNullOrEmpty(category))
            {
                cbCategory.SelectByText(category);
            }

            // Select Priority
            if (!string.IsNullOrEmpty(priority))
            {
                cbPriority.SelectByText(priority);
            }
        }

        /// <summary>
        /// Click on Submit button to submit the ticket
        /// </summary>
        public void SubmitATicket()
        {
            btnSubmit.Click();
            DriverUtils.WaitForPageLoad();
        }

        /// <summary>
        /// Create a new ticket without returning the Ticket ID
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="detail"></param>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        public void CreateANewTicket(string subject = null, string detail = null, string category = null, string priority = null)
        {
            // Click on New ticket link
            ClickALink("New ticket");

            // Fill Ticket Info
            FillTicketInfo(subject, detail, category, priority);

            // Click on Submit button
            SubmitATicket();
            
        }

        /// <summary>
        /// Create a new ticket and return the Ticket ID
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="detail"></param>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        public void CreateANewTicket(out string ticketID, string subject = null, string detail = null, string category = null, string priority = null)
        {
            // Fill Ticket Info
            FillTicketInfo(subject, detail, category, priority);

            // Click on Submit button to submit the ticket
            SubmitATicket();

            // Return TicketID
            ticketID = new TicketDetailsPage().GetTicketID();

        }
        #endregion

        #region Checkpoints
        #endregion
    }
}
