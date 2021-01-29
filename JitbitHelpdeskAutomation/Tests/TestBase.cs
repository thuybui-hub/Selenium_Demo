using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.Helpers;
using SeleniumCSharp.Core.Utilities;
using JitbitHelpdeskAutomation.Utilities;
using static NUnit.Framework.TestContext;
using JitbitHelpdeskAutomation.PageObject.Login;
using JitbitHelpdeskAutomation.PageObject.Common;
using JitbitHelpdeskAutomation.PageObject.Home;
using JitbitHelpdeskAutomation.PageObject.Incident;
using JitbitHelpdeskAutomation.PageObject.Ticket;
using JitbitHelpdeskAutomation.PageObject.UserDetails;

namespace JitbitHelpdeskAutomation.UITests
{
    [TestFixture]
    public class TestBase
    {
        #region Properties
        private LoginPage loginPage;
        private CommonPage pvCommonPage;
        private HomePage pvHomePage;
        private IncidentPage pvIncidentPage;
        private TicketNewPage pvTicketNewPage;
        private TicketDetailsPage pvTicketDetailsPage;
        private UserDetailsPage pvUserDetailsPage;
        #endregion

        #region Declare pages
        public UserDetailsPage UserDetailsPage
        {
            get
            {
                if (pvUserDetailsPage == null)
                {
                    pvUserDetailsPage = new UserDetailsPage();
                }
                return pvUserDetailsPage;
            }
        }

        public TicketDetailsPage TicketDetailsPage
        {
            get
            {
                if (pvTicketDetailsPage == null)
                {
                    pvTicketDetailsPage = new TicketDetailsPage();
                }
                return pvTicketDetailsPage;
            }
        }

        public TicketNewPage TicketNewPage
        {
            get
            {
                if (pvTicketNewPage == null)
                {
                    pvTicketNewPage = new TicketNewPage();
                }
                return pvTicketNewPage;
            }
        }

        public IncidentPage IncidentPage
        {
            get
            {
                if (pvIncidentPage == null)
                {
                    pvIncidentPage = new IncidentPage();
                }
                return pvIncidentPage;
            }
        }

        public HomePage HomePage
        {
            get
            {
                if (pvHomePage == null)
                {
                    pvHomePage = new HomePage();
                }
                return pvHomePage;
            }
        }

        public CommonPage CommonPage
        {
            get
            {
                if (pvCommonPage == null)
                {
                    pvCommonPage = new CommonPage();
                }
                return pvCommonPage;
            }
        }

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