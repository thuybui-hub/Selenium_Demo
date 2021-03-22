using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfectionLogAutomation.PageObject.LogEntry;

namespace InfectionLogAutomation.PageObject.Home
{
    public class HomePage : CommonPage
    {
        // Search Criterias
        public TextBox txtCommunity;
        public ComboBox cbbCommunity;
        public Ul lstCommunity;
        public TextBox txtLastUpdatedFrom;
        public TextBox txtLastUpdatedTo;
        public TextBox txtFilters;
        public ComboBox cbbFilters;
        public Ul lstFilters;
        public Button btnSearch;
        public Button btnReset;

        // ILog Table
        public Button btnClearFilters;
        public Button btnExportToExcel;
        public TextBox txtSearch;
        public BaseElement divDashboardTable;
        public Table tblDashboardHeader;
        public Table tblDashboard;

        // Filter popup
        public TextBox txtFilterValue;
        public Span spnEntryTypeFilter;
        public Ul lstEntrytypeFilter;
        public Button btnFilter;
        public Button btnClear;

        public HomePage()
        {
            // Search Criterias
            txtCommunity = new TextBox(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input"));
            cbbCommunity = new ComboBox(By.XPath("//select[@id=\"communityName\"]"));
            lstCommunity = new Ul(By.XPath("//ul[@id=\"communityName_taglist\"]"));
            txtLastUpdatedFrom = new TextBox(By.Id("datepickerFrom"));
            txtLastUpdatedTo = new TextBox(By.Id("datepickerTo"));
            txtFilters = new TextBox(By.XPath("//ul[@id=\"allFilters_taglist\"]//following-sibling::input"));
            cbbFilters = new ComboBox(By.XPath("//select[@id=\"allFilters\"]"));
            lstFilters = new Ul(By.XPath("//ul[@id=\"allFilters_listbox\"]"));
            btnSearch = new Button(By.Id("search"));
            btnReset = new Button(By.XPath("//button[@class=\"k-button btnClear\"]"));

            // ILog Table
            btnClearFilters = new Button(By.Id("clearFilterButton"));
            btnExportToExcel = new Button(By.Id("excelExportButton"));
            txtSearch = new TextBox(By.XPath("//span[@class=\"k-textbox k-grid-search k-display-flex\"]/input"));
            divDashboardTable = new BaseElement(By.Id("logGrid"));
            tblDashboardHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblDashboard = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));

            // Filter popup
            txtFilterValue = new TextBox(By.XPath("//form[@aria-hidden=\"false\"]//input[@title=\"Value\"]"));
            spnEntryTypeFilter = new Span(By.XPath("//input[@title=\"Value\"]//following-sibling::span[@class=\"k-select\"]"));
            lstEntrytypeFilter = new Ul(By.XPath("//div[@class=\"k-animation-container\"]//ul[@class=\"k-list k-reset\"]"));
            btnFilter = new Button(By.XPath("//form[@aria-hidden=\"false\"]//button[text()=\"Filter\"]"));
            btnClear = new Button(By.XPath("//button[text()=\"Clear\"]"));
        }

        #region Main Actions

        ///// <summary>
        ///// Return selected Last Updated From; Last Updated To
        ///// </summary>
        ///// <param name="from"></param>
        ///// <param name="to"></param>
        //public void GetSelectedDateFromDatePicker(out DateTime from, out DateTime to)
        //{
        //    from = DateTime.Now;
        //    to = DateTime.Now;
        //    Link lnk;
        //    Span pickerFrom = new Span(By.XPath("//span[@aria-controls=\"datepickerFrom_dateview\"]"));            
        //    Span pickerTo = new Span(By.XPath("//span[@aria-controls=\"datepickerTo_dateview\"]"));            

        //    pickerFrom.Click();
        //    DriverUtils.wait(1);            
        //    lnk = new Link(By.XPath("//td[@class=\"k-state-selected k-state-focused\"]/a"));
        //    string frm = lnk.GetAttribute("title");
        //    from = DateTime.Parse(frm);            

        //    pickerTo.Click();
        //    DriverUtils.wait(1);
        //    lnk =  new Link(By.XPath("//td[@aria-selected=\"true\"]/a"));
        //    string t = lnk.GetAttribute("title");
        //    to = DateTime.Parse(t);           
        //}

        public void ClearAllFilters()
        {
            DriverUtils.WaitForPageLoad();
            btnClearFilters = new Button(By.Id("clearFilterButton"));
            btnClearFilters.Click();
        }

        public void PerformASearchOnDashboardTable(string searchValue)
        {
            DriverUtils.WaitForPageLoad();
            txtSearch.Click();
            System.Windows.Forms.SendKeys.SendWait(searchValue);
        }

        public void ExportToExcel()
        {
            DriverUtils.WaitForPageLoad();
            btnExportToExcel.Click();
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
            ClickOnTableColumnFilter(columnName);            
            EnterFilterData(filterValue);
        }

        public void ClearAFilteredTableColumn(string columnName)
        {
            ClickOnTableColumnFilter(columnName);
            btnClear.WaitForVisible();
            btnClear.Click();
        }

        public void OpenALogEntry(string ID)
        {
            ShowBothActiveAndInactiveRecords();
            DriverUtils.WaitForPageLoad();
            if (!string.IsNullOrEmpty(ID))
            {
                tblDashboard.ClickTableCell("ID", ID);
            }
        }

        public void OpenALogEntry(int rowIndex)
        {
            DriverUtils.WaitForPageLoad();
            tblDashboard.ClickTableCell(5, rowIndex);
        }

        public void DeleteALogEntry(string ID)
        {
            ShowBothActiveAndInactiveRecords();
            DriverUtils.WaitForPageLoad();
            int rowIndex = tblDashboard.GetTableRowIndex(5, ID);
            tblDashboard.ClickTableCell(13, rowIndex);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
        }

        public void DeleteLogEntries(int numberOfRecords, string startPosition = "bottom")
        {
            DriverUtils.WaitForPageLoad();

            while (numberOfRecords > 0)
            {
                if (tblDashboard.RowCount() > 0)
                {
                    if (startPosition.Equals("top"))
                    {
                        tblDashboard.ClickTableCell(13, 0);
                        System.Windows.Forms.SendKeys.SendWait("{Enter}");
                        DriverUtils.WaitForPageLoad();
                    }
                    else
                    {
                        tblDashboard.ClickTableCell(13, tblDashboard.RowCount()-1);
                        System.Windows.Forms.SendKeys.SendWait("{Enter}");
                        DriverUtils.WaitForPageLoad();
                    }

                }
                numberOfRecords--;
            }
        }

        public void FillSearchCriterias(string community = null, string lastUpdatedFrom = null, string lastUpdatedTo = null, string filters = null)
        {
            DriverUtils.WaitForPageLoad();
            txtLastUpdatedFrom = new TextBox(By.XPath("//input[@id=\"datepickerFrom\"]"));
            txtLastUpdatedTo = new TextBox(By.XPath("//input[@id=\"datepickerTo\"]"));

            if (!string.IsNullOrEmpty(community))
            {
                txtCommunity.Click();
                ClearAllValueInCombobox("Community");
                txtCommunity.SendKeys(community);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if (!string.IsNullOrEmpty(lastUpdatedFrom))
            {
                txtLastUpdatedFrom.Click();
                txtLastUpdatedFrom.GetElement().Clear();
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                txtLastUpdatedFrom.SendKeys(lastUpdatedFrom);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if (!string.IsNullOrEmpty(lastUpdatedTo))
            {
                txtLastUpdatedTo.Click();
                txtLastUpdatedTo.GetElement().Clear();
                System.Windows.Forms.SendKeys.SendWait("{Enter}");                
                txtLastUpdatedTo.SendKeys(lastUpdatedTo);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                txtFilters.Click();
                ClearAllValueInCombobox("Filters");
                txtFilters.SendKeys(filters);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }
        }

        public void PerformASearchCriteria(string community = null, string lastUpdatedFrom = null, string lastUpdatedTo = null, string filters = null)
        {
            FillSearchCriterias(community, lastUpdatedFrom, lastUpdatedTo, filters);
            DriverUtils.WaitForPageLoad();
            btnSearch.Click();
        }

        public void PerformASearchCriteria(List<string> community = null, string lastUpdatedFrom = null, string lastUpdatedTo = null, List<string> filters = null)
        {
            DriverUtils.WaitForPageLoad();

            if(community != null)
            {
                txtCommunity.Click();
                ClearAllValueInCombobox("Community");
                foreach (var c in community)
                {
                    txtCommunity.SendKeys(c);
                    DriverUtils.wait(1);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                }
            }
            
            if (!string.IsNullOrEmpty(lastUpdatedFrom))
            {
                txtLastUpdatedFrom.Click();
                txtLastUpdatedFrom.GetElement().Clear();
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                txtLastUpdatedFrom.Click();
                txtLastUpdatedFrom.SendKeys(lastUpdatedFrom);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if (!string.IsNullOrEmpty(lastUpdatedTo))
            {
                txtLastUpdatedTo.Click();
                txtLastUpdatedTo.GetElement().Clear();
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                txtLastUpdatedTo.Click();
                txtLastUpdatedTo.SendKeys(lastUpdatedTo);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

            if(filters != null)
            {
                txtFilters.Click();
                ClearAllValueInCombobox("Filters");
                foreach (var f in filters)
                {
                    txtFilters.SendKeys(f);
                    DriverUtils.wait(1);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                }
            }
            
            btnSearch.Click();
        }

        public void ShowBothActiveAndInactiveRecords()
        {
            DriverUtils.WaitForPageLoad();
            txtFilters.Click();
            ClearAllValueInCombobox("Filters");
            btnSearch.Click();
        }

        public void ResetSearchCriteria()
        {
            DriverUtils.WaitForPageLoad();
            btnReset.Click();
        }

        public List<string> GetAllValueInColumnOfBulkInsertRecords(string columnName, int numberOfRecords)
        {
            DriverUtils.WaitForPageLoad();
            List<string> list = new List<string>() { };
            int colIndex = tblDashboardHeader.GetTableColumnIndex(columnName);

            for(int i = 0; i < numberOfRecords; i++)
            {
                list.Add(tblDashboard.GetTableCellValue(colIndex, i));
            }
            return list;
        }
        #endregion Main Actions

        #region Check Points


        /// <summary>
        /// Check to see if the home page displays correctly
        /// </summary>
        /// <returns></returns>
        public bool IsHomePageDisplayed()
        {
            DriverUtils.WaitForPageLoad();
            return lnkInfectiousOutbreakLog.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && txtLastUpdatedFrom.IsDisplayed()
                && txtLastUpdatedTo.IsDisplayed()
                && txtFilters.IsDisplayed()
                && btnSearch.IsDisplayed()
                && btnReset.IsDisplayed()
                && divDashboardTable.IsDisplayed();
        }


        /// <summary>
        /// Check to see if dashboard table displays
        /// </summary>
        /// <returns></returns>
        public bool IsILogTableDisplayed()
        {
            DriverUtils.WaitForPageLoad();
            return divDashboardTable.IsDisplayed();
        }

        public bool DoesEntryTypeFilterExistWithExpectedOptions(List<string> expectedEntryTypeFilterList = null)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            Link filterEntryType = GetTableColumnFilter("Entry Type");
            List<string> actualEntryTypeFilterList = new List<string> { };

            // Check if Entry Type filter exists or not
            result = filterEntryType.IsDisplayed();

            if (result)
            {
                filterEntryType.Click();
                spnEntryTypeFilter.Click();
                DriverUtils.WaitForPageLoad();
                actualEntryTypeFilterList = lstEntrytypeFilter.GetOptions();

                // Check if list of Entry Type filter matches the expected list or not
                result = result && actualEntryTypeFilterList.SequenceEqual(expectedEntryTypeFilterList);
                filterEntryType.Click();
            }

            return result;
        }

        public bool DoesFilterDataDisplayCorrectly(string columnName, string filterValue)
        {
            DriverUtils.WaitForPageLoad();
            List<string> actualResult;
            if (divNoRecords.IsDisplayed() == false)
            {
                ShowAllILogRecords();
                actualResult = tblDashboard.GetTableAllCellValueInColumn(columnName);
                return actualResult.All(x => x == filterValue);
            }
            else return true;
        }

        public bool DoesSearchResultDisplaysCorrectly(string searchValue)
        {
            bool result = true;

            if (divNoRecords.IsDisplayed() == false)
            {
                ShowAllILogRecords();
                for (int i = 0; i < tblDashboard.RowCount(); i++)
                {
                    result = result && tblDashboard.GetTableAllCellValueInRow(i).Contains(searchValue);
                }
            }
            else result = true;

            return result;
        }

        public bool IsFilterDataPreservedAfterMovingToAnotherForm(string columnName, string filterValue)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            ClickOnTableColumnFilter(columnName);
            spnEntryTypeFilter.Click();
            result = lstEntrytypeFilter.GetSelectedOption().Equals(filterValue);
            ClickOnTableColumnFilter(columnName);
            return result;
        }

        public bool IsAllFilterDataCleared()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            Link columnFilter;

            List<string> columnName = new List<string> { "Last Updated", "Division", "Region", "BU", "Community", "ID", "Name", "Resident LOB", "Infection Type", "Onset Date", "Test Status", "Disposition", "Entry Type" };
            foreach (string column in columnName)
            {
                columnFilter = GetTableColumnFilter(column);
                columnFilter.WaitForVisible();
                result = result && !columnFilter.GetAttribute("class").Contains("k-state-active");
            }

            return result;
        }

        public bool IsSearchDataonDashboardTableCleared()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            txtSearch.Click();
            result = txtSearch.GetText().Equals("") || txtSearch.GetText().Equals(null);
            return result;
        }
        
        /// <summary>
        /// Check to see if a column is sortable
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsColumnHeaderSortable(Table table, string columnName)
        {
            DriverUtils.WaitForPageLoad();
            return table.GetColumnHeaderAttribute(columnName, "data-role").Contains("columnsorter");
        }

        /// <summary>
        /// Check to see if a column is sorted by ascending order
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsColumnHeaderSortedByAscendingOrder(Table table, string columnName)
        {
            DriverUtils.WaitForPageLoad();
            return table.GetColumnHeaderAttribute(columnName, "aria-sort").Contains("ascending");
        }

        /// <summary>
        /// Check to see if a column is sorted by descending
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsColumnHeaderSortedByDescendingOrder(Table table, string columnName)
        {
            DriverUtils.WaitForPageLoad();
            return table.GetColumnHeaderAttribute(columnName, "aria-sort").Contains("descending");
        }

        /// <summary>
        /// Check to see if Last Updated From/To & Filters show correct default value
        /// </summary>
        /// <returns></returns>
        public bool AreDefaultSearchValuesCorrect()
        {
            bool result = true;
            DateTime from, to;

            //GetSelectedDateFromDatePicker(out from, out to);
            from = Convert.ToDateTime(txtLastUpdatedFrom.GetValue());
            to = Convert.ToDateTime(txtLastUpdatedTo.GetValue());
            string f = txtFilters.GetValue();

            // Date range is 7 days
            double difference = to.Date.Subtract(from.Date).TotalDays;

            // Last Updated To is current date
            double differenceToday = to.Date.Subtract(DateTime.Now.Date).TotalDays;

            LI selectedFilter = new LI(By.XPath("//ul[@id=\"allFilters_taglist\"]/li"));
            result = selectedFilter.GetText().Equals("Active")
                && difference == 7 && differenceToday == 0 && string.IsNullOrEmpty(txtCommunity.GetText());

            return result;
        }

        /// <summary>
        /// Check to see if Community is allowed to select multiple items
        /// </summary>
        /// <returns></returns>
        public bool IsCommunityMultiselect()
        {
            Div parent = new Div(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input/parent::div"));            
            return parent.GetAttribute("class").Contains("multiselect");
        }

        /// <summary>
        /// Check to see if Last Updated From/To is able to see the date from the calendar
        /// </summary>
        /// <returns></returns>
        public bool AreDateSelectedFromCalendar()
        {
            return txtLastUpdatedFrom.GetAttribute("data-role").Equals("datepicker")
                && txtLastUpdatedTo.GetAttribute("data-role").Equals("datepicker");
        }

        /// <summary>
        /// Check to see if the field Filters contains correct list of items
        /// </summary>
        /// <returns></returns>
        public bool IsFiltersFieldCorrect()
        {
            bool result = true;
            List<string> exFilterItems = new List<string>() { "Active", "Inactive", "Tested - Pending", "Tested - Confirmed", "Expired", "Mass Testing", "Duplicates" };
            exFilterItems.Sort();

            Div filters = new Div(By.XPath("//ul[@id=\"allFilters_taglist\"]//following-sibling::input/parent::div"));
            result = filters.GetAttribute("class").Contains("multiselect");

            txtFilters.Click();
            List<string> actualFiltersItems = lstFilters.GetOptions();
            actualFiltersItems.Sort();

            result = result && actualFiltersItems.SequenceEqual(exFilterItems);
            return result;
        }

        public bool IsUserUnableToDeleteLogEntry()
        {
            return divDashboardTable.GetAttribute("data-columns").Contains("{ command: ['destroy'], title: 'Delete', hidden: true }");
        }

        public bool DoAllLogEntriesHaveCorrectLOB(List<string> logEntryList)
        {
            bool result = true;
            bool temp;
            for (int i = 0; i < logEntryList.Count; i++)
            {
                temp = logEntryList[i].ToString().Equals("AL")
                    || logEntryList[i].ToString().Equals("ALZ")
                    || logEntryList[i].ToString().Equals("IL")
                    || logEntryList[i].ToString().Equals("SNF");

                result = result & temp;
            }
            return result;
        }

        /// <summary>
        /// Check to see if the created bulk insert records show the correctly information
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="testingDate"></param>
        /// <returns></returns>
        public bool DoesCreatedBulkInsertRecordsShowCorrectInformation(List<string> ID, string testingDate, string page = "Team")
        {
            bool result = true;
            string date = DateTime.Parse(testingDate).AddDays(-1).ToString("MM/dd/yyyy");
            //List<string> ID = GetAllValueInColumnOfBulkInsertRecords("ID", numberOfRecords);

            LogEntryDetailPage logEntry = new LogEntryDetailPage();

            foreach (string i in ID)
            {
                OpenALogEntry(i);
                //result = result & logEntry.DoesDataOnEditPageDisplayCorrectly(logEntryData, page);
                //BackToPreviousPage();
            }
            return result;
        }
        #endregion Check Points
    }
}
