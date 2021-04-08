using AvailableUnitsAutomation.PageObject;
using AvailableUnitsAutomation.Utilities;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.PageObject
{
    public class SearchPage : CommonPage
    {
        #region Properties
        // Search criteria
        public readonly Span spnCommunity;
        public readonly Span spnStatus;
        public readonly Span spnChangeType;
        public readonly Span spnLob;
        public readonly Span spnEffectiveDate;
        public readonly Button btnSearch;

        // Search result table
        public readonly Link lnkExportToExcel;
        public readonly Table tblSearchResultHeader;
        public readonly Table tblSearchResult;
        #endregion Properties

        public SearchPage()
        {
            // Search criteria
            spnCommunity = new Span(By.XPath("//label[@id=\"communityName_label\"]//following-sibling::span"));
            spnStatus = new Span(By.XPath("//label[@id=\"status_label\"]//following-sibling::span"));
            spnChangeType = new Span(By.XPath("//label[@id=\"changetypes_label\"]//following-sibling::span"));
            spnLob = new Span(By.XPath("//label[@id=\"lob_label\"]//following-sibling::span"));
            spnEffectiveDate = new Span(By.XPath("//label[@id=\"requestDate_label\"]//following-sibling::span"));
            btnSearch = new Button(By.XPath("//button[@class=\"btn btn-primary\"]"));

            // Search result table
            lnkExportToExcel = new Link(By.XPath("//a[@class=\"k-button k-button-icontext k-grid-excel\"]"));
            tblSearchResultHeader = new Table(By.XPath("//div[@id=\"searchresultgrid\"]//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblSearchResult = new Table(By.XPath("//div[@id=\"searchresultgrid\"]//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
        }

        #region Actions
        /// <summary>
        ///     Fill search fields
        /// </summary>
        /// <param name="communityName"></param>
        /// <param name="status"></param>
        /// <param name="changeType"></param>
        /// <param name="lob"></param>
        /// <param name="effectiveDate"></param>
        public void FillSearchFields(string communityName = null, string status = null, string changeType = null, string lob = null, string effectiveDate = null)
        {
            DriverUtils.WaitForPageLoad();

            if (!string.IsNullOrEmpty(communityName))
            {
                spnCommunity.Click();
                lstBoxCommunityName.SelectOptionByText(communityName);
            }

            if (!string.IsNullOrEmpty(status))
            {
                spnStatus.Click();
                lstBoxStatus.SelectOptionByText(status);
            }

            if (!string.IsNullOrEmpty(changeType))
            {
                spnChangeType.Click();
                lstBoxChangeTypes.SelectOptionByText(changeType);
            }

            if (!string.IsNullOrEmpty(lob))
            {
                spnLob.Click();
                lstBoxLob.SelectOptionByText(lob);
            }

            if (!string.IsNullOrEmpty(effectiveDate))
            {
                spnEffectiveDate.Click();
                lstBoxEffectiveDate.SelectOptionByText(effectiveDate);
            }

        }

        /// <summary>
        ///     Click on Search button
        /// </summary>
        public void SearchRequests()
        {
            DriverUtils.WaitForPageLoad();
            btnSearch.Click();
        }

        /// <summary>
        ///     Fill Search info and click on Search button
        /// </summary>
        public void PerformASearch(string communityName = null, string status = null, string changeType = null, string lob = null, string effectiveDate = null)
        {
            FillSearchFields(communityName, status, changeType, lob, effectiveDate);
            SearchRequests();
        }
        #endregion Actions

        #region Check points
        /// <summary>
        ///     Check if page UI displays correctly or not
        /// </summary>
        /// <returns></returns>
        public bool DoesUIDisplayCorrectly()
        {
            DriverUtils.WaitForPageLoad();

            bool result = true;

            result = spnCommunity.IsDisplayed()
                && spnStatus.IsDisplayed()
                && spnChangeType.IsDisplayed()
                && spnLob.IsDisplayed()
                && spnEffectiveDate.IsDisplayed()
                && btnSearch.IsDisplayed()
                && divSearchResultGrid.IsDisplayed();

            return result;
        }

        /// <summary>
        ///     Check if page UI with format displays correctly or not
        /// </summary>
        /// <returns></returns>
        public bool DoesUIWithFormatDisplayCorrectly()
        {
            DriverUtils.WaitForPageLoad();

            bool result = true;
            
            result = spnCommunity.GetAttribute("role").Equals("listbox")
                && spnStatus.GetAttribute("role").Equals("listbox")
                && spnChangeType.GetAttribute("role").Equals("listbox")
                && spnLob.GetAttribute("role").Equals("listbox")
                && spnEffectiveDate.GetAttribute("role").Equals("listbox");

            return result;
        }

        /// <summary>
        ///     Check if search result table shows the correct columns
        /// </summary>
        /// <returns></returns>
        public bool DoesSearchResultTableShowColumnsCorrectly()
        {
            DriverUtils.WaitForPageLoad();

            List<string> expectedColumns = new List<string>() { "View", "Community Name", "Effective Date", "Unit Number", "Line of Business", "Change Type", "Submitter", "Status" };
            List<string> actualColumns = tblSearchResultHeader.GetAllColumnsHeader();

            return actualColumns.SequenceEqual(expectedColumns);
        }

        /// <summary>
        ///     Check if serach result displays correctly or not
        /// </summary>
        /// <param name="communityName"></param>
        /// <param name="status"></param>
        /// <param name="changeType"></param>
        /// <param name="lob"></param>
        /// <param name="effectiveDate"></param>
        /// <returns></returns>
        public bool DoesSearchResultDisplayCorrectly(string communityName = null, string status = null, string changeType = null, string lob = null, string effectiveDate = null)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            List<string> actualList;

            if (divNoRecords.IsDisplayed() == false)
            {
                SelectPagingValue("All", "Search");

                if (!string.IsNullOrEmpty(communityName))
                {
                    actualList = tblSearchResult.GetTableAllCellValueInColumn(1);
                    result = result & actualList.All(x => x.Equals(communityName));
                }

                if (!string.IsNullOrEmpty(status))
                {
                    actualList = tblSearchResult.GetTableAllCellValueInColumn(7);
                    result = result & actualList.All(x => x.Equals(status));
                }

                if (!string.IsNullOrEmpty(changeType))
                {
                    actualList = tblSearchResult.GetTableAllCellValueInColumn(5);
                    result = result & actualList.All(x => x.Equals(changeType));
                }

                if (!string.IsNullOrEmpty(lob))
                {
                    actualList = tblSearchResult.GetTableAllCellValueInColumn(4);
                    result = result & actualList.All(x => x.Equals(lob));
                }

                if (!string.IsNullOrEmpty(effectiveDate))
                {
                    actualList = tblSearchResult.GetTableAllCellValueInColumn(2);
                    result = result & actualList.All(x => x.Equals(effectiveDate));
                }
            }
            else result = true;
            return result;
        }
        #endregion Check points
    }
}
