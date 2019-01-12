using System.Configuration;

namespace BotCore.Util
{
    public static class ConfigReader
    {
        public static string GetStringValue(string key) => ConfigurationManager.AppSettings[key];
        public static int GetIntegerValue(string key) => int.Parse(ConfigurationManager.AppSettings[key]);
        public static bool GetBooleanValue(string key) => bool.Parse(ConfigurationManager.AppSettings[key]);
    }
}
