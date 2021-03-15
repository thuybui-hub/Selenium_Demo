using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type Div.
    /// </summary>
    public class Div : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Ul using By locator.
        /// </summary>
        public Div(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type tr using By Xpath locator.
        /// </summary>
        public Div(string locator)
            : base(locator)
        {
        }        
    }
}