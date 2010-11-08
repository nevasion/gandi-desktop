using System;
using System.Windows;

namespace GandiDesktop.Presentation
{
    public class ResourceSettingInfo
    {
        private const char InfoSeparator = ';';
        private const string ToStringTemplate = "{0};{1},{2};{3}";

        public const char ResourceSeparator = '/';

        public string Name { get; set; }
        public Point Location { get; set; }
        public int ZIndex { get; set; }

        public static ResourceSettingInfo Parse(string source)
        {
            try
            {
                string[] tokens = source.Split(ResourceSettingInfo.InfoSeparator);

                ResourceSettingInfo info = new ResourceSettingInfo()
                {
                    Name = tokens[0],
                    Location = Point.Parse(tokens[1]),
                    ZIndex = Int32.Parse(tokens[2])
                };

                return info;
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            return String.Format(
                ResourceSettingInfo.ToStringTemplate,
                this.Name,
                this.Location.X,
                this.Location.Y,
                this.ZIndex);
        }
    }
}