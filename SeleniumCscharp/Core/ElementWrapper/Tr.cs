using OpenQA.Selenium;


namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type row.
    /// </summary>
    public class Tr : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Tr using By locator.
        /// </summary>
        public Tr(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type tr using By Xpath locator.
        /// </summary>
        public Tr(string locator)
            : base(locator)
        {
        }
    }
}