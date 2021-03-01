using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
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
    }
}
