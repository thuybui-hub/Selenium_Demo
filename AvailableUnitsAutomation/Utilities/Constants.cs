using System;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using System.Configuration;
using System.Collections.Generic;
using System.IO;

namespace AvailableUnitsAutomation.Utilities
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

        public static readonly string AdminPassword = ConfigurationManager.AppSettings["adminPassword"];
        public static readonly string AdminUserName = ConfigurationManager.AppSettings["adminUserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["userPassword"];
        public static readonly string UserName = ConfigurationManager.AppSettings["userName"];
        public static string CommonPassword = ConfigurationManager.AppSettings["commonPassword"];
        public static string EDUser = ConfigurationManager.AppSettings["EDUser"];
        public static string RDOUser = ConfigurationManager.AppSettings["RDOUser"];
        public static string RevenueManagementUser = ConfigurationManager.AppSettings["RevenueManagementUser"];
        public static string DVPUser = ConfigurationManager.AppSettings["DVPUser"];
        public static string COOUser = ConfigurationManager.AppSettings["COOUser"];
        public static string BOMUser = ConfigurationManager.AppSettings["BOMUser"];
        public static string SalesUser = ConfigurationManager.AppSettings["SalesUser"];
        public static readonly string DataPath = ConfigurationManager.AppSettings["dataPath"];
        public static readonly string JsonDataPath = ConfigurationManager.AppSettings["jsonDataPath"];

        public static readonly string EDUserInfo = Path.Combine(Constants.JsonDataPath, "EDUserInfo.json");

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
