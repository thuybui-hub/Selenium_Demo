using InfectionLogAutomation.PageObject.Common;
using OpenQA.Selenium;
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
        public readonly ComboBox cbbCommunity;
        public readonly TextBox txtLastUpdatedFrom;
        public readonly TextBox txtLastUpdatedTo;
        public readonly ComboBox cbbFilters;
        public readonly Button btnSearch;
        public readonly Button btnReset;

        // ILog Table
        public readonly Button btnClearFilters;
        public readonly Button btnExportToExcel;
        public readonly Table tblILogTableHeader;
        public readonly Table tblILogTable;
        public readonly BaseElement divNoRecords;

        public HomePage()
        {
            // Search Criterias
            cbbCommunity = new ComboBox(By.Id("communityName"));
            txtLastUpdatedFrom = new TextBox(By.Id("datepickerFrom"));
            txtLastUpdatedTo = new TextBox(By.Id("datepickerTo"));
            cbbFilters = new ComboBox(By.Id("allFilters"));
            btnSearch = new Button(By.Id("search"));
            btnReset = new Button(By.ClassName("k-button btnClear"));

            // ILog Table
            btnClearFilters = new Button(By.Id("clearFilterButton"));
            btnExportToExcel = new Button(By.Id("excelExportButton"));
            tblILogTableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblILogTable = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
            divNoRecords = new BaseElement(By.ClassName("k-grid-norecords"));
        }

        #region Main Actions
        #endregion Main Actions

        #region Check Points

        public bool IsHomePageDisplayedAsDefault()
        {

            return true;
        }

        public bool IsILogTableDisplayed()
        {
            return true;
        }
        #endregion Check Points
    }
}
