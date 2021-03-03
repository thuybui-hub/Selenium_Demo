using InfectionLogAutomation.PageObject.Common;
using InfectionLogAutomation.Utilities;
using Microsoft.VisualStudio.TestTools.UITesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.PageObject.LogEntry
{
    public class LogEntryDetailPage : CommonPage
    {
        public readonly Label lblTitle;
        public readonly TextBox txtRegion;
        public readonly ComboBox cbbRegion;
        public readonly TextBox txtCommunity;
        public readonly ComboBox cbbCommunity;
        public readonly TextBox txtEmployee;
        public readonly ComboBox cbbEmployee;
        public readonly TextBox txtLastName;
        public readonly TextBox txtFirstName;
        public readonly TextBox txtMrn;
        public readonly Button btnAdvancedSearch;
        public readonly Span spnInfectionType;
        public readonly ComboBox cbbInfectionType;
        public readonly TextBox txtOnsetDate;
        public readonly TextArea txtSymptoms;
        public readonly Span spnTestingStatus;
        public readonly ComboBox cbbTestingStatus;
        public readonly TextBox txtTestingStatusDate;
        public readonly Span spnDisposition;
        public readonly ComboBox cbbDisposition;
        public readonly TextBox txtDispositionDate;
        public readonly TextArea txtComments;
        public readonly Button btnSaveLogEntry;
        public readonly Button btnCancelLogEntry;

        public readonly Ul lstBoxRegion;
        public readonly Ul lstBoxCommunity;
        public readonly Ul lstBoxTestingStatus;
        public readonly Ul lstBoxDisposition;

        #region Actions
        public LogEntryDetailPage()
        {
            lblTitle = new Label(By.XPath("//div[@id=\"logForm\"]//h3"));
            txtRegion = new TextBox(By.XPath("//ul[@id=\"region_taglist\"]//following-sibling::input"));
            cbbRegion = new ComboBox(By.XPath("//select[@Id=\"region\"]"));
            txtCommunity = new TextBox(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input"));
            cbbCommunity = new ComboBox(By.Id("communityName"));
            txtEmployee = new TextBox(By.XPath("//ul[@id=\"person_taglist\"]//following-sibling::input"));
            cbbEmployee = new ComboBox(By.Id("person"));
            txtLastName = new TextBox(By.Id("lastName"));
            txtFirstName = new TextBox(By.Id("firstName"));
            txtMrn = new TextBox(By.Id("mrn"));
            btnAdvancedSearch = new Button(By.Id("advanceSearch"));
            spnInfectionType = new Span(By.XPath("//label[@id=\"infectionType_label\"]//following-sibling::span"));
            cbbInfectionType = new ComboBox(By.Id("infectionType"));
            txtOnsetDate = new TextBox(By.Id("onsetDate"));
            txtSymptoms = new TextArea(By.XPath("//textarea[@id=\"symptoms\"]//preceding-sibling::iframe"));
            spnTestingStatus = new Span(By.XPath("//label[@id=\"testingStatus_label\"]//following-sibling::span"));
            cbbTestingStatus = new ComboBox(By.Id("testingStatus"));
            txtTestingStatusDate = new TextBox(By.Id("testingStatusDate"));
            spnDisposition = new Span(By.XPath("//label[@id=\"disposition_label\"]//following-sibling::span"));
            cbbDisposition = new ComboBox(By.Id("disposition"));
            txtDispositionDate = new TextBox(By.Id("dispositionDate"));
            txtComments = new TextArea(By.XPath("//textarea[@id=\"comments\"]//preceding-sibling::iframe"));
            btnSaveLogEntry = new Button(By.XPath("//button[@class=\"k-button btnSave\"]"));
            btnCancelLogEntry = new Button(By.XPath("//button[@class=\"k-button btnCancel\"]"));

            lstBoxRegion = new Ul(By.XPath("//ul[@id=\"region_listbox\"]//li"));
            lstBoxCommunity = new Ul(By.XPath("//ul[@id=\"communityName_listbox\"]//li"));
            lstBoxTestingStatus = new Ul(By.XPath("//ul[@id=\"testingStatus_listbox\"]//li"));
            lstBoxDisposition = new Ul(By.XPath("//ul[@id=\"disposition_listbox\"]//li"));
        }

        #endregion Actions
        /// <summary>
        /// Get all items in a list of a control that drop down a list
        /// </summary>
        /// <param name="field">HtmlCustom field</param>
        /// <returns></returns>
        public List<string> GetItemsFromControlList(Fields field)
        {
            DriverUtils.WaitForPageLoad();

            Ul element;

            switch (field)
            {
                case Fields.region:
                    element = lstBoxRegion;
                    txtRegion.Click();
                    break;

                case Fields.community:
                    element = lstBoxCommunity;
                    txtEmployee.Click();
                    break;
                case Fields.testStatus:
                    element = lstBoxTestingStatus;
                    spnTestingStatus.Click();
                    break;
                case Fields.disposition:
                    element = lstBoxDisposition;
                    spnDisposition.ScrollToView();
                    spnDisposition.Click();
                    break;
                
                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }
            List<string> displayedList = new List<string> { };
            List<IWebElement> listElements = element.GetElements();

            foreach (IWebElement item in listElements)
            {
                displayedList.Add(item.Text);
            }

            switch (field)
            {
                case Fields.region:
                    txtRegion.Click();
                    break;
                case Fields.community:
                    txtCommunity.Click();
                    break;
                case Fields.testStatus:                    
                    spnTestingStatus.Click();
                    break;
                case Fields.disposition:                    
                    spnDisposition.Click();
                    break;

                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }

            return displayedList;
        }
        #region Check points
        /// <summary>
        /// Check to see if a form displays via form's title
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public bool CheckPageExist(string pageTitle)
        {
            DriverUtils.WaitForPageLoad(3);
            string title = lblTitle.GetText();
            return title.Contains(pageTitle);
        }

        /// <summary>
        /// Check to see if UI of New Team Log entry display correctly
        /// </summary>
        /// typeofLogEntry can be: Team, Resident, Client
        /// <returns></returns>
        public bool DoesUIDisplayCorrectly(string typeofLogEntry)
        {
            bool result = txtRegion.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && spnInfectionType.IsDisplayed()
                && txtOnsetDate.IsDisplayed()
                && txtSymptoms.IsDisplayed()
                && spnTestingStatus.IsDisplayed()
                && txtTestingStatusDate.IsDisplayed()
                && spnDisposition.IsDisplayed()
                && txtDispositionDate.IsDisplayed();

            // Scroll down the screen to show the other elements
            //DriverUtils.ScrollBy(0, 1000);
            txtComments.ScrollToView();

            result = result && txtComments.IsDisplayed()
                && btnSaveLogEntry.IsDisplayed()
                && btnCancelLogEntry.IsDisplayed();                      

            if (!string.IsNullOrEmpty(typeofLogEntry))
            {
                switch (typeofLogEntry)
                {
                    case "Team":
                        result = result && txtEmployee.IsDisplayed();
                        break;

                    case "Resident":
                        result = result && txtEmployee.IsDisplayed();
                        break;

                    case "Client":
                        result = result && txtFirstName.IsDisplayed() && txtLastName.IsDisplayed() && txtMrn.IsDisplayed();
                        break;

                    default:
                        throw new Exception(string.Format("Type of Log Entry is incorrect"));
                }
            }

            return result;               
        }

        #endregion Check points
    }
}
