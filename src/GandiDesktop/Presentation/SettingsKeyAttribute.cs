using System;

namespace GandiDesktop.Presentation
{
    class SettingsKeyAttribute : Attribute
    {
        public string Key { get; set; }

        public SettingsKeyAttribute(string key)
        {
            this.Key = key;
        }
    }
}