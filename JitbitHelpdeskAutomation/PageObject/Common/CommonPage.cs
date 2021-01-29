using JitbitHelpdeskAutomation.Utilities;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System;

namespace JitbitHelpdeskAutomation.PageObject.Common
{
    public class CommonPage
    {
        public Link lnkTabMenu;
        public Link lnkSubTabMenu;
        public Link lnkCategory;
        public Link lnkAllCategories;
        public Button btnUserMenu;
        public Link lnkUserMenuOption;
        public Link lnkSearchForUser;
        public TextBox txtSearchForUser;
        private OpenQA.Selenium.IWebElement iframeLogin;
        public Label lblServiceNowTab;

        public Button btnButton;
        public Link lnkLink;

        public TextBox txtSearch;

        public CommonPage()
        {
            btnButton = new Button("//button[contains(text(), '{0}')]");
            lnkLink = new Link("//a[contains(text(), '{0}')]");
                        
            txtSearch = new TextBox(By.XPath("//div[@class='divSearch']//input"));
            lnkTabMenu = new Link("//ul[@class='tabmenu']//a[contains(text(),'{0}')]");
            lnkSubTabMenu = new Link("//ul[@class='tabmenu2']//*[contains(text(),'{0}')]");
            lnkCategory = new Link("//div[@id='leftsidebar']//a[span[text()='{0}']]");
            lnkAllCategories = new Link(By.XPath("//div[@id='leftsidebar']//a[contains(text(),'All categories')]"));
            btnUserMenu = new Button(By.Id("user_info_dropdown"));
            lnkUserMenuOption = new Link("//ul[@class='dropdown-menu']//a[text()='{0}']");
            lnkSearchForUser = new Link(By.XPath("//a[span[text()='Search for user']]"));
            txtSearchForUser = new TextBox(By.Id("s2id_autogen2_search"));
            lblServiceNowTab = new Label("//span[@class='tab_caption_text' and text()='{0}']");
        }
        public void SelectTabMenu(string tabName = null)
        {
            lnkTabMenu.Format(tabName);
            lnkTabMenu.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void SelectSubTabMenu(string subTabName = null)
        {
            lnkSubTabMenu.Format(subTabName);
            lnkSubTabMenu.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void SelectTicketCatagory(string categoryName = null)
        {
            lnkCategory.Format(categoryName);
            lnkCategory.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void SelectAllTicketCatagories()
        {
            lnkAllCategories.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void SwitchToMainIFrame()
        {
            iframeLogin = DriverUtils.GetDriver().FindElement(By.Id("gsft_main"));
            DriverUtils.SwitchToIframe(iframeLogin);
        }

        public void ImpersionateUser(string user = null)
        {
            btnUserMenu.WaitForVisible();
            btnUserMenu.ClickByJs();
            lnkUserMenuOption.Format("Impersonate User");
            lnkUserMenuOption.ClickByJs();
            DriverUtils.wait();
            DriverUtils.SwitchToNewWindow();
            lnkSearchForUser.ClickAndHold();
            DriverUtils.wait(2);
            System.Windows.Forms.SendKeys.SendWait(user);
            DriverUtils.wait(2);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            DriverUtils.wait();
            DriverUtils.WaitForPageLoad();
        }
        
        public void OpenNewIncidentPage()
        {
            DriverUtils.GoToUrl(Constants.SNUrl + "/nav_to.do?uri=%2Fincident.do");
            DriverUtils.WaitForPageLoad();
        }

        public void OpenIncidentListPage()
        {
            DriverUtils.GoToUrl(Constants.SNUrl + "/nav_to.do?uri=%2Fincident_list.do");
            DriverUtils.WaitForPageLoad();
        }

        public void SearchFor(string searchName = null)
        {
            txtSearch.SendKeys(searchName);
        }

        public void ClickAButton(string buttonName, string browser = null)
        {
            var clickAction = new Actions(DriverUtils.GetDriver());
            clickAction.ClickAndHold(btnButton.GetElement());
            Thread.Sleep(1);
            clickAction.Release().Perform();
            DriverUtils.WaitForPageLoad();
        }

        public void ClickALink(string linkName)
        {
            lnkLink.Format(linkName);
            lnkLink.Click();
            DriverUtils.WaitForPageLoad();
        }

        public void NavigateToTicketDetailsPage(string ticketID)
        {
            try
            {
                if (!string.IsNullOrEmpty(ticketID))
                {

                    string url = Constants.Url + "Ticket/" + ticketID;
                    DriverUtils.GoToUrl(url);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectServiceNowTab(string tabName)
        {
            lblServiceNowTab.Format(tabName);
            lblServiceNowTab.WaitForVisible();
            lblServiceNowTab.ClickByJs();
        }
    }
}
