using System;
using System.ComponentModel;
using System.Configuration;

namespace Education.Helpers
{
    public static class ConfigHelper
    {
        public static T GetAppSettingValue<T>(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new ArgumentNullException(key);
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(ConfigurationManager.AppSettings[key]);
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
