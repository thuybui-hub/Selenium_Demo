using OpenQA.Selenium;
using SeleniumCSharp.Core.ElementWrapper;
using SeleniumCSharp.Core.DriverWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using AvailableUnitsAutomation.Utilities;

namespace AvailableUnitsAutomation.PageObject
{
    public class CommonPage
    {
        // Paging
        public Span spnPaging;

        // Alert window
        public IAlert alertWin;

        // Table
        public BaseElement divNoRecords;

        // New Request and Search Page
        public readonly Ul lstBoxEffectiveDate;
        public readonly Ul lstBoxCommunityName;
        public readonly Ul lstBoxStatus;
        public readonly Ul lstBoxChangeTypes;
        public readonly Ul lstBoxLob;

        // Dashboard and Search page
        public Span spnSortAsc;
        public Span spnSortDesc;

        // Dashboard page
        public readonly Div divActiveListGrid;
        public readonly Div divPendingListGrid;

        // Search result table
        public readonly Div divSearchResultGrid;
        public readonly Span spnCommunityLoadingIcon;
        
        public CommonPage()
        {            
            // Table
            divNoRecords = new BaseElement(By.XPath("//div[@class=\"k-grid-norecords\"]"));

            // New Request and Search Page
            lstBoxEffectiveDate = new Ul(By.Id("requestDate_listbox"));
            lstBoxCommunityName = new Ul(By.Id("communityName_listbox"));
            lstBoxStatus = new Ul(By.Id("status_listbox"));
            lstBoxChangeTypes = new Ul(By.Id("changetypes_listbox"));
            lstBoxLob = new Ul(By.Id("lob_listbox"));

            // Dashboard page
            divActiveListGrid = new Div(By.Id("activeListgrid"));
            divPendingListGrid = new Div(By.Id("pendingListgrid"));

            // Search page
            divSearchResultGrid = new Div(By.Id("searchresultgrid"));
            //communityLoadingIcon = browser.FindContain<HtmlSpan>(new { Class = "k-i-loading" });
        }

        #region Main Action
        /// <summary>
        ///     Go to Create New Request or Search page
        /// </summary>
        /// <param name="linkText"> option = Create New Change Request / Advanced Search </param>
        public void GoToPage(string linkText)
        {
            DriverUtils.WaitForPageLoad();
            Link link = new Link(By.LinkText(linkText));
            link.Click();
        }

        /// <summary>
        ///     Open a request by Request ID
        /// </summary>
        /// <param name="requestID"></param>
        public void OpenARequest(string requestID)
        {
            DriverUtils.GoToUrl(Constants.Url + "/NewAvailableUnits.aspx?requestid=" + requestID);
            DriverUtils.WaitForPageLoad();
        }

        /// <summary>
        ///     Open a request by Row Index
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowIndex"></param>
        public void OpenARequest(Table table, int rowIndex)
        {
            DriverUtils.WaitForPageLoad();
            table.ClickTableCell(0, rowIndex);
            DriverUtils.WaitForPageLoad();
        }

        /// <summary>
        /// Select paging value under ADR table or Appeal table
        /// </summary>
        /// <param name="pagingValue"> pagingValue = 25, 50, 100, 150, 200, All </param>
        /// <param name="tableName">tableName = Active Requests/My Pending List/Search</param>
        public void SelectPagingValue(string pagingValue, string tableName)
        {
            switch (tableName)
            {
                case "Active Requests":
                    spnPaging = new Span(By.XPath("//div[@id=\"activeListgrid\"]//span[@class=\"k-input\"]"));
                    break;
                case "My Pending List":
                    spnPaging = new Span(By.XPath("//div[@id=\"pendingListgrid\"]//span[@class=\"k-input\"]"));
                    break;
                case "Search":
                    spnPaging = new Span(By.XPath("//div[@id=\"searchresultgrid\"]//span[@class=\"k-input\"]"));
                    break;
                default:
                    throw new Exception(string.Format("Table Name is invalid"));

            }
            spnPaging.ScrollToView();
            spnPaging.Click();
            System.Windows.Forms.SendKeys.SendWait(pagingValue);
            System.Windows.Forms.SendKeys.SendWait("{enter}");
            DriverUtils.WaitForPageLoad();
        }

        /// <summary>
        /// Show all records in selected table
        /// </summary>
        /// <param name="tableName">tableName = Active Requests/My Pending List/Search</param>
        public void ShowAllRecords(string tableName)
        {
            SelectPagingValue("All", tableName);
        }

        /// <summary>
        /// Sort a table column, default is asc
        /// </summary>
        /// <param name="tableName"> tableName = activeRequestsHeaderTbl, myPendingListHeaderTbl, searchResultHeaderTbl</param>
        /// <param name="columnName"></param>
        /// <param name="option"> option = asc/desc, default = asc </param>
        public void SortTableColumn(Table table, string columnName, string sortBy = "asc")
        {
            DriverUtils.WaitForPageLoad();

            int columnIndex = table.GetTableColumnIndex(columnName);
            //HtmlControl columnHeader = table.GetCell(0, columnIndex);

            spnSortAsc = new Span(By.XPath("//span[@class=\"k-icon k-i-sort-asc-sm\"]"));
            spnSortDesc = new Span(By.XPath("//span[@class=\"k-icon k-i-sort-desc-sm\"]"));

            //switch (sortBy)
            //{
            //    case "asc":
            //        if (spnSortAsc.IsDisplayed()) { break; }
            //        else if (spnSortDesc.IsDisplayed()) { columnHeader.DoubleClick(); }
            //        else { columnHeader.Click(); }
            //        break;
            //    case "desc":
            //        if (spnSortAsc.IsDisplayed()) { columnHeader.Click(); }
            //        else if (spnSortDesc.IsDisplayed()) { break; }
            //        else { columnHeader.DoubleClick(); }
            //        break;
            //    default:
            //        throw new Exception(string.Format("Option is invalid."));
            //}
        }


        public void WriteList(List<string> list)
        {
            System.Console.WriteLine("These are values in the list");
            foreach (string item in list)
                System.Console.WriteLine(item);
        }

        public void FillValue(BaseElement element, string value)
        {
            if (element.IsDisplayed())
            {
                element.Click();
                //System.Windows.Forms.SendKeys.SendWait("^a");
                //DriverUtils.wait(1);
                //System.Windows.Forms.SendKeys.SendWait(value);
                element.SendKeys(value);
                DriverUtils.wait(1);
                System.Windows.Forms.SendKeys.SendWait("{enter}");
            }
        }

        public string GetAlertWinText()
        {
            DriverUtils.wait(1);
            IAlert alertWin = DriverUtils.GetDriver().SwitchTo().Alert();
            return alertWin.Text;
        }

        public void ClearAllValueInCombobox(string field)
        {
            Span spnClearIcon = new Span(By.XPath("//label[text()=\""+ field +"\"]//following-sibling::div//span[@class=\"k-icon k-clear-value k-i-close\"]"));
            if (spnClearIcon.IsDisplayed())
            {
                spnClearIcon.Click();
            }

        }

        public void CloseAlertPopup()
        {
            DriverUtils.wait(1);
            IAlert alertWin = DriverUtils.GetDriver().SwitchTo().Alert();

            string alertMsg = alertWin.Text;
            
            if (ExpectedConditions.AlertIsPresent() != null)
                alertWin.Accept();           
            
        }
        #endregion Main Action

        #region Check Points
        /// <summary>
        /// Check to see if all items in a list equal a specific value
        /// </summary>
        /// <param name="list">The list needs to be checked</param>
        /// <param name="value">Value to check</param>
        /// <returns></returns>
        public bool DoAllListItemEqualValue(List<string> list, string value)
        {
            return list.TrueForAll(i => i.Equals(value));
        }

        /// <summary>
        /// Check to see if an alert presents
        /// </summary>
        /// <returns></returns>
        public bool isAlertPresent()
        {
            try
            {
                DriverUtils.GetDriver().SwitchTo().Alert();                
                return true;
            }
            
            catch (NoAlertPresentException Ex)
            {
                return false;
            } 
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
