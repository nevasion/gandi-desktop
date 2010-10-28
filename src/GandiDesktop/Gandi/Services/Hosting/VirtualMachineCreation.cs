namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachineCreation
    {
        public string Hostname { get; set; }
        public int Memory { get; set; }
        public int Shares { get; set; }
        public DataCenter DataCenter { get; set; }
        public Disk SystemDisk { get; set; }
        public IpVersion IpVersion { get; set; }
        public bool IsGandiAI { get; set; }
        public string UnixLogin { get; set; }
        public string UnixPassword { get; set; }
    }
}