using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type TextArea.
    /// </summary>
    public class TextArea : BaseElement
    {
        /// <summary>
        ///     Find a web element of type TextArea using By locator.
        /// </summary>
        public TextArea(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type TextArea using By Xpath locator.
        /// </summary>
        public TextArea(string locator)
            : base(locator)
        {
        }
    }
}
