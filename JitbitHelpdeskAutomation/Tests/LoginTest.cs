using JitbitHelpdeskAutomation.PageObject.Common;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using JitbitHelpdeskAutomation.PageObject.Login;
using JitbitHelpdeskAutomation.DataObject;
using JitbitHelpdeskAutomation.PageObject.Home;
using SeleniumCSharp.Core.DriverWrapper;
using JitbitHelpdeskAutomation.Utilities;

namespace JitbitHelpdeskAutomation.UITests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginTest : TestBase
    {

        private readonly LoginPage loginPage = new LoginPage();
        private readonly LoginData loginData = JsonParser.Get<LoginData>();
        private readonly CommonPage commonPage = new CommonPage();
        private readonly HomePage homePage = new HomePage();
        private readonly TicketListPage ticketListPage = new TicketListPage();

        [Test]
        [Category("Smoke")]
        [Category("Login")]
        [Description("Login with valid user")]
        public void Login_ValidUser()
        {
            Log.Info("1. Go to Service Now site");
            DriverUtils.GoToUrl(Constants.Url);

            Log.Info("1. Login with valid user");
            loginPage.Login(loginData.ValidUser, loginData.ValidPassword);
        }
    }
}