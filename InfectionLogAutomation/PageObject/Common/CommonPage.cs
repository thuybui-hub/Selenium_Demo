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
        
        public CommonPage()
        {
            // Main menu
            lnkInfectiousOutbreakLog = new Link(By.LinkText("Infectious Outbreak Log"));
            
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

            Link menuItem = new Link(By.LinkText(menuItemName));
            menuItem.Click();

            BaseElement subMenuItems = new BaseElement(By.XPath("//a[contains(text(), '" + menuItemName + "')]//following-sibling::ul//a"));
            List<BaseElement> test= subMenuItems.GetListElements();

            DriverUtils.WaitForPageLoad();

            foreach (BaseElement x in test)
            {
                listSubMenuItems.Add(x.GetText());
            }

            return listSubMenuItems;
        }
        #endregion Main Action

        #region Check Points
        public bool IsInfectionLogPageDiplayed()
        {
            return lnkInfectiousOutbreakLog.IsDisplayed();
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

        public bool IsHomeDefaultPage()
        {
            return true;
        }
        #endregion Check Points
    }
}
