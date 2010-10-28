namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachineUpdate
    {
        public int? MaxMemory { get; set; }
        public int? Shares { get; set; }
        public int? Memory { get; set; }
        public bool? IsRescueConsoleActive { get; set; }
        public string UnixPassword { get; set; }
    }
}