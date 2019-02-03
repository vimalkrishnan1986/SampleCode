using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Fabric;
using System.Text;

namespace EducationSystem.Cloud.Common.AppSettings
{
    public sealed class AppSettingService : IAppSettingService
    {
        private const string AppSettingSection = "AppSettings";
        private const string ConString = "ConString";
        private readonly ServiceContext _serviceContext;

        public string ConnectionString
        {
            get; private set;
        }

        public AppSettingService(ServiceContext serviceContext)
        {
            _serviceContext = serviceContext ?? throw new ArgumentNullException(nameof(serviceContext));
            LoadSettings();
        }


        private void LoadSettings()
        {
            ConnectionString = ReadSettingValue<string>(AppSettingSection, ConString);
        }


        private T ReadSettingValue<T>(string settingName, string key)
        {
            var configurationPackage = _serviceContext.CodePackageActivationContext.GetConfigurationPackageObject("Config");

            if (configurationPackage == null)
            {
                throw new IndexOutOfRangeException("Configuration Section is not found");

            }
            var settings = configurationPackage.Settings.Sections[settingName];

            if (settings == null)
            {
                throw new ArgumentNullException(settingName);
            }

            if (settings.Parameters == null)
            {
                throw new NullReferenceException(nameof(settings.Parameters));
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(settings.Parameters[key].Value);
        }
    }
}
