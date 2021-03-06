﻿using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.Helpers;
using SeleniumCSharp.Core.Utilities;
using AvailableUnitsAutomation.Utilities;
using static NUnit.Framework.TestContext;
using AvailableUnitsAutomation.PageObject;
using InfectionLogAutomation.PageObject;

namespace AvailableUnitsAutomation.Tests
{
    [TestFixture]
    public class TestBase
    {
        #region Properties
        private LoginPage loginPage;
        private CommonPage commonPage;
        private HomePage homePage;
        private SearchPage searchPage;
        private NewChangeRequestPage newChangeRequestPage;
        #endregion

        #region Declare pages
        public LoginPage LoginPage
        {
            get
            {
                if (loginPage == null)
                {
                    loginPage = new LoginPage();
                }
                return loginPage;
            }
        }
        public CommonPage CommonPage
        {
            get
            {
                if (commonPage == null)
                {
                    commonPage = new CommonPage();
                }
                return commonPage;
            }
        }
        public HomePage HomePage
        {
            get
            {
                if (homePage == null)
                {
                    homePage = new HomePage();
                }
                return homePage;
            }
        }

        public SearchPage SearchPage
        {
            get
            {
                if (searchPage == null)
                {
                    searchPage = new SearchPage();
                }
                return searchPage;
            }
        }

        public NewChangeRequestPage NewChangeRequestPage
        {
            get
            {
                if (newChangeRequestPage == null)
                {
                    newChangeRequestPage = new NewChangeRequestPage();
                }
                return newChangeRequestPage;
            }
        }
        #endregion

        [SetUp]
        public void SetUp()
        {
            Constants.SetUIEnvVariables();
            //Create extent test                  
            extentTest = ExtentTestManager.StartTest(TestContextInfo.TestName, TestContextInfo.Description,
                TestContextInfo.Categories);

            Log = new Logger(Utils.GetLogFile(TestContextInfo.CurrentContext), TestContextInfo.CurrentContext,
                extentTest);
            Log.StartTest();

            // Delete old log files
            FileUtils.DeleteFilesOlderThan(Constants.LogRetentionDays, Utils.GetLogFolder(), Constants.LogExtension);
            FileUtils.DeleteFilesOlderThan(Constants.LogRetentionDays, Utils.GetScreenshotFolder(),
                Constants.ImgExtension);

            //Create new web driver
            DriverUtils.CreateDriver(new DriverProperties(Constants.ConfigFilePath,
                Constants.Driver));
            DriverUtils.Maximize();
            //DriverUtils.GoToUrl(Constants.Url);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContextInfo.Result.Outcome.Status != TestStatus.Passed)
                {
                    var screenshotPath = Utils.GeScreenshotFile(TestContextInfo.CurrentContext);
                    DriverUtils.TakeScreenshot(screenshotPath);
                    AddTestAttachment(screenshotPath);
                    Log.GetBrowserLog();

                    //Add screenshot for extents report                    
                    extentTest.Fail(MarkupHelper.CreateCodeBlock(TestContextInfo.Result.Message));
                    extentTest.AddScreenCaptureFromPath(screenshotPath);
                }

                else if (TestContextInfo.Result.Outcome.Status == TestStatus.Passed)
                {
                    extentTest.Pass("Passed");
                }

                AddTestAttachment(Log.LogPath);
                ExtentTestManager.EndTest();
                DriverUtils.CloseDrivers();
            }
            catch (Exception e)
            {
                Log.Error(e);
                extentTest.Error(e);
            }

            Log.EndTest();
        }

        //private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;
        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        protected Logger Log { get; set; }
        private ExtentTest extentTest;
    }
}
