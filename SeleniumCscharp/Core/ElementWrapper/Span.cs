using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type Span.
    /// </summary>
    public class Span : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Span using By locator.
        /// </summary>
        public Span(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type Span using By Xpath locator.
        /// </summary>
        public Span(string locator)
            : base(locator)
        {
        }

        /// <summary>
        /// Return the inner text of all item with the same span symbol 
        /// </summary>
        /// <returns></returns>
        public List<string> GetItems()
        {
            List<string> displayedList = new List<string> { };
            List<IWebElement> listElements = GetElements();

            foreach (IWebElement item in listElements)
            {
                if (item.Displayed)
                {
                    displayedList.Add(item.Text.Trim());
                }
            }

            return displayedList;
        }
    }
}
