using System.Configuration;

namespace BotCore.Util
{
    /// <summary>
    /// Class which wrapes ConfigurationManager.AppSettings
    /// <see cref="ConfigurationManager.AppSettings"/>
    /// </summary>
    public static class ConfigReader
    {
        /// <summary>
        /// Return a string configuration value
        /// </summary>
        /// <param name="key">configuration value</param>
        /// <returns>string configuration value</returns>
        public static string GetStringValue(string key) => ConfigurationManager.AppSettings[key];
        /// <summary>
        /// Return a int configuration value
        /// </summary>
        /// <param name="key">configuration value</param>
        /// <returns>int configuration value</returns>
        public static int GetIntegerValue(string key) => int.Parse(ConfigurationManager.AppSettings[key]);
        /// <summary>
        /// Return a boolean configuration value
        /// </summary>
        /// <param name="key">configuration value</param>
        /// <returns>boolean configuration value</returns>
        public static bool GetBooleanValue(string key) => bool.Parse(ConfigurationManager.AppSettings[key]);
    }
}
