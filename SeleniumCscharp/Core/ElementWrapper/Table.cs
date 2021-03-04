using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using System.Drawing;
using System.IO;
using System;

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
        public string GetTextInCellControl(IWebElement cellControl)
        {
            string text = cellControl.Text;
            return text.Trim();
        }

        /// <summary>
        /// Get table cell value
        /// </summary>        
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public string GetTableCellValue(int columnIndex, int rowIndex)
        {            
            IWebElement tbl = GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            List<IWebElement> tds = new List<IWebElement>(trs[rowIndex].FindElements(By.TagName("td")));
            IWebElement td = tds[columnIndex];
            return td.Text;
        }

        /// <summary>
        /// Get table column index
        /// </summary>        
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetTableColumnIndex(string columnName)
        {
            int columnIndex = 0;            
            IWebElement tbl = GetElement();

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
        /// <param name="columnIndex"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetTableRowIndex(int columnIndex, string content)
        {
            int rowIndex = 0;
            IWebElement tbl = GetElement();

            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));

            for (int i = 0; i<trs.Count; i++)
            {
                List<IWebElement> tds = new List<IWebElement>(trs[i].FindElements(By.TagName("td")));
                if (tds.Count > columnIndex)
                {
                    IWebElement td = tds[columnIndex];

                    if ((!string.IsNullOrWhiteSpace(td.Text)) && (td.Text.Equals(content)))
                    {
                        rowIndex = i;
                        break;
                    }
                    else rowIndex = -1;
                }
            }
            return rowIndex;
        }

        /// <summary>
        /// Get table all cell value in row
        /// </summary>        
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public List<string> GetTableAllCellValueInRow(int rowIndex = 0)
        {            
            List<string> list = new List<string>() { };
            IWebElement tbl = GetElement();

            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            List<IWebElement> tds = new List<IWebElement>(trs[rowIndex].FindElements(By.TagName("td")));

            for (int i = 0; i < tds.Count; i++)
            {
                string temp = GetTableCellValue(i, rowIndex);
                if (string.IsNullOrEmpty(temp))
                    list.Add("null");
                else list.Add(temp.Trim());
            }
            return list;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllColumnsHeader()
        {
            List<string> list = new List<string>() { };
            IWebElement tbl = GetElement();
            List<IWebElement> tr = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            List<IWebElement> th = new List<IWebElement>(tr[0].FindElements(By.TagName("th")));            

            for (int i = 0; i < th.Count; i++)
            {                
                if (string.IsNullOrEmpty(th[i].Text))
                    list.Add("null");
                else list.Add(th[i].Text.Trim());
            }

            return list;
        }

        /// <summary>
        /// Get table all cell value in column
        /// </summary>        
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<string> GetTableAllCellValueInColumn( string columnName)
        {
            List<string> list = new List<string>() { };
            Table tableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            IWebElement tbl = GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            int columnIndex = GetTableColumnIndex(columnName);

            for(int i = 0; i<trs.Count; i++)
            {
                string temp = GetTableCellValue(columnIndex, i);
                if (string.IsNullOrEmpty(temp))
                    list.Add("null");
                else list.Add(temp.Trim());
            }
            return list;
        }

        /// <summary>
        /// Click table cell
        /// </summary>
        /// <param name="trs"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public void ClickTableCell(List<IWebElement> trs, int columnIndex, int rowIndex)
        {
            List<IWebElement> tds = new List<IWebElement>(trs[rowIndex].FindElements(By.TagName("td")));
            IWebElement cell = tds[columnIndex];
            BaseElement c = new BaseElement(cell);
            c.Click(cell.Size.Width / 2, cell.Size.Height / 2);
        }

        /// <summary>
        /// Click table cell by using column index and row index
        /// </summary>       
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public void ClickTableCell(int columnIndex, int rowIndex)
        {
            IWebElement tbl = GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            ClickTableCell(trs, columnIndex, rowIndex);
        }

        /// <summary>
        /// Click table cell by using a value of a column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="content"></param>
        public void ClickTableCell(string columnName, string content = null)
        {          
            IWebElement tbl = GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            Table tableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            int columnIndex = GetTableColumnIndex(columnName);

            if(content == null)
            {
                ClickTableCell(trs, columnIndex, 0);
            }
            else
            {
                int rowIndex = GetTableRowIndex(columnIndex, content);
                ClickTableCell(trs, columnIndex, rowIndex);
            }
        }

        /// <summary>
        /// Click table cell using link text
        /// </summary>  
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="text"></param>
        public void ClickTableCellLinkText(int columnIndex, int rowIndex, string text = null)
        {           
            IWebElement tbl = GetElement();
            List<IWebElement> trs = new List<IWebElement>(tbl.FindElements(By.TagName("tr")));
            IWebElement row = trs[rowIndex];

            if (text == null)
            {
                ClickTableCell(rowIndex, columnIndex);
            }
            else
            {
                IWebElement textLnk = row.FindElement(By.LinkText(text));
                textLnk.Click();
            }
        }

        /// <summary>
        /// Convert CSV to data table
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public System.Data.DataTable ConvertCSVtoDataTable(string pathFile)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (StreamReader sr = new StreamReader(pathFile))
            {
                string[] headers = sr.ReadLine().Split('|');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split('|');
                    System.Data.DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }

                    if (rows.Length != headers.Length)
                    {
                        for (int i = headers.Length; i < rows.Length; i++)
                        {
                            dr[headers.Length - 1] = dr[headers.Length - 1] + "|" + rows[i];
                        }
                    }

                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        /// <summary>
        /// Add format for list date string
        /// </summary>
        /// <param name="dateString"></param>
        /// <param name="format"></param>
        /// <param name="outputList"></param>
        public void AddFormatForListDateString(List<string> dateString, string format, out List<string> outputList)
        {
            outputList = new List<string> { };

            for (int index = 0; index < dateString.Count; index++)
            {
                string newString = DateTime.Parse(dateString[index]).ToString(format);
                outputList.Add(newString);
            }
        }

    }
}