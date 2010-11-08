using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace GandiDesktop.Presentation
{
    public static class Settings
    {
        static class Keys
        {
            public const string WindowLocation = "window_location";
            public const string WindowSize = "window_size";
            public const string ResourceInfos = "resource_infos";
        }

        static class Values
        {
            [SettingsKey(Keys.WindowLocation)]
            public static string WindowLocation { get; set; }

            [SettingsKey(Keys.WindowSize)]
            public static string WindowSize { get; set; }

            [SettingsKey(Keys.ResourceInfos)]
            public static string ResourceInfos { get; set; }
        }

        private const string KeyValueSeparator = "=";
        private const string LineTemplate = "{0}{1}{2}";
        private const string FileName = "gandidesktop.settings.ini";

        public static Point? WindowLocation
        {
            get
            {
                Point? location = null;

                if (!String.IsNullOrEmpty(Settings.Values.WindowLocation))
                {
                    try { location = Point.Parse(Settings.Values.WindowLocation); }
                    catch { }
                }

                return location;
            }
            set
            {
                Settings.Values.WindowLocation = (value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
            }
        }

        public static Size? WindowSize
        {
            get
            {
                Size? size = null;

                if (!String.IsNullOrEmpty(Settings.Values.WindowSize))
                {
                    try { size = Size.Parse(Settings.Values.WindowSize); }
                    catch { }
                }

                return size;
            }
            set
            {
                Settings.Values.WindowSize = (value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
            }
        }

        public static ResourceSettingInfo[] ResourceInfos
        {
            get
            {
                List<ResourceSettingInfo> infoList = new List<ResourceSettingInfo>();

                if (!String.IsNullOrEmpty(Settings.Values.ResourceInfos))
                {
                    string[] resources = Settings.Values.ResourceInfos.Split(
                        new char[] { ResourceSettingInfo.ResourceSeparator },
                        StringSplitOptions.RemoveEmptyEntries);

                    foreach (string resource in resources)
                    {
                        ResourceSettingInfo resourceInfo = ResourceSettingInfo.Parse(resource);
                        if (resourceInfo != null)
                            infoList.Add(resourceInfo);
                    }
                }

                return infoList.ToArray();
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    StringBuilder resourceInfos = new StringBuilder();

                    foreach (ResourceSettingInfo resourceInfo in value)
                    {
                        resourceInfos.Append(resourceInfo);
                        resourceInfos.Append(ResourceSettingInfo.ResourceSeparator);
                    }

                    Settings.Values.ResourceInfos = resourceInfos.ToString();
                }
                else
                {
                    Settings.Values.ResourceInfos = null;
                }
            }
        }

        static Settings()
        {
            Settings.Load();
        }

        public static void Save()
        {
            PropertyInfo[] properties = typeof(Settings.Values).GetProperties();

            using (FileStream stream = new FileStream(Settings.FileName, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    foreach (PropertyInfo property in properties)
                    {
                        object[] propertyAttributes = property.GetCustomAttributes(true);
                        if (propertyAttributes != null && propertyAttributes.Length > 0)
                        {
                            SettingsKeyAttribute settingsKeyAttribute = propertyAttributes.OfType<SettingsKeyAttribute>().FirstOrDefault();
                            if (settingsKeyAttribute != null && !String.IsNullOrEmpty(settingsKeyAttribute.Key))
                            {
                                string key = settingsKeyAttribute.Key;
                                string value = property.GetValue(null, null) as string;

                                writer.WriteLine(String.Format(Settings.LineTemplate, key, Settings.KeyValueSeparator, value));
                            }
                        }
                    }
                }
            }
        }

        static void Load()
        {
            PropertyInfo[] properties = typeof(Settings.Values).GetProperties();

            if (File.Exists(Settings.FileName))
            {
                using (StreamReader reader = new StreamReader(Settings.FileName))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        int indexOfSeparator = line.IndexOf(Settings.KeyValueSeparator);

                        string key = line.Substring(0, indexOfSeparator);
                        string value = line.Substring(indexOfSeparator + Settings.KeyValueSeparator.Length);

                        foreach (PropertyInfo property in properties)
                        {
                            object[] propertyAttributes = property.GetCustomAttributes(true);
                            if (propertyAttributes != null && propertyAttributes.Length > 0)
                            {
                                SettingsKeyAttribute settingsKeyAttribute = propertyAttributes.OfType<SettingsKeyAttribute>().FirstOrDefault();
                                if (settingsKeyAttribute != null && key == settingsKeyAttribute.Key)
                                {
                                    property.SetValue(null, value, null);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}