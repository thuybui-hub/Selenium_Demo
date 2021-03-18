using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.PageObject.Common;
using InfectionLogAutomation.Utilities;
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

        // Select Employee pop-up
        public readonly TextBox txtLastName;
        public readonly TextBox txtFirstName;
        public readonly Button btnSearch;
        public readonly Button btnSelect;
        public readonly Button btnCancel;
        public readonly BaseElement divSearchResultTable;
        public readonly Table tblSearchResultTable;
        public readonly Table tblSearchResultTableHeader;

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
            btnCancelBulkInsert = new Button(By.XPath("//div[@id=\"logForm\"]//following-sibling::div//button[text()=\"Cancel\"]"));

            // Select Employee pop-up
            txtLastName = new TextBox(By.Id("aslastName"));
            txtFirstName = new TextBox(By.Id("asfirstName"));
            btnSearch = new Button(By.Id("searchButton"));
            btnSelect = new Button(By.XPath("//button[@class=\"k-button k-primary\" and text()=\"Select\"]"));
            btnCancel = new Button(By.XPath("//div[@id=\"divSearch\"]//following-sibling::div//button[text()=\"Cancel\"]"));
            divSearchResultTable = new BaseElement(By.Id("employeesearchresult"));
            tblSearchResultTableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblSearchResultTable = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));

        }

        #region Main Actions
        public void FillBulkInsertRandomly(out BulkProcessingData bulkProcessingData, out int actNumOfSelectedEmployee, int expNumOfEmployee = 1)
        {
            DriverUtils.WaitForPageLoad();
            Random rd = new Random();
            List<string> list, listID, listName;
            bulkProcessingData = new BulkProcessingData();

            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string selectedValue, comments;

            // Fill Region            
            list = GetItemsFromControlList(Fields.region);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtRegion.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            bulkProcessingData.Region = selectedValue;

            // Fill Community
            DriverUtils.WaitForPageLoad();
            list = GetItemsFromControlList(Fields.community);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtCommunity.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            bulkProcessingData.Community = selectedValue;

            // Fill Team Member / Resident
            btnSelectEmployee.Click();
            btnSearch.Click();
            DriverUtils.WaitForPageLoad();
            listID = tblSearchResultTable.GetTableAllCellValueInColumn("Person ID");

            List<string> lstSelectedID = new List<string>();
            List<string> lstSelectedName = new List<string>();

            if (expNumOfEmployee > listID.Count || expNumOfEmployee == listID.Count)
            {
                actNumOfSelectedEmployee = tblSearchResultTable.RowCount();
                IWebElement checkbox = tblSearchResultTableHeader.GetElement().FindElement(By.TagName("input"));
                checkbox.Click();
            }
            else
            {
                actNumOfSelectedEmployee = expNumOfEmployee;
                List<IWebElement> lstCheckbox = new List<IWebElement>(tblSearchResultTable.GetElement().FindElements(By.TagName("input")));
                int index;
                while (expNumOfEmployee > 0)
                {
                    index = rd.Next(0, listID.Count - 1);
                    lstSelectedID.Add(listID[index]);
                    int rowIndex = tblSearchResultTable.GetTableRowIndex(3, listID[index]);
                    listName = tblSearchResultTable.GetTableAllCellValueInRow(rowIndex);
                    lstSelectedName.Add(listName[1] + ", " + listName[2]);
                    lstCheckbox[rowIndex].Click();
                    listID.RemoveAt(index);
                    expNumOfEmployee--;
                }
            }
            btnSelect.Click();

            // Fill Testing Date            
            txtTestingDate.SendKeys(date);
            bulkProcessingData.TestingDate = date;

            // Fill Comments
            txtComments.ScrollToView();
            comments = "Comments " + Utils.GetRandomValue("random value");
            txtComments.Click();
            System.Windows.Forms.SendKeys.SendWait(comments);
            bulkProcessingData.Comments = comments;
        }

        public void SaveBulkInsert()
        {
            DriverUtils.WaitForPageLoad();
            btnInsert.ScrollToView();
            btnInsert.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void CancelBulkInsert()
        {
            DriverUtils.WaitForPageLoad();
            btnCancelBulkInsert.ScrollToView();
            btnCancelBulkInsert.Click();
            DriverUtils.WaitForPageLoad();
        }

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
                    element = lstRegion;
                    txtRegion.Click();
                    break;

                case Fields.community:
                    element = lstCommunity;
                    txtCommunity.Click();
                    break;
                case Fields.employee:
                    element = lstEmployee;
                    txtEmployee.Click();
                    break;

                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }
            List<string> displayedList = element.GetOptions();

            switch (field)
            {
                case Fields.region:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;
                case Fields.community:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;
                case Fields.employee:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;

                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }

            return displayedList;
        }

        public string test()
        {
            return divNotes.GetText();
        }
        #endregion Main Actions

        #region Check Points
        public bool DoesUIDisplayCorrectly()
        {
            bool result = txtRegion.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && txtEmployee.IsDisplayed()
                && btnSelectEmployee.IsDisplayed()
                && txtComments.IsDisplayed()
                && divNotes.IsDisplayed()
                && divNotes.GetText().Equals("The following values will be used to create the log entries:\r\nOnset Date: (same as Testing Date)\r\nSymptoms: N/A\r\nCurrent Test Status: Tested - Pending\r\nCurrent Test Status Date: (same as Testing Date)\r\nCurrent Disposition: Not Quarantined\r\nCurrent Disposition Date: (same as Testing Date)");

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
