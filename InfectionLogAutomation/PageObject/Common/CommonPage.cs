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
            lblTitle = new Label(By.XPath("//h3"));

            // Table
            divNoRecords = new BaseElement(By.XPath("//div[@class=\"k-grid-norecords\"]"));

            lblRegionAndCommunity = new Label(By.XPath("//div[@id=\"logForm\"]//p"));
        }

        #region Main Action
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

        public bool IsUserAbleToAddNewRecords(string status, string typeOfRecords, string typeOfEntries)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            Link navigationItem = new Link(By.LinkText(typeOfRecords));
            lnkInfectiousOutbreakLog.ScrollToView();

            List<string> actualSubmenu, expectedSubMenu;
            switch (typeOfRecords)
            {
                case "New Log Entry":
                    navigationItem.Click();
                    switch (status)
                    {
                        case "able":
                            actualSubmenu = GetSubMenuItems(typeOfRecords);
                            result = navigationItem.IsDisplayed() && actualSubmenu.Contains(typeOfEntries);
                            break;
                        case "unable":
                            actualSubmenu = GetSubMenuItems(typeOfRecords);
                            result = navigationItem.IsDisplayed() && !actualSubmenu.Contains(typeOfEntries);
                            break;
                    }
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    break;
                case "Bulk Processing":
                    expectedSubMenu = new List<string>();
                    actualSubmenu = new List<string>();
                    if (typeOfEntries == "Team")
                    {
                        expectedSubMenu = new List<string>() { "Insert Team", "Edit Team" };
                    }
                    if (typeOfEntries == "Resident")
                    {
                        expectedSubMenu = new List<string>() { "Insert Resident", "Edit Resident" };
                    }

                    switch (status)
                    {
                        case "able":
                            navigationItem.Click();
                            actualSubmenu = GetSubMenuItems(typeOfRecords);
                            result = navigationItem.IsDisplayed() && expectedSubMenu.All(x => actualSubmenu.Contains(x));
                            System.Windows.Forms.SendKeys.SendWait("{Enter}");
                            break;
                        case "unable":
                            result = !navigationItem.IsDisplayed();
                            if (result==false)
                            {
                                navigationItem.Click();
                                actualSubmenu = GetSubMenuItems(typeOfRecords);
                                result = expectedSubMenu.All(x => !actualSubmenu.Contains(x));
                                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                            }
                            break;
                    }
                    break;
                default:
                    throw new Exception(string.Format("Status value is invalid."));
            }
            return result;
        }

        /// <summary>
        /// Check to if all items in a list equal a specific value
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
