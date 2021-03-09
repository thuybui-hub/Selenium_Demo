using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfectionLogAutomation.PageObject.BulkInsert
{
    public class BulkInsertPage : CommonPage
    {
        public readonly TextBox txtRegion;
        public readonly Ul lstRegion;
        public readonly TextBox txtCommunity;
        public readonly Ul lstCommunity;
        public readonly TextBox txtEmployee;
        public readonly Ul lstEmployee;
        public readonly Button btnSelectTeamMember;
        public readonly TextBox txtTestingDate;
        public readonly TextArea txtComments;
        public readonly BaseElement divNotes;
        public readonly Ul lstNotes;
        public readonly Button btnInsert;
        public readonly Button btnCancelBulkInsert;

        public BulkInsertPage()
        {
            txtRegion = new TextBox(By.XPath("//ul[@id=\"region_taglist\"]//following-sibling::input"));
            lstRegion = new Ul(By.XPath("//ul[@id=\"region_listbox\"]"));
            txtCommunity = new TextBox(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input"));
            lstCommunity = new Ul(By.XPath("//ul[@id=\"communityName_listbox\"]"));
            txtEmployee = new TextBox(By.XPath("//ul[@id=\"person_taglist\"]//following-sibling::input"));
            lstEmployee = new Ul(By.XPath("//ul[@id = \"person_listbox\"]"));
            btnSelectTeamMember = new Button(By.Id("advanceSearch"));
            txtTestingDate = new TextBox(By.Id("testingDate"));
            txtComments = new TextArea(By.XPath("//textarea[@id=\"comments\"]//preceding-sibling::iframe"));
            divNotes = new BaseElement(By.Id("divNotes"));
            lstNotes = new Ul(By.XPath("//div[@id=\"divNotes\"]//ul"));
            btnInsert = new Button(By.XPath("//button[@class=\"k-button btnSave\"]"));
            btnCancelBulkInsert = new Button(By.XPath("//button[@class=\"k-button btnCancel\"]"));
        }

        #region Main Actions
        #endregion Main Actions

        #region Check Points
        public bool DoesUIDisplayCorrectly()
        {
            bool result = txtRegion.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && txtEmployee.IsDisplayed()
                && btnSelectTeamMember.IsDisplayed()
                && txtComments.IsDisplayed()
                && divNotes.IsDisplayed();

            btnInsert.ScrollToView();

            result = result && btnInsert.IsDisplayed() && btnCancelBulkInsert.IsDisplayed();
            
            return result;
        }

        public bool DoesUIWithFormatDisplayCorrectly()
        {
            bool result = true;
            result = txtRegion.IsDisplayed()
                && txtRegion.GetAttribute("role").Equals("listbox")
                && txtCommunity.IsDisplayed()
                && txtCommunity.GetAttribute("role").Equals("listbox")
                && txtEmployee.IsDisplayed()
                && txtEmployee.GetAttribute("role").Contains("listbox")
                && txtTestingDate.IsDisplayed()
                && txtTestingDate.GetAttribute("data-role").Equals("datepicker")
                && txtComments.IsDisplayed();

            btnInsert.ScrollToView();

            result = result && btnInsert.IsDisplayed() && btnCancelBulkInsert.IsDisplayed();

            return result;
        }
        #endregion Check Points
    }
}
