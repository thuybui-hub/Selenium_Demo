using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.Helpers;
using SeleniumCSharp.Core.Utilities;
using InfectionLogAutomation.Utilities;
using static NUnit.Framework.TestContext;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.PageObject.Common;
using InfectionLogAutomation.PageObject.Home;
using InfectionLogAutomation.PageObject.LogEntry;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    public class TestBase
    {
        #region Properties
        private LoginPage loginPage;
        private CommonPage commonPage;
        private HomePage homePage;
        private LogEntryDetailPage logEntryDetailPage;
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

        public LogEntryDetailPage LogEntryDetailPage
        {
            get
            {
                if (logEntryDetailPage == null)
                {
                    logEntryDetailPage = new LogEntryDetailPage();
                }
                return logEntryDetailPage;
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

        protected Logger Log { get; set; }
        private ExtentTest extentTest;
    }
}
