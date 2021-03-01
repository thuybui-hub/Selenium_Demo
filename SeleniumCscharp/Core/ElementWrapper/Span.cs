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
    }
}
