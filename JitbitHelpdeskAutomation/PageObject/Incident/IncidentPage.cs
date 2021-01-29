using JitbitHelpdeskAutomation.PageObject.Common;
using JitbitHelpdeskAutomation.Utilities;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JitbitHelpdeskAutomation.PageObject.Incident
{
    public class IncidentPage : CommonPage
    {
        public TextBox txtCaller;
        public TextBox txtServiceApplication;
        public ComboBox cbImpact;
        public ComboBox cbIssueType;
        public TextBox txtShortDescription;
        public TextBox txtDescription;
        public TextBox txtIncidentNumber;
        public ComboBox cbState;
        public TextBox txtAssignmentGroup;
        public ComboBox cbTranferTicketTo;
        public Button btnSubmit;
        public Button btnTranferToStarSupport;
        public Link lnkAll;
        public ComboBox cbSearchOption;
        public TextBox txtSearchValue;
        public Link lnkIncident;
        public ComboBox cbCloseCode;

        //private readonly CommonPage commonPage = new CommonPage();
        public IncidentPage()
        {
            txtCaller = new TextBox(By.Id("sys_display.incident.caller_id"));
            txtServiceApplication = new TextBox(By.Id("sys_display.incident.u_service_application"));
            cbImpact = new ComboBox(By.Id("incident.impact"));
            cbIssueType = new ComboBox(By.Id("incident.urgency"));
            txtShortDescription = new TextBox(By.Id("incident.short_description"));
            txtDescription = new TextBox(By.Id("incident.description"));
            txtIncidentNumber = new TextBox(By.Id("sys_readonly.incident.number"));
            cbState = new ComboBox(By.XPath("//select[@id = 'incident.state']"));
            txtAssignmentGroup = new TextBox(By.Id("sys_display.incident.assignment_group"));
            cbTranferTicketTo = new ComboBox(By.Id("incident.u_transfer_ticket_to"));
            btnSubmit = new Button(By.Id("sysverb_insert"));
            btnTranferToStarSupport = new Button(By.Id("transfer_ticket"));
            lnkAll = new Link(By.XPath("//a[b[text()='All']]"));
            cbSearchOption = new ComboBox(By.XPath("//select[contains(@id,'select')]"));
            txtSearchValue = new TextBox(By.XPath("//input[contains(@id,'text')]"));
            lnkIncident = new Link("//table[@id='incident_table']//tbody//tr[count(//table[@id='incident_table']//tbody//tr)][//td[text()='{0}']]//td[3]//a");
            cbCloseCode = new ComboBox(By.XPath("//select[contains(@id, 'incident.close_code')]"));
        }

        public void FillIncidentInformation(string caller = null, string serviceApplication = null, string impact = null, string issueType = null, string shortDescription = null, string description = null, string assignmentGroup = null)
        {
            SwitchToMainIFrame();
            txtCaller.WaitForVisible();
            if (caller != null)
            {
                txtCaller.SendKeys(caller);
                System.Windows.Forms.SendKeys.SendWait("{Tab}");
                DriverUtils.wait();
            }

            if (serviceApplication != null)
            {
                txtServiceApplication.SendKeys(serviceApplication);
                System.Windows.Forms.SendKeys.SendWait("{Tab}");
            }

            if (impact != null)
                cbImpact.SelectByText(impact);

            if (issueType != null)
                cbIssueType.SelectByText(issueType);

            if (shortDescription != null)
                txtShortDescription.SendKeys(shortDescription);

            if (txtDescription != null)
                txtDescription.SendKeys(description);

            if (assignmentGroup != null)
            {
                txtAssignmentGroup.SendKeys(assignmentGroup);
                System.Windows.Forms.SendKeys.SendWait("{Tab}");
                DriverUtils.wait();
            }
        }

        public void SubmitNewIncident()
        {
            btnSubmit.ClickByJs();
            btnSubmit.WaitForInvisible();
            DriverUtils.WaitForPageLoad();
        }

        public void TranferAnIncidentToStarSupport(string catogory = "HR - General")
        {
            cbTranferTicketTo.WaitForVisible();
            cbTranferTicketTo.SelectByText(catogory);
            btnTranferToStarSupport.ClickByJs();
            btnTranferToStarSupport.WaitForInvisible();
            DriverUtils.WaitForPageLoad();
        }

        public void SearchForAnIncident(string criteria = null, string value = null, bool searchAll = false)
        {
            SwitchToMainIFrame();
            cbSearchOption.WaitForVisible();
            if (searchAll)
                lnkAll.Click();
            cbSearchOption.SelectByText(criteria);
            txtSearchValue.SendKeys(value);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            DriverUtils.wait();
        }

        public void OpenAnIncident(string value)
        {
            lnkIncident.Format(value);
            lnkIncident.WaitForVisible();
            lnkIncident.Click();
            DriverUtils.WaitForPageLoad();
        }

        public string GetIncidentNumber()
        {
            return txtIncidentNumber.GetValue();
        }

        public bool IsTransferToStarSupportButtonDisplayed()
        {
            return btnTranferToStarSupport.IsDisplayed();
        }

        public bool IsTransferTicketToDropdownWithBlankOptionDisplayed()
        {
            return cbTranferTicketTo.IsDisplayed() && cbTranferTicketTo.GetSelectedOption().Equals("-- None --");
        }

        public bool AreStarSupportCategoriesDisplayedCorrectly()
        {
            List<string> categories = new List<string>() { "-- None --", "Casamba - General", "Finance - General", "Finance - Account Payable", "HR - General", "HR - Absence", "HR - Benefits", "HR - New Hire", "HR - Payroll", "HR- HCM", "HR - Advanced HRIS", "Purchasing - General", "Purchasing - Approval", "Purchasing - Invoice", "Purchasing - Item", "Purchasing - Receipt", "Purchasing - Requisition", "Purchasing - Vendor", "PCC - General", "PCC - Clinical", "PCC - Financial" };
            return categories.SequenceEqual(cbTranferTicketTo.GetSelectOptions());
        }

        public bool IsIncidentClosedWithCorrectCloseCode()
        {
            cbCloseCode.WaitForVisible();
            cbState.WaitForVisible();
            return cbState.GetSelectedOption().Equals("Closed") && cbCloseCode.GetSelectedOption().Equals("Transferred to Star Support");
        }

        public bool IsOriginalIncidentOpened(string incidentNumber)
        {
            return GetIncidentNumber().Equals(incidentNumber);
        }
    }
}
