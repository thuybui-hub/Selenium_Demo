using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_25307: TestBase
    {
        [Test]
        [Description("IO Log: All Record Search Updates: Search page UI")]
        public void PBI_25307_AT_25312()
        {
            #region Test Data
            
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("Verify that Home page displays correctly");
            Assert.IsTrue(HomePage.IsHomePageDisplayed(), "Home page displays incorrectly");

            Log.Info("Verify that default search values are correct");
            Assert.IsTrue(HomePage.AreDefaultSearchValueCorrect(), "The default search values display incorrectly");

            bool test = HomePage.IsCommunitySearchable();
            #endregion
        }
    }
}
