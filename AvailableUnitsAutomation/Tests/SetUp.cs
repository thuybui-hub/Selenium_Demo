using NUnit.Framework;
using AvailableUnitsAutomation.Utilities;

namespace AvailableUnitsAutomation.Tests
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
