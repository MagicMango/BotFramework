using System.Configuration;

namespace WebServer.Handler
{
    /// <summary>
    /// As requested from Thomas485
    /// </summary>
    public static class UglyStuff
    {
        public static void ChangeSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
