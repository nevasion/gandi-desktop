using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Model
{
    public class VirtualMachineResource : IResource
    {
        public string Name
        {
            get;
            private set;
        }

        public ResourceType Type
        {
            get { return ResourceType.VirtualMachine; }
        }

        public VirtualMachineResource(VirtualMachine virtualMachine)
        {
            this.Name = virtualMachine.Hostname;
        }
    }
}