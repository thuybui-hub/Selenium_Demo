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
        public readonly BaseElement divSelectEmployeePopup;
        public readonly Span spnPopupTitle;
        public readonly TextBox txtLastName;
        public readonly TextBox txtFirstName;
        public readonly Button btnSearch;
        public readonly Button btnSelect;
        public readonly Button btnCancel;
        public readonly BaseElement divSearchResultTable;
        public readonly Table tblSearchResultTable;
        public readonly Table tblSearchResultTableHeader;
        public TextBox txtFilterValue;
        public Button btnFilter;

        public BulkInsertPage()
        {
            divSelectEmployeePopup = new BaseElement(By.XPath("//div[@class=\"k-widget k-window k-dialog\"]"));
            spnPopupTitle = new Span(By.XPath("//span[@class=\"k-window-title k-dialog-title\"]"));
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
            txtFilterValue = new TextBox(By.XPath("//form[@aria-hidden=\"false\"]//input[@title=\"Value\"]"));
            btnFilter = new Button(By.XPath("//form[@aria-hidden=\"false\"]//button[text()=\"Filter\"]"));

        }

        #region Main Actions
        public void FillBulkInsertRandomly(out List<List<string>> listBulk, out int actNumOfSelectedEmployee, int expNumOfEmployee = 1)
        {
            DriverUtils.WaitForPageLoad();
            Random rd = new Random();
            List<string> list, listID, listName, listItem;
            listBulk = new List<List<string>>();
            string selectedValue, region, community, date, comments;

            date = DateTime.Now.ToString("MM/dd/yyyy");
            comments = "Comments " + Utils.GetRandomValue("random value");

            // Fill Region            
            list = GetItemsFromControlList(Fields.region);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtRegion.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            region = selectedValue;

            // Fill Community
            DriverUtils.WaitForPageLoad();
            list = GetItemsFromControlList(Fields.community);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtCommunity.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            community = selectedValue;

            // Fill Team Member / Resident
            btnSelectEmployee.Click();
            btnSearch.Click();
            DriverUtils.WaitForPageLoad();
            listID = tblSearchResultTable.GetTableAllCellValueInColumn("Person ID");

            if (expNumOfEmployee > listID.Count || expNumOfEmployee == listID.Count)
            {
                actNumOfSelectedEmployee = tblSearchResultTable.RowCount();

                for (int i = 0; i < tblSearchResultTable.RowCount(); i++)
                {
                    listName = tblSearchResultTable.GetTableAllCellValueInRow(i);

                    listItem = new List<string>();
                    listItem.Add(region);
                    listItem.Add(community);
                    listItem.Add(listName[3]);
                    listItem.Add(listName[1] + ", " + listName[2]);
                    listItem.Add(date);
                    listItem.Add(comments);

                    listBulk.Add(listItem);
                }
                CheckAllSearchRecords();
            }
            else
            {
                actNumOfSelectedEmployee = expNumOfEmployee;
                List<IWebElement> lstCheckbox = new List<IWebElement>(tblSearchResultTable.GetElement().FindElements(By.TagName("input")));
                int index;
                while (expNumOfEmployee > 0)
                {
                    listItem = new List<string>();
                    listItem.Add(region);
                    listItem.Add(community);

                    index = rd.Next(0, listID.Count - 1);
                    listItem.Add(listID[index]);
                    
                    int rowIndex = tblSearchResultTable.GetTableRowIndex(3, listID[index]);
                    listName = tblSearchResultTable.GetTableAllCellValueInRow(rowIndex);
                    listItem.Add(listName[1] + ", " + listName[2]);

                    listItem.Add(date);
                    listItem.Add(comments);

                    lstCheckbox[rowIndex].Click();
                    listID.RemoveAt(index);
                    expNumOfEmployee--;

                    listBulk.Add(listItem);
                }
            }
            btnSelect.Click();

            // Fill Testing Date            
            txtTestingDate.SendKeys(date);

            // Fill Comments
            txtComments.ScrollToView();
            txtComments.Click();
            System.Windows.Forms.SendKeys.SendWait(comments);
        }

        public void FillLogEntryInfo(string region, string community, List<string> employeeList, string date, string comment)
        {
            DriverUtils.WaitForPageLoad();
            Random rd = new Random();

            // Fill region
            if (!string.IsNullOrEmpty(region))
            {
                txtRegion.SendKeys(region);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            // Fill community
            if (!string.IsNullOrEmpty(community))
            {
                DriverUtils.WaitForPageLoad();
                txtCommunity.SendKeys(community);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            // Fill Team Member / Resident
            if (employeeList != null)
            {
                int i = 0;
                do
                {
                    OpenSearchEmployeePopup();
                    PerformASearchEmployee(employeeList[i]);
                    if (!divNoRecords.IsDisplayed())
                    {
                        CheckOnEmployeeCheckBox(0);
                        SelectCheckedEmployee();
                    }
                    else
                    {
                        CancelSearchEmployee();
                    }
                    i++;
                }
                while (i < employeeList.Count);
            }

            // Fill Testing Date
            if (!string.IsNullOrEmpty(date))
            {
                txtTestingDate.SendKeys(date);
            }

            // Fill Comments
            if (!string.IsNullOrEmpty(comment))
            {
                txtComments.ScrollToView();
                txtComments.Click();
                System.Windows.Forms.SendKeys.SendWait("^a");
                System.Windows.Forms.SendKeys.SendWait(comment);
            }
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

        public void OpenSearchEmployeePopup()
        {
            DriverUtils.WaitForPageLoad();
            btnSelectEmployee.Click();
        }

        public void FillInSearchEnployeeForm(string name)
        {
            string firstName = name.Substring(0, name.IndexOf(","));
            string lastName = name.Substring(name.IndexOf(",") + 2, name.Length - name.IndexOf(",") - 2);

            FillInSearchEnployeeForm(lastName, firstName);
        }

        public void FillInSearchEnployeeForm(string lastName = null, string firstName = null)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                txtLastName.SendKeys(lastName);
                System.Windows.Forms.SendKeys.SendWait("{tab}");
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                txtFirstName.SendKeys(firstName);
                System.Windows.Forms.SendKeys.SendWait("{tab}");
            }
        }

        public void ClearSearchCriteria()
        {
            DriverUtils.WaitForPageLoad();
            txtLastName.GetElement().Clear();
            txtFirstName.GetElement().Clear();
            SearchEmployee();
        }

        public void SearchEmployee()
        {
            DriverUtils.WaitForPageLoad();
            btnSearch.Click();
        }

        public void PerformASearchEmployee(string name)
        {
            FillInSearchEnployeeForm(name);
            SearchEmployee();
        }

        public void PerformASearchEmployee(string lastName, string firstName)
        {
            FillInSearchEnployeeForm(lastName, firstName);
            SearchEmployee();
        }

        public void CancelSearchEmployee()
        {
            DriverUtils.WaitForPageLoad();
            btnCancel.Click();
        }

        public void CheckOnEmployeeCheckBox(List<string> ID)
        {
            DriverUtils.WaitForPageLoad();
            foreach (string i in ID)
            {
                int rowIndex = tblSearchResultTable.GetTableRowIndex(3, i);
                CheckOnEmployeeCheckBox(rowIndex);
            }
        }

        public void CheckOnEmployeeCheckBox(int rowIndex)
        {
            DriverUtils.WaitForPageLoad();
            List<IWebElement> lstCheckbox = new List<IWebElement>(tblSearchResultTable.GetElement().FindElements(By.TagName("input")));
            lstCheckbox[rowIndex].Click();
        }

        public void CheckAllSearchRecords()
        {
            DriverUtils.WaitForPageLoad();
            IWebElement checkbox = tblSearchResultTableHeader.GetElement().FindElement(By.TagName("input"));
            checkbox.Click();
        }

        public void SelectCheckedEmployee()
        {
            DriverUtils.WaitForPageLoad();
            btnSelect.Click();
        }

        public Link GetTableColumnFilter(string columnName)
        {
            return new Link(By.XPath("//a[text()=\"" + columnName + "\"]//preceding-sibling::a"));
        }

        public void ClickOnTableColumnFilter(string columnName)
        {
            DriverUtils.WaitForPageLoad();
            Link columnFilter = GetTableColumnFilter(columnName);
            columnFilter.WaitForVisible();
            columnFilter.Click();
        }

        public void EnterFilterData(string filterValue)
        {
            DriverUtils.WaitForPageLoad();
            txtFilterValue = new TextBox(By.XPath("//form[@aria-hidden=\"false\"]//input[@title=\"Value\"]"));
            btnFilter = new Button(By.XPath("//form[@aria-hidden=\"false\"]//button[text()=\"Filter\"]"));
            txtFilterValue.SendKeys(filterValue);
            btnFilter.Click();
        }

        public void FilterATableColumn(string columnName, string filterValue)
        {
            DriverUtils.WaitForPageLoad();
            DriverUtils.wait(1);
            ClickOnTableColumnFilter(columnName);
            EnterFilterData(filterValue);
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

        public bool CheckSelectEmployeePopupExit()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            result = divSelectEmployeePopup.IsDisplayed();
            return result;
        }

        public bool DoesSelectPopupUIDisplayCorrectly()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;

            result = txtLastName.IsDisplayed()
                && txtFirstName.IsDisplayed()
                && btnSearch.IsDisplayed()
                && btnSelect.IsDisplayed()
                && btnCancel.IsDisplayed();                

            btnSearch.Click();
            DriverUtils.WaitForPageLoad();

            result = result
                  && divSearchResultTable.IsDisplayed()
                  && IsColumnHeaderSortable(tblSearchResultTableHeader, "Last Name")
                  && IsColumnHeaderSortable(tblSearchResultTableHeader, "First Name")
                  && IsColumnHeaderSortable(tblSearchResultTableHeader, "Person ID");

            return result;
        }
        
        public bool DoesSearchRecordsPartialMatchSearchCriteria(string lastName, string firstName)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;

            List<string> firstNameList, lastNamelist;

            firstNameList = tblSearchResultTable.GetTableAllCellValueInColumn(2);
            lastNamelist = tblSearchResultTable.GetTableAllCellValueInColumn(1);
            if (!string.IsNullOrEmpty(lastName))
            {
                result = result && lastNamelist.All(ln => ln.ToLower().Contains(lastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                result = result && firstNameList.All(fn => fn.ToLower().Contains(firstName.ToLower()));
            }

            return result;
        }

        public bool AreCorrespondingEmployeesCheckedOrUnchecked(List<string> ID = null, string status = "Checked")
        {
            bool result = true;
            List<IWebElement> lstCheckbox;

            if (ID != null)
            {
                foreach (string i in ID)
                {
                    int rowIndex = tblSearchResultTable.GetTableRowIndex(3, i);
                    switch (status)
                    {
                        case "Checked":
                            lstCheckbox = new List<IWebElement>(tblSearchResultTable.GetElement().FindElements(By.TagName("input")));
                            result = result && lstCheckbox[rowIndex].GetAttribute("aria-checked").Equals("true");
                            break;
                        case "Unchecked":
                            lstCheckbox = new List<IWebElement>(tblSearchResultTable.GetElement().FindElements(By.TagName("input")));
                            result = result && lstCheckbox[rowIndex].GetAttribute("aria-checked").Equals("false");
                            break;
                    }
                }
            }
            else
            {
                if (status.Equals("CheckedAll"))
                {
                    IWebElement checkbox = tblSearchResultTableHeader.GetElement().FindElement(By.TagName("input"));
                    result = result && checkbox.GetAttribute("aria-checked").Equals("true");
                }
            }
            return result;
        }

        public bool DoesFilterDataDisplayCorrectly(string columnName, string filterValue)
        {
            DriverUtils.WaitForPageLoad();
            List<string> actualResult;
            if (divNoRecords.IsDisplayed() == false)
            {
                actualResult = tblSearchResultTable.GetTableAllCellValueInColumn(columnName);
                return actualResult.All(x => x.ToLower() == filterValue.ToLower());
            }
            else return true;
        }
        #endregion Check Points
    }
}
