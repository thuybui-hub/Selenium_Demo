using NUnit.Framework;
using InfectionLogAutomation.Utilities;

namespace InfectionLogAutomation.Tests
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
