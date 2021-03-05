using OpenQA.Selenium;
using System.Collections.Generic;

namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type Ul.
    /// </summary>
    public class Ul : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Ul using By locator.
        /// </summary>
        public Ul(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type tr using By Xpath locator.
        /// </summary>
        public Ul(string locator)
            : base(locator)
        {
        }

        /// <summary>
        /// Return the inner text of all options in the element of type combobox.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOptions()
        {
            List<string> displayedList = new List<string> { };
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));

            foreach (IWebElement item in listElements)
            {
                displayedList.Add(item.Text);
            }

            return displayedList;
        }
    }
}