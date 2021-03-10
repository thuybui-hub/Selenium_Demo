﻿using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using SeleniumCSharp.Core.Drivers;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Collections.Generic;

namespace SeleniumCSharp.Core.DriverWrapper
{
    /// <summary>
    ///     Use to control selenium web driver.
    /// </summary>
    public class DriverUtils
    {
        /// <summary>
        ///     Contain properties of web driver
        /// </summary>
        private static readonly ThreadLocal<DriverManager> _DRIVER = new ThreadLocal<DriverManager>();

        private static readonly ThreadLocal<Key> _DRIVER_KEY = new ThreadLocal<Key>();

        /// <summary>
        ///     Get time out for finding web element
        /// </summary>
        /// <returns></returns>
        public static int GetElementTimeOut()
        {
            return _DRIVER.Value.GetElementTimeOut(GetKeyName());
        }

        /// <summary>
        ///     create webdriver by properties,  driver key = "default" if not set
        /// </summary>
        public static void CreateDriver(DriverProperties pros, string key = "default")
        {
            DriverManager driverManager = null;
            switch (pros.GetDriverType())
            {
                case DriverType.Chrome:
                    driverManager = new ChromeBrowser();
                    break;
                case DriverType.Firefox:
                    driverManager = new FireFoxBrowser();
                    break;
                case DriverType.IE:
                    driverManager = new IEBrowser();
                    break;
                //case DriverType.IOSApp:
                //    driverManager = new IOSAppDriver();
                //    break;
                //case DriverType.IOSWeb:
                //    driverManager = new IOSWebDriver();
                //    break;
                //case DriverType.AndroidApp:
                //    driverManager = new AndroidAppDriver();
                //    break;
                //case DriverType.AndroidWeb:
                //    driverManager = new AndroidWebDriver();
                //    break;
                case DriverType.Edge:
                    break;
                case DriverType.Safari:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (driverManager != null)
            {
                driverManager.SetDriverProperties(key, pros);
                driverManager.CreateDriver(key);
                if (_DRIVER.Value == null)
                {
                    _DRIVER.Value = driverManager;
                }
                else
                {
                    _DRIVER.Value.SetDriverProperties(key, pros);
                    _DRIVER.Value.SetWebDriver(key, driverManager.GetWebDriver(key));
                }

                var driverKey = new Key {Name = key};
                _DRIVER_KEY.Value = driverKey;
            }
            else
            {
                throw new Exception("Cannot create web driver");
            }
        }

        /// <summary>
        ///     return current webdriver
        /// </summary>
        public static IWebDriver GetDriver()
        {
            return _DRIVER.Value.GetWebDriver(GetKeyName());
        }

        /// <summary>
        ///     Get current web driver key
        /// </summary>
        /// <returns></returns>
        public static string GetKeyName()
        {
            return _DRIVER_KEY.Value.Name;
        }

        /// <summary>
        /// Get current key object of web driver
        /// </summary>
        /// <returns></returns>
        public static Key GetKey()
        {
            return _DRIVER_KEY.Value;
        }

        /// <summary>
        ///     Switch to another driver in the list, 1st driver key = "default" if not set
        /// </summary>
        public static void SwitchDriverTo(string key = "default")
        {
            _DRIVER_KEY.Value.Name = key;
        }


        /// <summary>
        ///     Navigate to URL.
        /// </summary>
        public static void GoToUrl(string url)
        {
            GetDriver().Navigate().GoToUrl(url);
            WaitForPageLoad();
        }

        /// <summary>
        ///     Back to prevous page
        /// </summary>
        public static void BackToPreviousPage()
        {
            WaitForPageLoad();
            GetDriver().Navigate().Back();
            WaitForPageLoad();
        }

        /// <summary>
        ///     Return current URL.
        /// </summary>
        public static string CurrentUrl()
        {
            return GetDriver().Url;
        }

        /// <summary>
        ///     Quit all driver instances.
        /// </summary>
        public static void CloseDrivers()
        {
            _DRIVER.Value.CloseDrivers();
        }

        /// <summary>
        ///     Close current web driver instance
        /// </summary>
        public static void CloseCurrent()
        {
            GetDriver().Close();
        }

        /// <summary>
        ///     Maximize web browser.
        /// </summary>
        public static void Maximize()
        {
            try
            {
                GetDriver().Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when maximizing ", e.Message);
            }
        }

        /// <summary>
        ///     Minimize web browser.
        /// </summary>
        public static void Minimize()
        {
            GetDriver().Manage().Window.Minimize();
        }

        /// <summary>
        ///     Switch to Iframe IWebElement.
        /// </summary>
        public static void SwitchToIframe(IWebElement iframe)
        {
            GetDriver().SwitchTo().Frame(iframe);
        }

        /// <summary>
        ///     Switch to Previous frame
        /// </summary>
        public static void SwitchToPrevious()
        {
            GetDriver().SwitchTo().ParentFrame();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SwitchToNewWindow()
        {
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles.Last());
        }

        /// <summary>
        /// 
        /// </summary>
        public static void SwitchToPreviousParentWindow()
        {
            GetDriver().SwitchTo().Window(GetDriver().WindowHandles.First());
        }

        /// <summary>
        ///     Create javascript executor.
        /// </summary>
        public static IJavaScriptExecutor GetJsExecutor()
        {
            return (IJavaScriptExecutor) GetDriver();
        }

        /// <summary>
        ///     Execute javascript .
        /// </summary>
        public static object ExecuteScript(string code, params object[] args)
        {
            return GetJsExecutor().ExecuteScript(code, args);
        }

        /// <summary>
        ///     Get current driver properties
        /// </summary>
        /// <returns></returns>
        public static DriverProperties GetDriverProperties()
        {
            return _DRIVER.Value.GetDriverProperties(GetKeyName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public static void TakeScreenshot(string filepath)
        {
            var ssdriver = GetDriver() as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(filepath, ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<LogEntry> GetBrowserLog()
        {
            return GetDriver().Manage().Logs.GetLog(LogType.Browser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitForPageLoad(int timeoutInSeconds = 60)
        {
            try
            {
                var wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(timeoutInSeconds));
                // If an alert dialog is present then this call to javascript will fail out, to avoid some noises when debugging tests we will check for the alert before attempting the wait
                if (null != ExpectedConditions.AlertIsPresent().Invoke(GetDriver())) return;               

                wait.Until(webDriver => (bool)((IJavaScriptExecutor)GetDriver()).ExecuteScript("if (typeof jQuery != 'undefined') { return jQuery.active == 0; } else {  return true; }"));
                wait.Until(webDriver => ((IJavaScriptExecutor)GetDriver()).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public static void Highlight(IWebElement element)
        {
            var javaScriptDriver = (IJavaScriptExecutor)GetDriver();
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 3px; border-style: solid; border-color: red"";";
            javaScriptDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOut"></param>
        public static void wait(int timeOut = 10)
        {
            Thread.Sleep(timeOut * 1000);
        }

        ///// <summary>
        ///// Scroll down, up, left, right.
        ///// xPixels is the number at x-axis, it moves to the left if number is positive and it move to the right if number is negative
        ///// yPixels is the number at y-axis, it moves to the down if number is positive and it move to the up if number is in negative 
        ///// </summary>
        //public static void ScrollBy(int xPixels, int yPixels)
        //{
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)GetDriver();
        //    string javaScript = "window.scrollBy(" + xPixels + ", " + yPixels + ")";
        //    js.ExecuteScript(javaScript);
        //}
    }
}