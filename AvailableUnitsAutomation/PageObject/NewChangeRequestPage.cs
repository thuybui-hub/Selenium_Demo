using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using SeleniumCSharp.Core.Utilities;
using AvailableUnitsAutomation.PageObject;
using AvailableUnitsAutomation.DataObject;
using System.Linq;

namespace InfectionLogAutomation.PageObject
{
    public class NewChangeRequestPage: CommonPage
    {
        #region Properties
        public Span spnPageTitle;
        public Span spnRequestState;
        public Label lblTemporaryRequest;
        public Span spnEffectiveDate;        
        public Span spnCommunityName;        
        public Span spnBU;
        public Span spnRegion;
        public Span spnDivision;
        public Span spnExecutiveDirector;
        public Span spnRDO;
        public Span spnDVP;
        public Span spnRDSM;
        public Span spnServiceLines;
        public Button btnSave;
        public Button btnDeleteRequest;
        public Span spnSubmittedBy;
        public TextBox txtPhone;
        public string addNewRecord = "Add new record";
        public string backToDashboardPage = "Request Dashboard";

        // Change Details section        
        public TextBox txtUnitNumber;
        public Span spnServiceLine;        
        public TextBox txtMonthlySellingRate;
        public Span spnChangeType;
        public TextBox txtBusinessReason;
        public string updateNewRecordChangeDetails = "Update";
        public string cancelNewRecordChangeDetails = "Cancel";

        // Change Summary section

        // Reason for Request

        public NewChangeRequestPage()
        {
            spnPageTitle = new Span(By.XPath("//div[@id=\"communityInfoHeader\"]/span"));
            spnRequestState = new Span(By.XPath("//label[contains(text(), \"Request State\")]//following-sibling::span"));
            lblTemporaryRequest = new Label(By.XPath("//label[@class=\"k-checkbox-label\"]"));
            spnEffectiveDate = new Span(By.XPath("//span[@aria-owns=\"requestDate_listbox\"]/span"));            
            spnCommunityName = new Span(By.XPath("//span[@aria-owns=\"communityName_listbox\"]/span"));
            spnBU = new Span(By.XPath("//label[contains(text(), \"Business Unit\")]//following-sibling::span"));
            spnRegion = new Span(By.XPath("//label[contains(text(), \"Region\")]//following-sibling::span"));
            spnDivision = new Span(By.XPath("//label[contains(text(), \"Division\")]//following-sibling::span"));
            spnExecutiveDirector = new Span(By.XPath("//label[contains(text(), \"Executive Director\")]//following-sibling::span"));
            spnRDO = new Span(By.XPath("//label[contains(text(), \"RDO\")]//following-sibling::span"));
            spnDVP = new Span(By.XPath("//label[contains(text(), \"DVP\")]//following-sibling::span"));
            spnRDSM = new Span(By.XPath("//label[contains(text(), \"RDSM\")]//following-sibling::span"));
            spnServiceLines = new Span(By.XPath("//label[contains(text(), \"Service Line\")]//following-sibling::span"));
            btnSave = new Button(By.XPath("//div[@class=\"form-group col-md-6\"]/button[contains(text(), \"Save\")]"));
            btnDeleteRequest = new Button(By.XPath("//div[@class=\"form-group col-md-6\"]/button[contains(text(), \"Delete Request\")]"));
            spnSubmittedBy = new Span(By.XPath("//label[contains(text(), \"Submitted By\")]//following-sibling::span"));
            txtPhone = new TextBox(By.Id("phone"));

            // Change Details section
            txtUnitNumber = new TextBox(By.Name("UnitNumber"));
            spnServiceLine = new Span(By.XPath("//td[@data-container-for=\"ServiceLine\"]//span[@class=\"k-input\"]"));
            txtMonthlySellingRate = new TextBox(By.Name("MonthlySellingRate"));
            spnChangeType = new Span(By.XPath("//td[@data-container-for=\"ChangeType\"]//span[@class=\"k-input\"]"));
            txtBusinessReason = new TextBox(By.Name("BusinessReason"));

            // Change Summary section

            // Reason for Request section
        }
        #endregion Properties

        #region Actions
        public void BackToDashboardPage()
        {
            GoToPage(backToDashboardPage);
            DriverUtils.WaitForPageLoad();
        }
        public void OpenChangeDetails()
        {
            GoToPage(addNewRecord);
            DriverUtils.WaitForPageLoad();
        }

        public void EnterChangeDetailsInfo(string unitNumber, string serviceLine, string rate, string type, string reason)
        {
            if (!string.IsNullOrEmpty(unitNumber))
            {
                txtUnitNumber.SendKeys(unitNumber);
            }

            if (!string.IsNullOrEmpty(serviceLine))
            {
                spnServiceLine.Click();
                System.Windows.Forms.SendKeys.SendWait(serviceLine);
            }

            if (!string.IsNullOrEmpty(rate))
            {
                txtMonthlySellingRate.SendKeys(rate);
            }

            if (!string.IsNullOrEmpty(type))
            {
                spnChangeType.Click();
                System.Windows.Forms.SendKeys.SendWait(type);
            }

            if (!string.IsNullOrEmpty(reason))
            {
                txtBusinessReason.SendKeys(reason);
            }
        }

        public void FillChangeDetailsInfo(UnitData unitData)
        {
            if (!string.IsNullOrEmpty(unitData.UnitNumber))
            {
                txtUnitNumber.SendKeys(unitData.UnitNumber);
            }

            if (!string.IsNullOrEmpty(unitData.ServiceLine))
            {
                spnServiceLine.Click();
                System.Windows.Forms.SendKeys.SendWait(unitData.ServiceLine);
            }

            if (!string.IsNullOrEmpty(unitData.MonthlySellingRate))
            {
                txtMonthlySellingRate.SendKeys(unitData.MonthlySellingRate);
            }

            if (!string.IsNullOrEmpty(unitData.ChangeType))
            {
                spnChangeType.Click();
                System.Windows.Forms.SendKeys.SendWait(unitData.ChangeType);
            }

            if (!string.IsNullOrEmpty(unitData.BusinessReason))
            {
                txtBusinessReason.SendKeys(unitData.BusinessReason);
            }
        }

        public void SaveChangeDetailsInfo()
        {
            GoToPage(updateNewRecordChangeDetails);
            DriverUtils.WaitForPageLoad();
        }

        public void CancelChangeDetailsInfo()
        {
            GoToPage(cancelNewRecordChangeDetails);
            DriverUtils.WaitForPageLoad();
        }

        public void SaveNewRequest(out string requestID)
        {
            btnSave.ScrollToView();
            btnSave.Click();
            //DriverUtils.WaitForPageLoad();
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            requestID = GetRequestId();
        }

        public string GetRequestId()
        {
            string requestURL = DriverUtils.GetDriverProperties().GetRemoteUrl();
            //string requestURL = Browser.GetCurrentUrl();
            int num = requestURL.Count(x => x == '=');
            string shortRequestURL;
            if (num == 2)
            {
                shortRequestURL = requestURL.Remove(requestURL.IndexOf("="), 1);
            }
            else
            {
                shortRequestURL = requestURL;
            }
            string requestID = shortRequestURL.Substring(shortRequestURL.IndexOf("=") + 1, shortRequestURL.Length - shortRequestURL.IndexOf("=") - 1);
            return requestID;
        }
        #endregion Actions

        #region Check points
        #endregion Check points
    }
}
