using InfectionLogAutomation.PageObject.Common;
using Microsoft.VisualStudio.TestTools.UITesting;
using OpenQA.Selenium;
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

        #region Actions
        public LogEntryDetailPage()
        {
            lblTitle = new Label(By.XPath("//div[@id=\"logForm\"]//h3"));
            txtRegion = new TextBox(By.XPath("//ul[@id=\"region_taglist\"]//following-sibling::input"));
            cbbRegion = new ComboBox(By.Id("region"));
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
            txtSymptoms = new TextArea(By.Id("symptoms"));
            spnTestingStatus = new Span(By.XPath("//label[@id=\"testingStatus_label\"]//following-sibling::span"));
            cbbTestingStatus = new ComboBox(By.Id("testingStatus"));
            txtTestingStatusDate = new TextBox(By.Id("testingStatusDate"));
            spnDisposition = new Span(By.XPath("//label[@id=\"disposition_label\"]//following-sibling::span"));
            cbbDisposition = new ComboBox(By.Id("disposition"));
            txtDispositionDate = new TextBox(By.Id("dispositionDate"));
            txtComments = new TextArea(By.Id("comments"));
            btnSaveLogEntry = new Button(By.XPath("//button[@class=\"k-button btnSave\"]"));
            btnCancelLogEntry = new Button(By.XPath("//button[@class=\"k-button btnCancel\"]"));
        }

        #endregion Actions

        #region Check points
        /// <summary>
        /// Check to see if a form displays via form's title
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public bool CheckPageExist(string pageTitle)
        {
            DriverUtils.WaitForPageLoad();
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
                && cbbTestingStatus.IsDisplayed()
                && txtTestingStatusDate.IsDisplayed()
                && cbbDisposition.IsDisplayed()
                && txtDispositionDate.IsDisplayed();

            //btnSaveLogEntry.ScrollToView();
            txtComments.MoveToElement();
            //Mouse.MoveScrollWheel(-1);

            bool test = txtComments.IsDisplayed();

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
