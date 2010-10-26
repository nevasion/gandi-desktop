namespace GandiDesktop.Gandi.Services.Hosting
{
    public enum IpVersion
    {
        v4,
        v6
    }

    public class InterfaceCreation
    {
        public DataCenter DataCenter { get; set; }
        public IpVersion IpVersion { get; set; }
        public double? Bandwidth { get; set; }
    }
}