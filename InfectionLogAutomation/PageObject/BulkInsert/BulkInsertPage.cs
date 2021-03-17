using InfectionLogAutomation.DataObject;
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
        public readonly Button btnSelectEmployee;
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
            btnSelectEmployee = new Button(By.Id("advanceSearch"));
            txtTestingDate = new TextBox(By.Id("testingDate"));
            txtComments = new TextArea(By.XPath("//textarea[@id=\"comments\"]//preceding-sibling::iframe"));
            divNotes = new BaseElement(By.Id("divNotes"));
            lstNotes = new Ul(By.XPath("//div[@id=\"divNotes\"]//ul"));
            btnInsert = new Button(By.XPath("//button[@class=\"k-button btnSave\"]"));
            btnCancelBulkInsert = new Button(By.XPath("//button[@class=\"k-button btnCancel\"]"));
        }

        #region Main Actions
        //public void FillBulkInsertRandomly(out LogEntryData logEntryData)
        //{
        //    DriverUtils.WaitForPageLoad();
        //    Random rd = new Random();
        //    List<string> list;
        //    logEntryData = new LogEntryData();

        //    string date = DateTime.Now.ToString("MM/dd/yyyy");
        //    string selectedValue, name, ID, symptom, comments;

        //    // Fill Region            
        //    list = GetItemsFromControlList(Fields.region);
        //    selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
        //    txtRegion.SendKeys(selectedValue);
        //    DriverUtils.wait(1);
        //    System.Windows.Forms.SendKeys.SendWait("{Enter}");
        //    logEntryData.Region = selectedValue;

        //    // Fill Community
        //    DriverUtils.WaitForPageLoad();
        //    list = GetItemsFromControlList(Fields.community);
        //    selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
        //    txtCommunity.SendKeys(selectedValue);
        //    DriverUtils.wait(1);
        //    System.Windows.Forms.SendKeys.SendWait("{Enter}");
        //    logEntryData.Community = selectedValue;

        //    // Fill Testing Date            
        //    txtOnsetDate.SendKeys(date);
        //    logEntryData.OnsetDate = date;

        //    // Fill Comments
        //    txtComments.ScrollToView();
        //    comments = "Comments " + Utils.GetRandomValue("random value");
        //    txtComments.Click();
        //    System.Windows.Forms.SendKeys.SendWait(comments);
        //    logEntryData.Comments = comments;
        //}
        #endregion Main Actions

        #region Check Points
        public bool DoesUIDisplayCorrectly()
        {
            bool result = txtRegion.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && txtEmployee.IsDisplayed()
                && btnSelectEmployee.IsDisplayed()
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
