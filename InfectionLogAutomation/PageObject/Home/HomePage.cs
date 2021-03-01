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
        public readonly TextBox txtLastUpdatedFrom;
        public readonly TextBox txtLastUpdatedTo;
        public readonly TextBox txtFilters;
        public readonly ComboBox cbbFilters;
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

        public HomePage()
        {
            // Search Criterias
            txtCommunity = new TextBox(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input"));
            cbbCommunity = new ComboBox(By.XPath("//select[@id=\"communityName\"]"));
            txtLastUpdatedFrom = new TextBox(By.Id("datepickerFrom"));
            txtLastUpdatedTo = new TextBox(By.Id("datepickerTo"));
            txtFilters = new TextBox(By.XPath("//ul[@id=\"allFilters_taglist\"]//following-sibling::input"));
            cbbFilters = new ComboBox(By.XPath("//select[@id=\"allFilters\"]"));
            btnSearch = new Button(By.Id("search"));
            btnReset = new Button(By.XPath("//button[@class=\"k-button btnClear\"]"));

            // ILog Table
            btnClearFilters = new Button(By.Id("clearFilterButton"));
            btnExportToExcel = new Button(By.Id("excelExportButton"));
            txtSearch = new TextBox(By.XPath("//span[@class=\"k-textbox k-grid-search k-display-flex\"]/input"));
            divDashboardTable = new BaseElement(By.Id("logGrid"));
            tblDashboardTableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblDashboardTable = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
            divNoRecords = new BaseElement(By.ClassName("k-grid-norecords"));
        }

        #region Main Actions
        #endregion Main Actions
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
        #region Check Points

        public bool IsHomePageDisplayed()
        {
            DriverUtils.WaitForPageLoad();
            return txtCommunity.IsDisplayed()
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
        #endregion Check Points
    }
}
