using NUnit.Framework;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;
using System;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]    
    public class PBI_23953: TestBase
    {
        [Test]
        [Description("IO Log - New Resident - Add PCC Facility Name")]
        public void PBI_23953_AT_23966()
        {
            #region Test Data
            Random r = new Random();
            List<string> list;
            string region, community;
            #endregion

            #region Main Steps
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(Constants.AdminUserName, Constants.AdminPassword);

            Log.Info("3. Go to New Log Entry -> Resident");
            HomePage.SelectMenuItem(Constants.NewResidentLogEntryPath);

            Log.Info("4. Select a region");
            list = LogEntryDetailPage.GetItemsFromControlList(Fields.region);
            region = list[r.Next(0, list.Count - 1)];
            LogEntryDetailPage.txtRegion.SendKeys(region);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");

            Log.Info("5. Select a community");
            list = LogEntryDetailPage.GetItemsFromControlList(Fields.community);
            community = list[r.Next(0, list.Count - 1)];
            LogEntryDetailPage.txtCommunity.SendKeys(community);
            DriverUtils.wait(1);
            System.Windows.Forms.SendKeys.SendWait("{Enter}");

            Log.Info("Verify that resident include column that displays PCC facility");
            LogEntryDetailPage.txtEmployee.Click();            
            list = LogEntryDetailPage.residentHeaderTemplate.GetItems();
            Assert.IsTrue(list.Contains("PCC FACILITY"), "Resident does not include column displaying PCC facility");
            #endregion
        }
    }
}
