using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public readonly Button btnAdvancedSearch;
        public readonly ComboBox cbbInfectionType;

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
            btnAdvancedSearch = new Button(By.Id("advanceSearch"));
            cbbInfectionType = new ComboBox(By.Id("infectionType"));
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
        /// <returns></returns>
        public bool DoesUIDisplayCorrectly()
        {
            return txtRegion.IsDisplayed()
                & txtCommunity.IsDisplayed()
                & txtEmployee.IsDisplayed();
                //& cbbInfectionType.IsDisplayed();                
        }

        #endregion Check points
    }
}
