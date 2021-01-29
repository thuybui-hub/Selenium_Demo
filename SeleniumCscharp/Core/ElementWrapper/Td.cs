using OpenQA.Selenium;


namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type column.
    /// </summary>
    public class Td : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Td using By locator.
        /// </summary>
        public Td(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type td using By Xpath locator.
        /// </summary>
        public Td(string locator)
            : base(locator)
        {
        }
    }
}