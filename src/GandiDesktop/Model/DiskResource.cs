using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Model
{
    public class DiskResource : IResource
    {
        public string Name
        {
            get;
            private set;
        }

        public ResourceType Type
        {
            get { return ResourceType.Disk; }
        }

        public DiskResource(Disk disk)
        {
            this.Name = disk.Name;
        }
    }
}