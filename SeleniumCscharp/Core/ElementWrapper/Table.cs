using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;

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

        /// <summary>
        /// Get text in cell control
        /// </summary>
        /// <param name="cellControl"></param>
        /// <returns>String</returns>
        public string GetTextInCellControl(Td cellControl)
        {
            string text = cellControl.GetText();
            return text.Trim();
        }

        /// <summary>
        /// Get table cell value
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public string GetTableCellValue(Table table, int columnIndex, int rowIndex)
        {
            table.WaitForVisible();
            IWebElement tbl = table.GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            List<IWebElement> tds = new List<IWebElement>(trs[rowIndex].FindElements(By.TagName("td")));
            IWebElement td = tds[columnIndex];
            return td.Text;
        }

        /// <summary>
        /// Get table column index
        /// </summary>
        /// <param name="tableHeader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetTableColumnIndex(Table tableHeader, string columnName)
        {
            int columnIndex = 0;
            tableHeader.WaitForVisible();
            IWebElement tbl = tableHeader.GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            List<IWebElement> ths = new List<IWebElement>(trs[0].FindElements(By.TagName("th")));

            for (int i = 0; i<ths.Count; i++)
            {
                List<IWebElement> links = new List<IWebElement>(ths[i].FindElements(By.TagName("a")));
                IWebElement column = links[1];
                
                if ((!string.IsNullOrWhiteSpace(column.Text)) && (column.Text.Equals(columnName)))
                {
                    columnIndex = i;
                    break;
                }
                else columnIndex = - 1;
            }
            return columnIndex;
        }

        /// <summary>
        /// Get table row index
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnIndex"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetTableRowIndex(Table table, int columnIndex, string content)
        {
            int rowIndex = 0;
            IWebElement tbl = table.GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));

            for (int i = 0; i<trs.Count; i++)
            {
                List<IWebElement> tds = new List<IWebElement>(trs[i].FindElements(By.TagName("td")));
                IWebElement td = tds[columnIndex];

                if ((!string.IsNullOrWhiteSpace(td.Text)) && (td.Text.Equals(content)))
                {
                    rowIndex = i;
                    break;
                }
                else rowIndex = -1;
            }
            return 0;
        }

    }
}