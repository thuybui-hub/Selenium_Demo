using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Return the inner text of all options in the element of type combobox defined as UL -> LI.
        /// </summary>
        /// <returns></returns>
        public List<string> GetOptions()
        {
            List<string> displayedList = new List<string> { };
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));

            if(listElements.Count == 0)
            {
                displayedList = null;
            }
            else
            {
                foreach (IWebElement item in listElements)
                {
                    if (item.Displayed)
                    {
                        displayedList.Add(item.Text.Trim());
                    }
                }
            }
            return displayedList;
        }

        /// <summary>
        /// Return the inner text of all sub options in the element of type combobox defined as UL -> LI -> Span
        /// </summary>
        /// <returns></returns>
        public List<string> GetSubOptions()
        {
            List<string> displayedList = new List<string> { };
            List<IWebElement> lstLIElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));
            
            foreach (IWebElement element in lstLIElements)
            {
                List<IWebElement> spanElements = new List<IWebElement>(element.FindElements(By.TagName("span")));
                string rowValue = "";

                for (int i = 0; i < spanElements.Count; i++)
                {
                    rowValue = rowValue + " " + spanElements[i].Text;                    
                }
                displayedList.Add(rowValue.Trim());
            }

            return displayedList;
        }

        /// <summary>
        /// Return selected value in a list
        /// </summary>
        /// <returns></returns>
        public string GetSelectedOption()
        {
            string selectedOption = "";
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));

            foreach (IWebElement item in listElements)
            {
                if (item.GetAttribute("aria-selected").Equals("true"))
                {
                    selectedOption = item.Text;
                    break;
                }
            }
            return selectedOption;
        }

        /// <summary>
        /// Return list of selected value in a list
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedOptions()
        {
            List<string> selectedOption = new List<string> { };
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));

            foreach (IWebElement item in listElements)
            {
                if (item.GetAttribute("aria-selected").Equals("true"))
                {
                    selectedOption.Add(item.Text);
                    break;
                }
            }
            return selectedOption;
        }

        /// <summary>
        /// Select an item in the element of type UL/LI by its inner text. Condition: The item has to be visible on the screen.
        /// </summary>
        /// <param name="text"> Text of option to be selected</param>
        public void SelectOptionByText(string text)
        {
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));

            foreach (IWebElement item in listElements)
            {
                if (item.Text.Equals(text))
                {                    
                    item.Click();
                    break;                  
                }                
            }
        }        

        /// <summary>
        /// Select an item in the element of type UL/LI by its index.
        /// </summary>
        /// <param name="index"> Index of item to be selected. Index starts from 1</param>
        public void SelectOptionByIndex(int index)
        {
            List<IWebElement> listElements = new List<IWebElement>(GetElement().FindElements(By.TagName("li")));
            int acIndex = index - 1;

            try
            {
                if (index <= listElements.Count)
                {
                    foreach (IWebElement item in listElements)
                    {
                        if (item.GetAttribute("data-offset-index").Equals(acIndex.ToString()))
                        {
                            item.Click();
                            break;
                        }
                    }
                }
            }
            catch
            { }
        }
    }
}