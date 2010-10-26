using System.Collections;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DiskUpdate
    {
        public string Name { get; set; }
        public int? Size { get; set; }
        public string KernelVersion { get; set; }
        public string CommandLine { get; set; }
    }
}