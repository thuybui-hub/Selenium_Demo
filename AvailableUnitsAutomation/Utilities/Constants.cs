using System;
using NUnit.Framework;
using SeleniumCSharp.Core.Utilities;
using System.Configuration;
using System.Collections.Generic;

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
        public static readonly string DashboardPath = ConfigurationManager.AppSettings["dashboardPath"];
        public static readonly string DashboardPathActive = ConfigurationManager.AppSettings["dashboardPathActive"];
        public static readonly string NewTeamLogEntryPath = ConfigurationManager.AppSettings["newTeamLogEntryPath"];
        public static readonly string NewResidentLogEntryPath = ConfigurationManager.AppSettings["newResidentLogEntryPath"];
        public static readonly string NewClientLogEntryPath = ConfigurationManager.AppSettings["newClientLogEntryPath"];
        public static readonly string NewTeamBulkInsertPath = ConfigurationManager.AppSettings["newTeamBulkInsertPath"];
        public static readonly string NewResidentBulkInsertPath = ConfigurationManager.AppSettings["newResidentBulkInsertPath"];
        public static readonly string BulkEditTeamPath = ConfigurationManager.AppSettings["bulkEditTeamPath"];
        public static readonly string BulkEditResidentPath = ConfigurationManager.AppSettings["bulkEditResidentPath"];
        public static readonly string ResidentCaseLogReporttPath = ConfigurationManager.AppSettings["residentCaseLogReporttPath"];
        public static string CommonPassword = ConfigurationManager.AppSettings["commonPassword"];
        public static string TeamAdminUser = ConfigurationManager.AppSettings["teamAdminUser"];
        public static string ResidentAdminUser = ConfigurationManager.AppSettings["residentAdminUser"];
        public static string AgeilityProfileMembers = ConfigurationManager.AppSettings["ageilityProfileMembers"];
        public static string TeamCommunitySubmittorUser = ConfigurationManager.AppSettings["teamCommunitySubmittorUser"];
        public static string TeamCommunityAdminUser = ConfigurationManager.AppSettings["teamCommunityAdminUser"];
        public static string ResidentCommunityAdminUser = ConfigurationManager.AppSettings["residentCommunityAdminUser"];
        public static string ResidentCommunitySubmittorUser = ConfigurationManager.AppSettings["residentCommunitySubmittorUser"];
        public static string ClientSubmittorUser = ConfigurationManager.AppSettings["​clientSubmittorUser"];
        public static string ResidentReadOnlyUser = ConfigurationManager.AppSettings["residentReadOnlyUser"];
        public static readonly string DataPath = ConfigurationManager.AppSettings["dataPath"];

        public static List<string> TestStatus = new List<string> { "Not Tested", "Tested - Confirmed", "Tested - Negative", "Tested - Pending", "Inconclusive" };
        public static List<string> Disposition = new List<string> { "Resolved Negative", "Quarantined", "Hospitalized", "Expired", "Recovered", "Transferred/Discharged", "Not Quarantined" };
        public static List<string> DashboardColumnsHeader = new List<string> { "Last Updated", "Division", "Region", "BU", "Community", "ID", "Name", "Resident LOB", "Infection Type", "Onset Date", "Test Status", "Disposition", "Entry Type", "Delete" };

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
