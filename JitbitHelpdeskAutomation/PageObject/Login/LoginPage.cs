using JitbitHelpdeskAutomation.PageObject.Common;
using JitbitHelpdeskAutomation.Utilities;
using OpenQA.Selenium;
using SeleniumCSharp.Core.DriverWrapper;
using SeleniumCSharp.Core.ElementWrapper;

namespace JitbitHelpdeskAutomation.PageObject.Login
{
    public class LoginPage
    {
        private readonly TextBox txtUserName;
        private readonly TextBox txtPassword;
        private readonly CheckBox chkRememberMe;
        private readonly Label lblRememberMe;
        private readonly Button btnLogin;
        private readonly CommonPage commonPage = new CommonPage();

        public LoginPage()
        {
            txtUserName = new TextBox(By.Id("user_name"));
            txtPassword = new TextBox(By.Id("user_password"));
            chkRememberMe = new CheckBox(By.Id("remember_me"));
            lblRememberMe = new Label(By.XPath("//span[input[@id='remember_me']]"));
            btnLogin = new Button(By.Id("sysverb_login"));
        }

        public void Login(string user = null, string password = null)
        {
            if (Constants.Driver.Contains("-chrome"))
            {
                string url = CreateByPassAuthenticationLink(user, password, Constants.Url);
                DriverUtils.GoToUrl(url);
            }
            else
            {
                if (user != null && password != null)
                {
                    DriverUtils.wait(1);
                    System.Windows.Forms.SendKeys.SendWait(user);
                    System.Windows.Forms.SendKeys.SendWait("{TAB}");
                    System.Windows.Forms.SendKeys.SendWait(password);
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                }
            }
            DriverUtils.WaitForPageLoad();
        }

        public void LoginToServiceNow(string user = null, string password = null)
        {
            commonPage.SwitchToMainIFrame();
            txtUserName.SendKeys(user);
            txtPassword.SendKeys(password);
            if (chkRememberMe.IsChecked())
                lblRememberMe.Click();
            btnLogin.Click();
            btnLogin.WaitForInvisible();
            DriverUtils.WaitForPageLoad();
        }

        public string CreateByPassAuthenticationLink(string user = null, string password = null, string url = null)
        {
            if (user.Contains("fve"))
                user = user.Replace("fve\\", "");
            return url.Insert(7, user + ":" + password + "@");
        }
    }
}