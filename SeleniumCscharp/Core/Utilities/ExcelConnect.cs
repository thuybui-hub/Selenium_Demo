using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using OfficeExcel = Microsoft.Office.Interop.Excel;

namespace SeleniumCSharp.Core.Utilities
{
    /// <summary>
    ///     Connect to Excel
    /// </summary>
    public class ExcelConnect
    {
        OleDbConnection connExcel;
        OleDbCommand commExcel;
        DataTable dt;
        DataSet ds;
        OleDbDataAdapter da;

        /// <summary>
        ///     Connect to Excel
        /// </summary>
        /// <param name="excelPath"></param>
        public void connectToExcel(string excelPath)
        {
            string newExcelPath = excelPath.Replace("xlsx", "xls");
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + newExcelPath + ";Extended Properties='Excel 12.0;HDR=Yes'";
            connExcel = new OleDbConnection(connectionString);
            commExcel = new OleDbCommand();
            commExcel.Connection = connExcel;
            connExcel.Open();
        }

        /// <summary>
        ///     Connect to Excel No HDR
        /// </summary>
        /// <param name="excelPath"></param>
        public void connectToExcelNOHDR(string excelPath)
        {
            string newExcelPath = excelPath.Replace("xlsx", "xls");
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + newExcelPath + ";Extended Properties='Excel 12.0;HDR=No'";
            connExcel = new OleDbConnection(connectionString);
            commExcel = new OleDbCommand();
            commExcel.Connection = connExcel;
            connExcel.Open();
        }

        /// <summary>
        ///     Excel Data
        /// </summary>
        /// <param name="command"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataSet excelData(string command, string table)
        {
            try
            {
                dt = new DataTable();
                ds = new DataSet();
                da = new OleDbDataAdapter();
                commExcel.CommandText = command;
                da.SelectCommand = commExcel;
                dt.TableName = table;
                da.Fill(dt);
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to read the excel file due to " + e.ToString());
                return null;
            }
            finally
            {
                closeExcelConnection();
            }
        }

        /// <summary>
        ///     Execute Command
        /// </summary>
        /// <param name="command"></param>
        public void executeCommand(string command)
        {
            try
            {
                commExcel.CommandText = command;
                commExcel.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot run Excel query.\n"
                            + e.Message);
            }
        }

        /// <summary>
        ///     Return null if there is no value
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetExcelSpecifyValue(string command, bool returnNull)
        {
            try
            {
                commExcel.CommandText = command;
                return commExcel.ExecuteScalar().ToString();
            }
            catch
            {
                return null;
            }
            finally
            {
                closeExcelConnection();
            }
        }

        /// <summary>
        ///     Close Excel Connection
        /// </summary>
        public void closeExcelConnection()
        {
            connExcel.Dispose();
            connExcel.Close();
            commExcel.Dispose();
        }

        #region File

        /// <summary>
        /// Get list of sheet names in the order they are defined in the spreadsheet
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetSheetNames(string filePath)
        {
            OfficeExcel.Application excel = null;
            OfficeExcel.Workbook workBooks = null;
            List<string> sheetnames = new List<string>();
            object missing = Missing.Value;

            try
            {
                excel = new OfficeExcel.Application();
                workBooks = excel.Workbooks.Open(filePath);

                foreach (OfficeExcel.Worksheet sheet in workBooks.Sheets)
                {
                    sheetnames.Add(sheet.Name);
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                workBooks.Close();
                excel.Application.Quit();
            }
            return sheetnames;
        }

        /// <summary>
        ///     Unprotect a Workbook
        /// </summary>
        /// <param name="filePath"></param>
        public static void UnprotectAWorkbook(string filePath)
        {
            OfficeExcel.Application excel = null;
            OfficeExcel.Workbook workBooks = null;

            try
            {
                excel = new OfficeExcel.Application();
                workBooks = excel.Workbooks.Open(filePath);
                workBooks.Unprotect("");
                workBooks.Save();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                workBooks.Close();
                excel.Application.Quit();
            }
        }
        
        /// <summary>
        ///     Wait for File Exist
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="miliSecond"></param>
        public static void WaitForFileExist(string filePath, int miliSecond)
        {
            int numTimes = miliSecond / 2000;
            while (numTimes != 0)
            {
                if (File.Exists(filePath))
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    numTimes--;
                }
            }
        }

        /// <summary>
        ///     Check if file exist or not
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="failedMessage"></param>
        /// <returns></returns>
        public static bool IsFileExist(string fileFullPath, out string failedMessage)
        {
            if (File.Exists(fileFullPath))
            {
                failedMessage = "Passed";
                return true;
            }
            else
            {
                failedMessage = fileFullPath + " is not exist";
                return false;
            }
        }

        /// <summary>
        ///     Delete file
        /// </summary>
        /// <param name="fileFullPath"></param>
        public static void DeleteFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
            }
        }

        /// <summary>
        /// Get all data of Search result to DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public static System.Data.DataTable GetDataFromCSVFileToDataTable(string filePath)
        {
            System.Data.DataTable dtTable = new System.Data.DataTable();

            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            StreamReader sr = new StreamReader(filePath);

            // Get Columns Name
            string colsLine = sr.ReadLine();
            colsLine = colsLine.Substring(1, colsLine.Length - 2);
            foreach (string col in Regex.Split(colsLine, "\",\""))
            {
                dtTable.Columns.Add(col);
            }

            // Get Rows
            string[] cells = null;
            string row;
            // Read and display lines from the file until the end of 
            // the file is reached.
            while ((row = sr.ReadLine()) != null)
            {
                row = row.Substring(1, row.Length - 2);

                cells = Regex.Split(row, "\",\"");
                dtTable.Rows.Add(cells);
            }
            sr.Close();
            return dtTable;
        }

        /// <summary>
        /// Convert XLX to CSV
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void ConvertXLSToCSV(string sourceFile, string destinationFile)
        {
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }
            var app = new Microsoft.Office.Interop.Excel.Application();
            var wb = app.Workbooks.Open(sourceFile);
            wb.SaveAs(destinationFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
            wb.Close(Microsoft.Office.Interop.Excel.XlSaveAction.xlSaveChanges);
            app.Quit();
        }

        /// <summary>
        /// Export DataTable to excel file
        /// </summary>
        /// <param name="Tbl"></param>
        /// <param name="ExcelFilePath"></param>
        public static void ExportDataTableToExcel(System.Data.DataTable Tbl, string ExcelFilePath = null)
        {
            DeleteFile(ExcelFilePath);
            try
            {
                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null & ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Convert xlsx to xls
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void ConvertXLSXToXLS(string sourceFile, string destinationFile)
        {
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }
            var app = new OfficeExcel.Application();
            var wb = app.Workbooks.Open(sourceFile);
            wb.SaveAs(destinationFile, OfficeExcel.XlFileFormat.xlExcel5);

            // Sleep 3s to convert completely
            System.Threading.Thread.Sleep(3000);

            wb.Close();
            app.Workbooks.Close();
            app.Quit();
            File.Delete(sourceFile);
        }

        /// <summary>
        ///     Convert .csv to .xls
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        /// <param name="deleteSourceFile"></param>
        public static void ConvertCSVToXLS(string sourceFile, string destinationFile, bool deleteSourceFile = true)
        {
            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);

                // Sleep 1s to delete completely
                System.Threading.Thread.Sleep(1000);
            }
            var app = new OfficeExcel.Application();
            var wb = app.Workbooks.Open(sourceFile);
            wb.SaveAs(destinationFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8);

            // Sleep 3s to convert completely
            System.Threading.Thread.Sleep(3000);

            wb.Close();
            app.Workbooks.Close();
            app.Quit();
            if (deleteSourceFile) File.Delete(sourceFile);
        }

        /// <summary>
        ///     Check if text is exist in excel or not
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="textChecked"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DoesTextExistInExcel(string filePath, string textChecked, out string message)
        {
            StreamReader sr = new StreamReader(filePath);
            string row;

            // Read and display lines from the file until the end of 
            // the file is reached.
            while ((row = sr.ReadLine()) != null)
            {
                if (row.Contains(textChecked))
                {
                    message = "Passed";
                    return true;
                }
            }
            sr.Close();

            message = textChecked + " is not exist in excel file";
            return false;
        }


        /// <summary>
        /// Delete range of rows in excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        public static void DeleteExcelRows(string filePath, string rangeFrom, string rangeTo)
        {
            OfficeExcel.Application app = new OfficeExcel.Application();
            OfficeExcel.Workbook workbook;
            OfficeExcel.Worksheet worksheet;
            workbook = app.Workbooks.Open(filePath);
            worksheet = workbook.ActiveSheet;
            worksheet.get_Range(rangeFrom, rangeTo).EntireRow.Delete();
            app.DisplayAlerts = false;
            workbook.Save();
            app.Workbooks.Close();
            app.Quit();
        }

        /// <summary>
        /// Delete range of columns in excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rangeFrom"></param>
        public static void DeleteExcelColumns(string filePath, string rangeFrom)
        {
            Microsoft.Office.Interop.Excel.Application app = new OfficeExcel.Application();
            OfficeExcel.Workbook workbook;
            OfficeExcel.Worksheet worksheet;
            workbook = app.Workbooks.Open(filePath);
            worksheet = workbook.ActiveSheet;
            worksheet.get_Range(rangeFrom, Type.Missing).EntireColumn.Delete();
            app.DisplayAlerts = false;
            workbook.Save();
            app.Workbooks.Close();
            app.Quit();
        }

        /// <summary>
        ///     Insert new row in excel
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="lineNumber"></param>
        public static void InsertNewRowInExcel(string fileNamePath, int lineNumber)
        {
            OfficeExcel.Application app = new OfficeExcel.Application();
            OfficeExcel.Workbook workbook;
            OfficeExcel.Worksheet worksheet;
            workbook = app.Workbooks.Open(fileNamePath);
            worksheet = workbook.ActiveSheet;
            OfficeExcel.Range line = worksheet.Rows[lineNumber];
            line.Insert();

            app.DisplayAlerts = false;
            workbook.Save();
            app.Workbooks.Close();
            app.Quit();
        }

        /// <summary>
        ///     Push Data into Excel
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="rowNumber"></param>
        /// <param name="columnNumber"></param>
        /// <param name="value"></param>
        public static void PushDataIntoExcel(string fileNamePath, int rowNumber, int columnNumber, string value)
        {
            OfficeExcel.Application app = new OfficeExcel.Application();
            OfficeExcel.Workbook workbook;
            OfficeExcel.Worksheet worksheet;
            workbook = app.Workbooks.Open(fileNamePath);
            worksheet = workbook.ActiveSheet;

            worksheet.Cells[rowNumber, columnNumber] = value;

            app.DisplayAlerts = false;
            workbook.Save();
            app.Workbooks.Close();
            app.Quit();
        }


        #endregion
    }

    /// <summary>
    ///     Excel Actions
    /// </summary>
    public static class ExcelActions
    {
        #region Common Actions
        /// <summary>
        /// Update values of all cells in the specified column to the same new value
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="columnName"></param>
        /// <param name="newValue"></param>
        public static void UpdateCellValuesInColumn(string filePath, string sheetName, string columnName, string newValue)
        {
            filePath = filePath.Replace("csv", "xls");

            string command1 = "UPDATE [" + sheetName + "$] set [" + columnName + "] ='" + newValue + "'";
            ExcelConnect excelConnect = new ExcelConnect();
            excelConnect.connectToExcel(filePath);
            excelConnect.executeCommand(command1);
            excelConnect.closeExcelConnection();

            ExcelConnect.ConvertXLSToCSV(filePath, filePath.Replace("xls", "csv"));

        }

        /// <summary>
        ///     Delete First N Rows In Excel File
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="n"></param>
        public static void DeleteFirstNRowsInExcelFile(string filePath, int n)
        {
            Microsoft.Office.Interop.Excel.Application myApp;
            Microsoft.Office.Interop.Excel.Workbook myWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet myWorkSheet;
            Microsoft.Office.Interop.Excel.Range range, row;
            myApp = new Microsoft.Office.Interop.Excel.Application();
            myWorkBook = myApp.Workbooks.Open(filePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            myWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkBook.Worksheets.get_Item(1);

            range = myWorkSheet.get_Range("A1", "A" + n);
            row = range.EntireRow;
            row.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);
            myApp.DisplayAlerts = false;
            myWorkBook.Save();
            myWorkBook.Close();
            myApp.DisplayAlerts = true;
            myApp.Quit();
        }

        /// <summary>
        /// Get all data in excel file to data table
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataTable GetAllExcelData(string excelFilePath, string sheetName = null)
        {
            //By default sheetname = first sheet name in the file
            if (sheetName == null)
            {
                sheetName = ExcelConnect.GetSheetNames(excelFilePath)[0];
            }
            string selectAllRowsCommand = string.Format("SELECT * FROM [{0}$]", sheetName);
            //string selectAllRowsCommand = string.Format("SELECT * FROM {0}", sheetName);
            ExcelConnect excelConnect = new ExcelConnect();
            excelConnect.connectToExcel(excelFilePath);

            DataTable dataTable = excelConnect.excelData(selectAllRowsCommand, sheetName).Tables[sheetName];

            return dataTable;
        }

        /// <summary>
        /// Get all data in excel file to data table
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataTable GetAllExcelData(string excelFilePath, string sheetName, string columnName, string value)
        {
            //if sheetname = null, get first sheet name in the file
            if (sheetName == null)
            {
                sheetName = ExcelConnect.GetSheetNames(excelFilePath)[0];
            }
            string selectAllRowsCommand = string.Format("SELECT * FROM [{0}$] WHERE [{1}] = '{2}'", sheetName, columnName, value);
            ExcelConnect excelConnect = new ExcelConnect();
            excelConnect.connectToExcel(excelFilePath);

            DataTable dataTable = excelConnect.excelData(selectAllRowsCommand, sheetName).Tables[sheetName];

            return dataTable;
        }

        /// <summary>
        ///     Get All Excel Data
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetAllExcelData(string excelFilePath, string sheetName, string condition)
        {
            //if sheetname = null, get first sheet name in the file
            if (sheetName == null)
            {
                sheetName = ExcelConnect.GetSheetNames(excelFilePath)[0];
            }
            string selectAllRowsCommand = string.Format("SELECT * FROM [{0}$] WHERE {1}", sheetName, condition);
            ExcelConnect excelConnect = new ExcelConnect();
            excelConnect.connectToExcel(excelFilePath);

            DataTable dataTable = excelConnect.excelData(selectAllRowsCommand, sheetName).Tables[sheetName];

            return dataTable;
        }

        /// <summary>
        /// Get values of cells in the specified column in the excel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static List<string> GetCellValuesInColumn(string filePath, string sheetName, string colName, string conColName = null, string conValue = null)
        {
            DataTable data;

            //if sheetname = null, get first sheet name in the file
            if (sheetName == null)
            {
                sheetName = ExcelConnect.GetSheetNames(filePath)[0];
            }

            if (!string.IsNullOrEmpty(conColName) & !string.IsNullOrEmpty(conValue))
            {
                data = ExcelActions.GetAllExcelData(filePath, sheetName, conColName, conValue);
            }
            else
            {
                data = ExcelActions.GetAllExcelData(filePath, sheetName);
            }
            List<string> items = new List<string>();
            foreach (DataRow r in data.Rows)
            {
                string item = r[colName].ToString();
                if (!string.IsNullOrEmpty(item))
                    items.Add(item);
            }

            return items;
        }

        /// <summary>
        ///     Get Cell Values In Column
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="colName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<string> GetCellValuesInColumn(string filePath, string sheetName, string colName, string condition)
        {
            DataTable data;

            //if sheetname = null, get first sheet name in the file
            if (sheetName == null)
            {
                sheetName = ExcelConnect.GetSheetNames(filePath)[0];
            }

            data = ExcelActions.GetAllExcelData(filePath, sheetName, condition);
            List<string> items = new List<string>();
            foreach (DataRow r in data.Rows)
            {
                string item = r[colName].ToString();
                if (!string.IsNullOrEmpty(item))
                    items.Add(item);
            }

            return items;
        }
        #endregion        
    }
}
