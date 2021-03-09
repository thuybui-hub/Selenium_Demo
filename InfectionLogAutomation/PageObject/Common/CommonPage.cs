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

namespace InfectionLogAutomation.PageObject.Common
{
    public class CommonPage
    {
        // Main menu
        public readonly Link lnkInfectiousOutbreakLog;        

        // Paging
        public readonly Span spnPaging;

        // Page title
        public readonly Label lblTitle;

        // Alert window
        public IAlert alertWin;

        public CommonPage()
        {
            // Main menu
            lnkInfectiousOutbreakLog = new Link(By.LinkText("Infectious Outbreak Log"));

            // Paging
            spnPaging = new Span(By.XPath("//span[@class=\"k-pager-sizes k-label\"]//span[@class=\"k-input\"]"));

            // Page title
            lblTitle = new Label(By.XPath("//div[@id=\"logForm\"]//h3"));
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
            spnPaging.MoveToElement();
            spnPaging.Click();
            System.Windows.Forms.SendKeys.SendWait("All");
        }


        public string GetAlertWinText()
        {
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
        #endregion Main Action

        #region Check Points
        /// <summary>
        /// Check to see if the alert windown pop-up shows
        /// </summary>
        /// <returns></returns>
        public bool IsAlertPresent()
        {
            bool isPresent = false;
            try
            {
                alertWin = DriverUtils.GetDriver().SwitchTo().Alert();
                isPresent = true;
            }
            catch (NoAlertPresentException ex)
            {
            }

            return isPresent;
        }

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
        
        #endregion Check Points
    }
}
