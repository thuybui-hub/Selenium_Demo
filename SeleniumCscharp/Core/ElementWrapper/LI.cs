using OpenQA.Selenium;


namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type row.
    /// </summary>
    public class LI : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Tr using By locator.
        /// </summary>
        public LI(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type tr using By Xpath locator.
        /// </summary>
        public LI(string locator)
            : base(locator)
        {
        }
    }
}