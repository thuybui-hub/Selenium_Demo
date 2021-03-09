using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.PageObject.Home
{
    public class HomePage : CommonPage
    {
        // Search Criterias
        public readonly TextBox txtCommunity;
        public readonly ComboBox cbbCommunity;
        public readonly Ul lstCommunity;
        public readonly TextBox txtLastUpdatedFrom;
        public readonly TextBox txtLastUpdatedTo;
        public readonly TextBox txtFilters;
        public readonly ComboBox cbbFilters;
        public readonly Ul lstFilters;
        public readonly Button btnSearch;
        public readonly Button btnReset;

        // ILog Table
        public readonly Button btnClearFilters;
        public readonly Button btnExportToExcel;
        public readonly TextBox txtSearch;
        public readonly BaseElement divDashboardTable;
        public readonly Table tblDashboardTableHeader;
        public readonly Table tblDashboardTable;
        public readonly BaseElement divNoRecords;

        // Filter popup
        public readonly TextBox txtFilterValue;
        public readonly Span spnEntryTypeFilter;
        public readonly Ul lstEntrytypeFilter;
        public readonly Button btnFilter;
        public readonly Button btnClear;

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
            lstFilters = new Ul(By.XPath("//ul[@id=\"allFilters_taglist\"]"));
            btnSearch = new Button(By.Id("search"));
            btnReset = new Button(By.XPath("//button[@class=\"k-button btnClear\"]"));

            // ILog Table
            btnClearFilters = new Button(By.Id("clearFilterButton"));
            btnExportToExcel = new Button(By.Id("excelExportButton"));
            txtSearch = new TextBox(By.XPath("//span[@class=\"k-textbox k-grid-search k-display-flex\"]/input"));
            divDashboardTable = new BaseElement(By.Id("logGrid"));
            tblDashboardTableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblDashboardTable = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
            divNoRecords = new BaseElement(By.XPath("//div[@class=\"k-grid-norecords\"]"));

            // Filter popup
            txtFilterValue = new TextBox(By.XPath("//input[@title=\"Value\"]"));
            spnEntryTypeFilter = new Span(By.XPath("//input[@title=\"Value\"]//following-sibling::span[@class=\"k-select\"]"));
            lstEntrytypeFilter = new Ul(By.XPath("//div[@class=\"k-animation-container\"]//ul[@class=\"k-list k-reset\"]"));
            btnFilter = new Button(By.XPath("//button[text()=\"Filter\"]"));
            btnClear = new Button(By.XPath("//button[text()=\"Clear\"]"));
        }

        #region Main Actions
        public void ClearAllFilters()
        {
            DriverUtils.WaitForPageLoad();
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
            txtFilterValue.Click();
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
            DriverUtils.WaitForPageLoad();
            if (!string.IsNullOrEmpty(ID))
            {
                tblDashboardTable.ClickTableCell("ID", ID);
            }
        }

        public void OpenALogEntry(int rowIndex)
        {
            DriverUtils.WaitForPageLoad();
            tblDashboardTable.ClickTableCell(5, rowIndex);
        }

        public void DeleteALogEntry(string ID)
        {
            DriverUtils.WaitForPageLoad();
            int rowIndex = tblDashboardTable.GetTableRowIndex(5, ID);
            tblDashboardTable.ClickTableCell(13, rowIndex);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
        }

        public void PerformASearchCriteria(string community = null, string lastUpdatedFrom = null, string lastUpdatedTo = null, string filters = null)
        {
            DriverUtils.WaitForPageLoad();

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

            if (!string.IsNullOrEmpty(filters))
            {
                txtFilters.Click();
                ClearAllValueInCombobox("Filters");
                txtFilters.SendKeys(filters);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{Enter}");
            }

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
        #endregion Main Actions

        #region Check Points
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
                actualResult = tblDashboardTable.GetTableAllCellValueInColumn(columnName);
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
                for (int i = 0; i < tblDashboardTable.RowCount(); i++)
                {
                    result = result && tblDashboardTable.GetTableAllCellValueInRow(i).Contains(searchValue);
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

        public bool AreSearchCriteriasResetted()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;

            List<string> defaultCommunity = null;
            string defaultLastUpdatedFrom = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy");
            string defaultLastUpdatedTo = DateTime.Now.ToString("MM/dd/yyyy");
            List<string> defaultFilters = new List<string> { "Active" };

            txtCommunity.Click();
            List<string> actualCommunity = lstCommunity.GetSelectedOptions();

            txtLastUpdatedFrom.Click();
            string actualLastUpdatedForm = txtLastUpdatedFrom.GetText();

            txtLastUpdatedTo.Click();
            string actualLastUpdatedTo = txtLastUpdatedTo.GetText();

            txtFilters.Click();
            List<string> actualFilters = lstFilters.GetSelectedOptions();

            result = actualCommunity.SequenceEqual(defaultCommunity)
                  && actualLastUpdatedForm.Equals(defaultLastUpdatedFrom)
                  && actualLastUpdatedTo.Equals(defaultLastUpdatedTo)
                  && actualFilters.SequenceEqual(defaultFilters);

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
        #endregion Check Points
    }
}
