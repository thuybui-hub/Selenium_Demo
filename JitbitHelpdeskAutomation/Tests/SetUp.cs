using NUnit.Framework;
using JitbitHelpdeskAutomation.Utilities;

namespace JitbitHelpdeskAutomation.UITests
{
    [SetUpFixture]
    public class SetUp
    {
        [OneTimeSetUp]
        public void BeforeAll()
        {
            Constants.SetUIEnvVariables();
            ExtentManager.GetReporter();           
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            ExtentManager.FlushReporter();
        }
    }
}