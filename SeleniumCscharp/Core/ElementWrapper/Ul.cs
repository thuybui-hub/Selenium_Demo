using OpenQA.Selenium;


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
    }
}