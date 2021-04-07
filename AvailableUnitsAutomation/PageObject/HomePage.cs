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
    public class HomePage
    {
        #region Properties
        #endregion Properties

        #region Actions
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
        #endregion Actions

        #region Check points
        #endregion Check points
    }
}
