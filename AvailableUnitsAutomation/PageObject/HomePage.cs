using AvailableUnitsAutomation.PageObject;
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
    public class HomePage : CommonPage
    {
        #region Properties
        public readonly Table tblActiveRequestsHeader;
        public readonly Table tblActiveRequests;
        public readonly Table tblMyPendingListHeader;
        public readonly Table tblMyPendingList;
        #endregion Properties

        public HomePage()
        {
            tblActiveRequestsHeader = new Table(By.XPath("//div[@id=\"pendingListgrid\"]//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblActiveRequests = new Table(By.XPath("//div[@id=\"pendingListgrid\"]//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
            tblMyPendingListHeader = new Table(By.XPath("//div[@id=\"activeListgrid\"]//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblMyPendingList = new Table(By.XPath("//div[@id=\"activeListgrid\"]//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
        }

        #region Actions
        
        #endregion Actions

        #region Check points
        #endregion Check points
    }
}
