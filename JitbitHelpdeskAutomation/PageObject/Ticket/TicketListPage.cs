using System;
using OpenQA.Selenium;
using SeleniumCSharp.Core.ElementWrapper;
using SeleniumCSharp.Core.Helpers;
using JitbitHelpdeskAutomation.PageObject.Common;

namespace JitbitHelpdeskAutomation.PageObject.Ticket
{
    public class TicketListPage: CommonPage
    {
        // Properties
        public Table tblTickets;
        public Tr trTicket;

        public TicketListPage()
        {
            tblTickets = new Table(By.XPath("//table[@id='tblTickets']"));            
            trTicket = new Tr(By.XPath("//table[@id='tblTickets']//tr[contains(@class, 'ticketRow')]"));
        }

        #region Actions       

        public int GetTicketsRows()
        {
            return trTicket.GetElements().Count;
        }
        #endregion

        #region Checkpoints
        #endregion

    }
}
