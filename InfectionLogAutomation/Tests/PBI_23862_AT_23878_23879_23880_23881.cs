using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using InfectionLogAutomation.PageObject.Login;
using InfectionLogAutomation.DataObject;
using InfectionLogAutomation.Utilities;
using SeleniumCSharp.Core.DriverWrapper;
using System.Collections.Generic;

namespace InfectionLogAutomation.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class PBI_23862_AT_23878_23879_23880_23881 : TestBase
    {
        private readonly LoginData loginData = JsonParser.Get<LoginData>();

        [Test]
        [Description("Navigation: General UI")]
        public void PBI_23862_AT_23878()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("Verify that Infectious Outbreak page shows");
            CommonPage.IsInfectionLogPageDiplayed();

            Log.Info("Verify that there are 3 tabs on the top menu bar: Home, New Log Entry, Reports; Home is default tab");
            CommonPage.AreThere5TabsOnMainMenu();
        }

        [Test]
        [Description("Navigation: Home tab")]
        public void PBI_23862_AT_23879()
        {
            Log.Info("1. Launch the site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("2. Login with valid user");
            LoginPage.Login(loginData.ValidUser, loginData.ValidPassword);

            Log.Info("Verify that Infectious Outbreak page shows");
            CommonPage.IsInfectionLogPageDiplayed();

            Log.Info("Verify that there are 3 tabs on the top menu bar: Home, New Log Entry, Reports; Home is default tab");
            CommonPage.AreThere5TabsOnMainMenu();
        }
    }
}
