using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Model
{
    public class InterfaceResource : IResource
    {
        public string Name { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.Interface; }
        }

        public InterfaceResource(Interface iface)
        {
            this.Name = string.Format("{0} Mbits", iface.Bandwidth);
        }
    }
}