using OpenQA.Selenium;
using SeleniumCSharp.Core.ElementWrapper;
using SeleniumCSharp.Core.DriverWrapper;
using InfectionLogAutomation.Utilities;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace InfectionLogAutomation.PageObject.Common
{
    public class CommonPage
    {
        // Main menu
        public Link lnkInfectiousOutbreakLog;        

        // Paging
        public Span spnPaging;

        // Page title
        public Label lblTitle, lblRegionAndCommunity;

        // Alert window
        public IAlert alertWin;

        // Table
        public BaseElement divNoRecords;

        public CommonPage()
        {
            // Main menu
            lnkInfectiousOutbreakLog = new Link(By.LinkText("Infectious Outbreak Log"));

            // Paging
            spnPaging = new Span(By.XPath("//span[@class=\"k-pager-sizes k-label\"]//span[@class=\"k-input\"]"));

            // Page title
            lblTitle = new Label(By.XPath("//div[@id=\"logForm\"]//h3"));

            // Table
            divNoRecords = new BaseElement(By.XPath("//div[@class=\"k-grid-norecords\"]"));

            lblRegionAndCommunity = new Label(By.XPath("//div[@id=\"logForm\"]//p"));
        }

        #region Main Action
        public void SelectMenuItem(string path)
        {
            DriverUtils.WaitForPageLoad();
            Link menuItem, subMenuItem;
            string[] items = path.Split('|');
            switch (items.Length)
            {
                case 1:
                    menuItem = new Link(By.LinkText(items[0]));
                    menuItem.Click();
                    break;
                case 2:
                    menuItem = new Link(By.LinkText(items[0]));
                    menuItem.Click();
                    subMenuItem = new Link(By.LinkText(items[1]));
                    subMenuItem.Click();
                    break;
            }
        }

        public List<string> GetSubMenuItems(string menuItemName)
        {
            DriverUtils.WaitForPageLoad();
            List<string> listSubMenuItems = new List<string> { };
            Ul menuItem = new Ul(By.XPath("//a[contains(text(), \"" + menuItemName + "\")]//following-sibling::ul"));
            listSubMenuItems = menuItem.GetOptions();
            return listSubMenuItems;
        }

        public void ShowAllILogRecords()
        {
            DriverUtils.WaitForPageLoad();
            spnPaging.ScrollToView();
            spnPaging.Click();
            System.Windows.Forms.SendKeys.SendWait("All");
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
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

            //if (!string.IsNullOrEmpty(alertMsg))
            //{
            //    alertWin.Accept();
            //}
            if (ExpectedConditions.AlertIsPresent() != null)
                alertWin.Accept();           
            
        }
        #endregion Main Action

        #region Check Points
        /// <summary>
        /// Check to see if the alert windown pop-up shows
        /// </summary>
        /// <returns></returns>
        //public bool IsAlertPresent()
        //{
        //    bool isPresent = false;
        //    try
        //    {
        //        alertWin = DriverUtils.GetDriver().SwitchTo().Alert();
        //        isPresent = true;
        //    }
        //    catch (NoAlertPresentException ex)
        //    {
        //    }

        //    return isPresent;
        //}

        /// <summary>
        /// Check to see if a form displays via form's title
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public bool CheckPageExist(string pageTitle)
        {
            DriverUtils.WaitForPageLoad(3);
            string title = lblTitle.GetText();
            return title.Contains(pageTitle);
        }
        
        public bool AreThere5TabsOnMainMenu()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            Link navigationItem;
            string[] elements = new string[] { "Home", "New Log Entry", "Bulk Processing", "Reports", "User Guide" };

            foreach(string x in elements)
            {
                navigationItem = new Link(By.LinkText(x));
                result = result & navigationItem.IsDisplayed();
            }
            return result;
        }

        public bool IsReadOnlyUserAbleToAddNewRecords(string status)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            Link navigationItem = new Link(By.LinkText(status));
            lnkInfectiousOutbreakLog.ScrollToView();
            switch (status)
            {
                case "New Log Entry":
                    navigationItem.Click();
                    List<string> expectedSubMenu = null;
                    List<string> actualSubmenu = GetSubMenuItems("New Log Entry");
                    result = navigationItem.IsDisplayed() & (actualSubmenu == expectedSubMenu);
                    break;
                case "Bulk Processing":
                    result = !navigationItem.IsDisplayed();
                    break;
                default:
                    throw new Exception(string.Format("Status value is invalid."));
            }
            return result;
        }
        
        #endregion Check Points
    }
}
