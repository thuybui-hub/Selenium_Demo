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
    }
}
