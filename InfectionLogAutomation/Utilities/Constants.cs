using System;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;

namespace InfectionLogAutomation.Utilities
{
    public class Constants
    {
        public static string ConfigFilePath;
        public static int LogRetentionDays = 1;
        public static string LogExtension = ".log";
        public static string ImgExtension = ".png";
        public static string Driver; // can be chrome,ie,firefox
        public static string Url;
        public static string SNUrl;

        //Setting variables via test context
        public static void SetUIEnvVariables()
        {
            ConfigFilePath = FileUtils.GetBasePath() + GetString("ConfigPath");
            Driver = GetString("Driver");
            Url = GetString("Url");
            SNUrl = GetString("SNUrl");
        }

        public static string GetString(string property)
        {
            if (TestContext.Parameters == null)
                throw new ArgumentException(
                    "Property does not exist, does not have a value, or a test setting is not selected");
            return TestContext.Parameters[property];
        }
    }
}
