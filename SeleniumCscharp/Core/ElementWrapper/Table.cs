using OpenQA.Selenium;


namespace SeleniumCSharp.Core.ElementWrapper
{
    /// <summary>
    ///     Declare functions for the element of type Table.
    /// </summary>
    public class Table : BaseElement
    {
        /// <summary>
        ///     Find a web element of type Table using By locator.
        /// </summary>
        public Table(By locator)
            : base(locator)
        {
        }


        /// <summary>
        ///     Find a web element of type Table using By Xpath locator.
        /// </summary>
        public Table(string locator)
            : base(locator)
        {
        }             
    }
}