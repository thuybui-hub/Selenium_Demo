﻿using InfectionLogAutomation.PageObject.Common;
using InfectionLogAutomation.Utilities;
using Microsoft.VisualStudio.TestTools.UITesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;
using System;
using System.Collections.Generic;
using InfectionLogAutomation.DataObject;
using NUnit.Framework;
using OpenQA.Selenium.IE;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfectionLogAutomation.PageObject.LogEntry
{
    public class LogEntryDetailPage : CommonPage
    {   
        // Common elements     
        public TextBox txtRegion;
        public ComboBox cbbRegion;
        public TextBox txtCommunity;
        public ComboBox cbbCommunity;
        public TextBox txtEmployee;
        public ComboBox cbbEmployee;
        public TextBox txtLastName;
        public TextBox txtFirstName;
        public TextBox txtMrn;
        public Button btnAdvancedSearch;
        public Span spnInfectionType;
        public ComboBox cbbInfectionType;
        public TextBox txtOnsetDate;
        public TextArea txtSymptoms;
        public Span spnTestingStatus;
        public ComboBox cbbTestingStatus;
        public TextBox txtTestingStatusDate;
        public Span spnDisposition;
        public ComboBox cbbDisposition;
        public TextBox txtDispositionDate;
        public TextArea txtComments;
        public Button btnSaveLogEntry;
        public Button btnCancelLogEntry;

        public Ul lstBoxRegion;
        public Ul lstBoxCommunity;
        public Ul lstBoxEmployee;
        public Ul lstBoxTestingStatus;
        public Ul lstBoxDisposition;
        public Ul lstInfectionType;

        // Resident elements
        public Span residentHeaderTemplate;

        // Edit page
        public Span spnInfectionTypeValue, spnOnsetDateValue;


        // Duplicate pop-up
        public readonly BaseElement divDialogContent;
        public readonly Button btnSaveNewEntry;
        public readonly Button btnEditExisting;
        public readonly Button btnCancelEditting;

        // Tabs in New Resident Log Entry form
        public readonly BaseElement divLogEntry;
        public readonly BaseElement divAttachments;
        public readonly Button btnSelectFiles;
        public readonly BaseElement divDocumentTable;
        public readonly Table tblDocumentTableHeader;
        public readonly Table tblDocumentTable;
        public readonly Button btnUploadSelectedFile;
        public readonly Button btnClearSelectedFile;

        #region Actions
        public LogEntryDetailPage()
        {            
            txtRegion = new TextBox(By.XPath("//ul[@id=\"region_taglist\"]//following-sibling::input"));
            cbbRegion = new ComboBox(By.XPath("//select[@Id=\"region\"]"));
            txtCommunity = new TextBox(By.XPath("//ul[@id=\"communityName_taglist\"]//following-sibling::input"));
            cbbCommunity = new ComboBox(By.Id("communityName"));
            txtEmployee = new TextBox(By.XPath("//ul[@id=\"person_taglist\"]//following-sibling::input"));
            cbbEmployee = new ComboBox(By.Id("person"));
            txtLastName = new TextBox(By.Id("lastName"));
            txtFirstName = new TextBox(By.Id("firstName"));
            txtMrn = new TextBox(By.Id("mrn"));
            btnAdvancedSearch = new Button(By.Id("advanceSearch"));
            spnInfectionType = new Span(By.XPath("//label[@id=\"infectionType_label\"]//following-sibling::span"));
            cbbInfectionType = new ComboBox(By.Id("infectionType"));
            txtOnsetDate = new TextBox(By.Id("onsetDate"));
            txtSymptoms = new TextArea(By.XPath("//textarea[@id=\"symptoms\"]//preceding-sibling::iframe"));
            spnTestingStatus = new Span(By.XPath("//label[@id=\"testingStatus_label\"]//following-sibling::span"));
            cbbTestingStatus = new ComboBox(By.Id("testingStatus"));
            txtTestingStatusDate = new TextBox(By.Id("testingStatusDate"));
            spnDisposition = new Span(By.XPath("//label[@id=\"disposition_label\"]//following-sibling::span"));
            cbbDisposition = new ComboBox(By.Id("disposition"));
            txtDispositionDate = new TextBox(By.Id("dispositionDate"));
            txtComments = new TextArea(By.XPath("//textarea[@id=\"comments\"]//preceding-sibling::iframe"));
            btnSaveLogEntry = new Button(By.XPath("//button[@class=\"k-button btnSave\"]"));
            btnCancelLogEntry = new Button(By.XPath("//button[@class=\"k-button btnCancel\"]"));

            lstBoxRegion = new Ul(By.XPath("//ul[@id=\"region_listbox\"]"));
            lstBoxCommunity = new Ul(By.XPath("//ul[@id=\"communityName_listbox\"]"));
            lstBoxEmployee = new Ul(By.XPath("//ul[@id = \"person_listbox\"]"));
            lstBoxTestingStatus = new Ul(By.XPath("//ul[@id=\"testingStatus_listbox\"]"));
            lstBoxDisposition = new Ul(By.XPath("//ul[@id=\"disposition_listbox\"]"));
            lstInfectionType = new Ul(By.XPath("//ul[@id=\"infectionType_listbox\"]"));

            // Resident elements
            residentHeaderTemplate = new Span(By.XPath("//div[@id=\"person-list\"]/div/span"));

            // Edit page
            spnInfectionTypeValue = new Span(By.XPath("//label[text()=\"Infection Type: \"]/span"));
            spnOnsetDateValue = new Span(By.XPath("//label[text()=\"Onset Date: \"]/span"));

            // Duplicate pop-up
            divDialogContent = new BaseElement(By.Id("dialog"));
            btnSaveNewEntry = new Button(By.XPath("//button[@class=\"k-button k-primary\" and text()=\"Save New Entry\"]"));
            btnEditExisting = new Button(By.XPath("//button[@class=\"k-button\" and text()=\"Edit Existing\"]"));
            btnCancelEditting = new Button(By.XPath("//button[@class=\"k-button\" and text()=\"Cancel\"]"));

            // Tabs in New Resident Log Entry form
            divLogEntry = new BaseElement(By.Id("entryTab"));
            divAttachments = new BaseElement(By.Id("attachmentTab"));
            btnSelectFiles = new Button(By.XPath("//div[@class=\"k-button k-upload-button\"]"));
            divDocumentTable = new BaseElement(By.Id("documentsGrid"));
            tblDocumentTableHeader = new Table(By.XPath("//div[@class=\"k-grid-header-wrap k-auto-scrollable\"]/table"));
            tblDocumentTable = new Table(By.XPath("//div[@class=\"k-grid-content k-auto-scrollable\"]/table"));
            btnUploadSelectedFile = new Button(By.XPath("//button[@class=\"k-button k-upload-selected k-primary\" and text()=\"Upload\"]"));
            btnClearSelectedFile = new Button(By.XPath("//button[@class=\"k-button k-clear-selected\" and text()=\"Clear\"]"));
        }
                
        public void FillClientInfo(string lastName, string firstName, string MRN)
        {
            txtLastName.SendKeys(lastName);
            txtFirstName.SendKeys(firstName);
            txtMrn.SendKeys(MRN);
        }
        

        /// <summary>
        /// Fill log entry information randomly and return log entry's information selected
        /// </summary>
        /// <param name="typeOfEntry">typeOfLogEntry can be: Team, Resident, Client</param>
        /// <returns></returns>
        public void FillLogEntryInfoRandomly(string typeOfEntry, out List<string> lstResult)
        {
            DriverUtils.WaitForPageLoad();
            Random rd = new Random();
            List<string> list;
            lstResult = new List<string> { };
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string selectedValue, name, ID, symptom, comments;

            // Fill Region            
            list = GetItemsFromControlList(Fields.region);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();                       
            txtRegion.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            lstResult.Add(selectedValue);            

            // Fill Community
            DriverUtils.WaitForPageLoad();            
            list = GetItemsFromControlList(Fields.community);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();            
            txtCommunity.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            lstResult.Add(selectedValue);

            // Fill Employee/Resident/Firs tName; Last Name; MRN
             switch (typeOfEntry)
            {
                case "Team":
                    DriverUtils.WaitForPageLoad();                    
                    list = GetItemsFromControlList(Fields.employee);
                    selectedValue = list[rd.Next(0, list.Count - 1)];
                    name = selectedValue.Substring(0, selectedValue.IndexOf("(") - 1);
                    ID = selectedValue.Substring(selectedValue.IndexOf("(") + 1, selectedValue.IndexOf(")") - selectedValue.IndexOf("(") - 1);                    
                    txtEmployee.SendKeys(selectedValue);
                    DriverUtils.wait(2);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    lstResult.Add(name);
                    lstResult.Add(ID);
                    break;

                case "Resident":
                    DriverUtils.WaitForPageLoad();
                    txtEmployee.Click();
                    list = lstBoxEmployee.GetSubOptions();
                    selectedValue = list[rd.Next(0, list.Count - 1)];
                    int secondSpaceIndex = selectedValue.IndexOf(" ", selectedValue.IndexOf(" ", 0) + 1);
                    int thirdSpaceIndex = selectedValue.IndexOf(" ", secondSpaceIndex + 1);
                    name = selectedValue.Substring(0, secondSpaceIndex);
                    ID = selectedValue.Substring(secondSpaceIndex + 1, thirdSpaceIndex - secondSpaceIndex - 1);
                    txtEmployee.SendKeys(name);
                    DriverUtils.wait(2);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    lstResult.Add(name);
                    lstResult.Add(ID);
                    break;

                case "Client":
                    string lastName = "LN" + Utils.GetRandomValue("last name");
                    string firstName = "FN" + Utils.GetRandomValue("first name");
                    string MRN = rd.Next(100, 123456789).ToString();
                    FillClientInfo(lastName, firstName, MRN);
                    lstResult.Add(lastName + ", " + firstName);                    
                    lstResult.Add(MRN);
                    break;
            }

            // Fill Onset Date            
            txtOnsetDate.SendKeys(date);
            lstResult.Add(date);

            // Fill Symptoms
            symptom = "Symptom " + Utils.GetRandomValue("random value");
            txtSymptoms.Click();
            System.Windows.Forms.SendKeys.SendWait(symptom);            
            lstResult.Add(symptom);

            // Fill Test Status            
            list = GetItemsFromControlList(Fields.testStatus);
            selectedValue = list[rd.Next(0, list.Count - 1)];
            spnTestingStatus.Click();
            lstBoxTestingStatus.SelectOptionByText(selectedValue);
            lstResult.Add(selectedValue);

            // Fill Test Status date
            txtTestingStatusDate.SendKeys(date);
            lstResult.Add(date);

            // Fill Disposition            
            list = GetItemsFromControlList(Fields.disposition);
            selectedValue = list[rd.Next(0, list.Count - 1)];
            spnDisposition.Click();
            lstBoxDisposition.SelectOptionByText(selectedValue);
            lstResult.Add(selectedValue);            

            // Fill Disposition Date
            txtDispositionDate.SendKeys(date);
            lstResult.Add(date);

            // Fill Comments
            txtComments.ScrollToView();
            comments = "Comments " + Utils.GetRandomValue("random value");
            txtComments.Click();
            System.Windows.Forms.SendKeys.SendWait(comments);
            lstResult.Add(comments);            
        }


        /// <summary>
        /// Fill log entry information randomly and return log entry's information selected as LogEntryData
        /// </summary>
        /// <param name="typeOfEntry">typeOfLogEntry can be: Team, Resident, Client</param>
        /// <returns></returns>
        public void FillLogEntryInfoRandomly(string typeOfEntry, out LogEntryData logEntryData)
        {
            DriverUtils.WaitForPageLoad();
            Random rd = new Random();
            List<string> list;
            logEntryData = new LogEntryData();

            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string selectedValue, name, ID, symptom, comments;

            // Fill Region            
            list = GetItemsFromControlList(Fields.region);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtRegion.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            logEntryData.Region = selectedValue;

            // Fill Community
            DriverUtils.WaitForPageLoad();
            list = GetItemsFromControlList(Fields.community);
            selectedValue = list[rd.Next(0, list.Count - 1)].Trim();
            txtCommunity.SendKeys(selectedValue);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
            logEntryData.Community = selectedValue;

            // Fill Employee/Resident/Firs tName; Last Name; MRN
            switch (typeOfEntry)
            {
                case "Team":
                    DriverUtils.WaitForPageLoad();
                    list = GetItemsFromControlList(Fields.employee);
                    selectedValue = list[rd.Next(0, list.Count - 1)];
                    name = selectedValue.Substring(0, selectedValue.IndexOf("(") - 1);
                    ID = selectedValue.Substring(selectedValue.IndexOf("(") + 1, selectedValue.IndexOf(")") - selectedValue.IndexOf("(") - 1);
                    txtEmployee.SendKeys(selectedValue);
                    DriverUtils.wait(2);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    logEntryData.Name = name;
                    logEntryData.MRN = ID;
                    break;

                case "Resident":
                    DriverUtils.WaitForPageLoad();
                    txtEmployee.Click();
                    list = lstBoxEmployee.GetSubOptions();
                    selectedValue = list[rd.Next(0, list.Count - 1)];
                    int secondSpaceIndex = selectedValue.IndexOf(" ", selectedValue.IndexOf(" ", 0) + 1);
                    int thirdSpaceIndex = selectedValue.IndexOf(" ", secondSpaceIndex + 1);
                    name = selectedValue.Substring(0, secondSpaceIndex);
                    ID = selectedValue.Substring(secondSpaceIndex + 1, thirdSpaceIndex - secondSpaceIndex - 1);
                    txtEmployee.SendKeys(name);
                    DriverUtils.wait(2);
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    logEntryData.Name = name;
                    logEntryData.MRN = ID;
                    break;

                case "Client":
                    string lastName = "LN" + Utils.GetRandomValue("last name");
                    string firstName = "FN" + Utils.GetRandomValue("first name");
                    string MRN = rd.Next(100, 123456789).ToString();
                    FillClientInfo(lastName, firstName, MRN);
                    logEntryData.Name = lastName + ", " + firstName;
                    logEntryData.MRN = MRN;                    
                    break;
            }

            // Fill Onset Date            
            txtOnsetDate.SendKeys(date);
            logEntryData.OnsetDate = date;            

            // Fill Symptoms
            symptom = "Symptom " + Utils.GetRandomValue("random value");
            txtSymptoms.Click();
            System.Windows.Forms.SendKeys.SendWait(symptom);
            logEntryData.Symptoms = symptom;            

            // Fill Test Status            
            list = GetItemsFromControlList(Fields.testStatus);
            selectedValue = list[rd.Next(0, list.Count - 1)];
            spnTestingStatus.Click();
            lstBoxTestingStatus.SelectOptionByText(selectedValue);
            logEntryData.CurrentTestStatus = selectedValue;            

            // Fill Test Status date
            txtTestingStatusDate.SendKeys(date);
            logEntryData.TestStatusDate = date;            

            // Fill Disposition            
            list = GetItemsFromControlList(Fields.disposition);
            selectedValue = list[rd.Next(0, list.Count - 1)];
            spnDisposition.Click();
            lstBoxDisposition.SelectOptionByText(selectedValue);
            logEntryData.CurrentDisposition = selectedValue;

            // Fill Disposition Date
            txtDispositionDate.SendKeys(date);
            logEntryData.DispositionDate = date;

            // Fill Comments
            txtComments.ScrollToView();
            comments = "Comments " + Utils.GetRandomValue("random value");
            txtComments.Click();
            System.Windows.Forms.SendKeys.SendWait(comments);
            logEntryData.Comments = comments;
        }

        /// <summary>
        /// Fill log entry info
        /// </summary>
        /// <param name="logEntryData"></param>
        /// <param name="typeOfEntry">typeOfLogEntry can be: Team, Resident, Client</param>
        public void FillLogEntryInfo(LogEntryData logEntryData, string typeOfEntry)
        {
            // Fill region
            if (!string.IsNullOrEmpty(logEntryData.Region))
                txtRegion.SendKeys(logEntryData.Region);

            // Fill community
            if (!string.IsNullOrEmpty(logEntryData.Community))
                txtCommunity.SendKeys(logEntryData.Community);

            // Fill employee/resident/Client name
            if (!string.IsNullOrEmpty(logEntryData.Name))
            {
                if (typeOfEntry.Equals("Client"))
                {
                    string lastName = logEntryData.Name.Substring(0, logEntryData.Name.IndexOf(","));
                    string firstName = logEntryData.Name.Substring(logEntryData.Name.IndexOf(" ", 0) + 1, logEntryData.Name.Length - logEntryData.Name.IndexOf(" ", 0) - 1); ;
                    txtLastName.SendKeys(lastName);
                    txtFirstName.SendKeys(firstName);
                }
                else
                {
                    txtEmployee.SendKeys(logEntryData.Name);
                }
            }

            // Fill OnsetDate
            if (!string.IsNullOrEmpty(logEntryData.OnsetDate))
            {
                txtOnsetDate.SendKeys(logEntryData.OnsetDate);
            }

            // Fill Symptoms
            if (!string.IsNullOrEmpty(logEntryData.Symptoms))
            {
                txtSymptoms.Click();
                System.Windows.Forms.SendKeys.SendWait(logEntryData.Symptoms);
            }

            // Fill Test Status
            if (!string.IsNullOrEmpty(logEntryData.CurrentTestStatus))
            {
                spnTestingStatus.Click();
                System.Windows.Forms.SendKeys.SendWait(logEntryData.TestStatusDate);
            }

            // Fill Test Status Date
            if (!string.IsNullOrEmpty(logEntryData.TestStatusDate))
            {
                txtTestingStatusDate.SendKeys(logEntryData.TestStatusDate);
            }

            // Fill Disposition
            if (!string.IsNullOrEmpty(logEntryData.CurrentDisposition))
            {
                spnDisposition.Click();
                System.Windows.Forms.SendKeys.SendWait(logEntryData.CurrentDisposition);
            }

            // Fill Disposition Date
            if (!string.IsNullOrEmpty(logEntryData.DispositionDate))
            {
                txtDispositionDate.SendKeys(logEntryData.DispositionDate);
            }

            // Fill Comments
            if (!string.IsNullOrEmpty(logEntryData.Comments))
            {
                txtComments.ScrollToView();
                txtComments.Click();
                System.Windows.Forms.SendKeys.SendWait(logEntryData.Comments);
            }
        }

        public void SelectATestStatusOrDisposition(string field, string value)
        {
            DriverUtils.WaitForPageLoad();

            switch (field)
            {
                case "Test Status":
                    spnTestingStatus.Click();
                    lstBoxTestingStatus.SelectOptionByText(value);
                    break;
                case "Disposition":
                    spnDisposition.Click();
                    lstBoxDisposition.SelectOptionByText(value);
                    break;
                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }
        }

        public void SaveLogEntry()
        {
            DriverUtils.WaitForPageLoad();
            btnSaveLogEntry.ScrollToView();

            btnSaveLogEntry.Click();            
            DriverUtils.WaitForPageLoad();          

            if (btnSaveNewEntry.IsDisplayed())
            {
                btnSaveNewEntry.Click();
            }

            DriverUtils.WaitForPageLoad();            
        }

        public void CancelLogEntry()
        {
            btnCancelLogEntry.ScrollToView();
            btnCancelLogEntry.Click();
        }        

        /// <summary>
        /// Get all items in a list of a control that drop down a list
        /// </summary>
        /// <param name="field">HtmlCustom field</param>
        /// <returns></returns>
        public List<string> GetItemsFromControlList(Fields field)
        {
            DriverUtils.WaitForPageLoad();
            Ul element;

            switch (field)
            {
                case Fields.region:
                    element = lstBoxRegion;
                    txtRegion.Click();
                    break;

                case Fields.community:
                    element = lstBoxCommunity;
                    txtCommunity.Click();
                    break;
                case Fields.employee:
                    element = lstBoxEmployee;
                    txtEmployee.Click();
                    break;
                case Fields.testStatus:
                    element = lstBoxTestingStatus;
                    spnTestingStatus.Click();
                    break;
                case Fields.disposition:
                    element = lstBoxDisposition;
                    spnDisposition.ScrollToView();
                    spnDisposition.Click();
                    break;

                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }
            List<string> displayedList = element.GetOptions();

            switch (field)
            {
                case Fields.region:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;
                case Fields.community:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;
                case Fields.employee:
                    System.Windows.Forms.SendKeys.SendWait("{tab}");
                    break;
                case Fields.testStatus:
                    spnTestingStatus.Click();
                    break;
                case Fields.disposition:
                    spnDisposition.Click();
                    break;

                default:
                    throw new Exception(string.Format("'{0}' field is invalid."));
            }

            return displayedList;
        }

        public void SelectResidentFormTab(string tabName)
        {
            DriverUtils.WaitForPageLoad();
            Span tab = new Span(By.XPath("//span[@class=\"k-link\" and contains(text(),\"" + tabName +"\")]"));
            tab.Click();
            DriverUtils.WaitForPageLoad();

            if (btnEditExisting.IsDisplayed())
            {
                btnEditExisting.Click();
                DriverUtils.WaitForPageLoad();
                tab.Click();
            }
        }

        #region Attachments
        /// <summary>
        /// Upload an attachment for a resident log entry
        /// </summary>
        /// <param name="filePath"></param>
        public void SelectAnAttachment(string filePath)
        {
            DriverUtils.WaitForPageLoad();
            btnSelectFiles.Click();
            System.Windows.Forms.SendKeys.SendWait(filePath);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
        }

        /// <summary>
        /// Clear the selected attachment in Selete files txt
        /// </summary>
        public void ClearSelectedAttachment()
        {
            DriverUtils.WaitForPageLoad();
            btnClearSelectedFile.Click();
        }

        /// <summary>
        /// Upload an attachment
        /// </summary>
        public void UploadAttachment()
        {
            DriverUtils.WaitForPageLoad();
            btnUploadSelectedFile.Click();
            DriverUtils.wait(2);
            System.Windows.Forms.SendKeys.SendWait("{Tab}");
            System.Windows.Forms.SendKeys.SendWait("{Enter}");
        }

        /// <summary>
        /// Click on Delete button of an attachment
        /// </summary>
        /// <param name="fileName"></param>
        public void ClickOnDeleteAttachedButton(string fileName)
        {
            DriverUtils.WaitForPageLoad();
            int rowIndex = tblDocumentTable.GetTableRowIndex(0, fileName);
            tblDocumentTable.ClickTableCell(3, rowIndex);
        }

        /// <summary>
        /// Confirm if continue on deleting attachment or not
        /// </summary>
        /// <param name="buttonName"></param>
        public void ConfirmAttachedDeletion(string buttonName)
        {
            DriverUtils.WaitForPageLoad();
            switch (buttonName)
            {
                case "OK":
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    break;
                case "Cancel":
                    System.Windows.Forms.SendKeys.SendWait("{Tab}");
                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                    break;
            }
        }

        /// <summary>
        /// Delete an attachment
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buttonName"></param>
        public void DeleteAnAttachment(string fileName, string buttonName)
        {
            ClickOnDeleteAttachedButton(fileName);
            ConfirmAttachedDeletion(buttonName);
        }
        #endregion Attachment
        #endregion Actions        


        #region Check points

        /// <summary>
        /// Check to see if UI with of New Team Log entry display correctly
        /// </summary>
        /// typeOfLogEntry can be: Team, Resident, Client
        /// <returns></returns>
        public bool DoesUIWithFormatDisplayCorrectly(string typeOfLogEntry = "Team")
        {
            bool result = true;            

            result = result && txtRegion.IsDisplayed()
                && txtRegion.GetAttribute("role").Equals("listbox")
                && txtCommunity.IsDisplayed()
                && txtCommunity.GetAttribute("role").Equals("listbox")
                && spnInfectionType.IsDisplayed()
                && spnInfectionType.GetAttribute("role").Equals("listbox")           
                && txtOnsetDate.IsDisplayed()
                && txtOnsetDate.GetAttribute("data-role").Equals("datepicker")
                && txtSymptoms.IsDisplayed()
                && spnTestingStatus.IsDisplayed()
                && spnTestingStatus.GetAttribute("role").Equals("listbox")
                && txtTestingStatusDate.IsDisplayed()
                & txtTestingStatusDate.GetAttribute("data-role").Equals("datepicker")
                && spnDisposition.IsDisplayed()
                && spnDisposition.GetAttribute("role").Equals("listbox")
                && txtDispositionDate.IsDisplayed()
                && txtDispositionDate.GetAttribute("data-role").Equals("datepicker");

            txtComments.ScrollToView();
            result = result
                && txtComments.IsDisplayed()                
                && btnSaveLogEntry.IsDisplayed()
                && btnCancelLogEntry.IsDisplayed();

            if (!string.IsNullOrEmpty(typeOfLogEntry))
            {
                switch (typeOfLogEntry)
                {
                    case "Team":
                        result = result && txtEmployee.IsDisplayed() && txtEmployee.GetAttribute("role").Contains("listbox");
                        break;

                    case "Resident":
                        result = result && txtEmployee.IsDisplayed() && txtEmployee.GetAttribute("role").Contains("listbox");
                        break;

                    case "Client":
                        result = result
                            && txtFirstName.IsDisplayed()
                            && txtFirstName.IsDisplayed()
                            && txtMrn.IsDisplayed();
                        break;
                    default:
                        throw new Exception(string.Format("Type of Log Entry is incorrect"));
                }
            }
                        return result;
        }
        /// <summary>
        /// Check to see if UI of New Team Log entry display correctly
        /// </summary>
        /// typeOfLogEntry can be: Team, Resident, Client
        /// <returns></returns>
        public bool DoesUIDisplayCorrectly(string typeOfLogEntry = "Team")
        {
            bool result = txtRegion.IsDisplayed()
                && txtCommunity.IsDisplayed()
                && spnInfectionType.IsDisplayed()
                && txtOnsetDate.IsDisplayed()
                && txtSymptoms.IsDisplayed()
                && spnTestingStatus.IsDisplayed()
                && txtTestingStatusDate.IsDisplayed()
                && spnDisposition.IsDisplayed()
                && txtDispositionDate.IsDisplayed();
            
            txtComments.ScrollToView();

            result = result && txtComments.IsDisplayed()
                && btnSaveLogEntry.IsDisplayed()
                && btnCancelLogEntry.IsDisplayed();                      

            if (!string.IsNullOrEmpty(typeOfLogEntry))
            {
                switch (typeOfLogEntry)
                {
                    case "Team":
                        result = result && txtEmployee.IsDisplayed();
                        break;

                    case "Resident":
                        result = result && txtEmployee.IsDisplayed();
                        break;

                    case "Client":
                        result = result && txtFirstName.IsDisplayed() && txtLastName.IsDisplayed() && txtMrn.IsDisplayed();
                        break;

                    default:
                        throw new Exception(string.Format("Type of Log Entry is incorrect"));
                }
            }

            return result;               
        }

        public bool DoesInfectionTypeDisplayCorrectly(string entryType)
        {
            bool result = false;
            switch (entryType)
            {
                case "Team":
                    result = spnInfectionType.GetAttribute("role").Equals("listbox");
                    spnInfectionType.Click();
                    result = result & !lstInfectionType.IsDisplayed();
                    break;
                case "Resident":
                    result = spnInfectionType.GetAttribute("role").Equals("listbox");
                    spnInfectionType.Click();
                    result = result & lstInfectionType.IsDisplayed();
                    break;
                case "Client":
                    result = spnInfectionType.GetAttribute("role").Equals("listbox");
                    spnInfectionType.Click();
                    result = result & !lstInfectionType.IsDisplayed();
                    break;
            }
            return result;
        }

        public bool IsTestStatusOrDispositionUnableToBeEditted(string field)
        {
            bool result = true;
            switch (field)
            {
                case "Test Status":
                    result = spnTestingStatus.GetAttribute("aria-disabled").Equals("true");
                    break;
                case "Disposition":
                    result = spnDisposition.GetAttribute("aria-disabled").Equals("true");
                    break;
                default:
                    throw new Exception(string.Format("Field is incorrect."));
            }
            return result;
        }

        public bool DoesDataOnEditPageDisplayCorrectly(List<string> entryInfo, string page = "Team")
        {
            DriverUtils.WaitForPageLoad();
            string title = "Infection Log Entry for Team Member " + entryInfo[6] + " (" + entryInfo[5] + ")";
            string regionAndCommunityInfo = "at " + entryInfo[2] + ", " + entryInfo[4];

            switch (page)
            {
                case "Resident":                    
                    title = "Infection Log Entry for Resident " + entryInfo[6] + " (" + entryInfo[5] + ")";
                    break;
                case "Client":                    
                    title = "Infection Log Entry for Ageility Client " + entryInfo[6] + " (" + entryInfo[5] + ")";
                    break;
            }         

            return lblTitle.GetText().Equals(title)
                && lblRegionAndCommunity.GetText().Equals(regionAndCommunityInfo)
                && spnInfectionTypeValue.GetText().Equals(entryInfo[8].ToString())
                && spnOnsetDateValue.GetText().Equals(entryInfo[9].ToString())
                && spnTestingStatus.GetText().Equals(entryInfo[10].ToString())
                && spnDisposition.GetText().Equals(entryInfo[11].ToString());
        }


        #region Attachments/// <summary>
        /// Check if readonly user is able to add attachments or not
        /// </summary>
        /// <param> status = 'able'/'unable'</param>
        public bool IsAbleToAddAttachment()
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            result = btnSelectFiles.IsDisplayed() && btnSelectFiles.IsEnabled();
            return result;
        }

        /// <summary>
        /// Check if readonly user is able to delete attachments or not
        /// </summary>
        /// <param> status = 'able'/'unable'</param>
        public bool IsUnableToDeleteAttachment()
        {
            bool result = true;
            BaseElement btnDeleteAttachment = new BaseElement(By.XPath("//a[@class=\"k-command-cell\"]"));
            List<BaseElement> lstDeleteBtn = btnDeleteAttachment.GetListElements();
            
            foreach (BaseElement btn in lstDeleteBtn)
            {
                if (btn.IsDisplayed())
                {
                    result = result && btn.GetAttribute("style").Equals("display:none");
                }
            }
            return result;
        }

        public bool IsDocumentUploadedSuccessfully(string fileName, string uploadedBy, string uploadedDate)
        {

            bool result = true;
            DriverUtils.WaitForPageLoad();

            if (tblDocumentTable.IsDisplayed())
            {
                List<string> lstFiles = tblDocumentTable.GetTableAllCellValueInColumn(0);

                result = lstFiles.Contains(fileName);

                if (result)
                {
                    int rowIndex = tblDocumentTable.GetTableRowIndex(0, fileName);
                    string actualUploadedBy = tblDocumentTable.GetTableCellValue(1, rowIndex);
                    string actualUploadedDate = DateTime.Parse(tblDocumentTable.GetTableCellValue(2, rowIndex)).ToString("MM/dd/yyyy");

                    int index = uploadedBy.IndexOf("\\", 0);
                    string name = uploadedBy.Substring(index + 1, uploadedBy.Length - index - 1);

                    result = result && actualUploadedBy.Equals(name)
                          && (actualUploadedDate.Equals(uploadedDate) || actualUploadedDate.Equals(DateTime.Parse(uploadedDate).AddDays(-1).ToString("MM/dd/yyyy")));
                }
            }
            else result = false;

            return result;
        }

        /// <summary>
        /// Check to see if the attachment is deleted successfully
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool IsAttachedDeleted(string fileName)
        {
            DriverUtils.WaitForPageLoad();
            bool result = true;
            if (divNoRecords.IsDisplayed())
            {
                result = true;
            }
            else
            {
                List<string> attachedList = tblDocumentTable.GetTableAllCellValueInColumn("File Name");
                if (attachedList.Contains(fileName))
                {
                    result = false;
                }
                else result = true;
            }

            return result;
        }

        /// <summary>
        /// Check if readonly user is able to update log entry information or not
        /// </summary>
        /// <param> </param>
        public bool IsReadOnlyUserAbleToUpdateLogEntryInfo()
        {
            BaseElement field = new BaseElement(By.XPath("/html[head/title[text()=\"Kendo UI Editor content\"]]/body"));
            List<BaseElement> lstField = field.GetListElements();
            return spnInfectionTypeValue.IsDisplayed()
                && spnOnsetDateValue.IsDisplayed()
                && txtSymptoms.IsDisplayed()
                && lstField[0].GetAttribute("contenteditable").Equals("false")
                && spnTestingStatus.IsDisplayed()
                && spnTestingStatus.GetAttribute("aria-disabled").Equals("true")
                && txtTestingStatusDate.IsDisplayed()
                && txtTestingStatusDate.GetAttribute("readonly").Equals(null)
                && spnDisposition.IsDisplayed()
                && spnDisposition.GetAttribute("aria-disabled").Equals("true")
                && txtDispositionDate.IsDisplayed()
                && txtDispositionDate.GetAttribute("readonly").Equals(null)
                && txtComments.IsDisplayed()
                && lstField[1].GetAttribute("contenteditable").Equals("false")
                && btnSaveLogEntry.IsDisplayed()
                && btnSaveLogEntry.GetAttribute("disabled").Equals("disabled");
        }
        #endregion Attachments
        #endregion Check points
    }
}
