using System.Collections.Generic;
using GandiDesktop.Gandi.Services.Hosting;
using System;

namespace GandiDesktop.Presentation.Model
{
    public class VirtualMachineResource : IResource
    {
        private const string PluralSuffix = "s";
        private const string CPUName = "CPU";
        private const string CPUValueTemplate = "{0} Core{1}";
        private const string MemoryName = "RAM";
        private const string MemoryValueTemplate = "{0} Mo";

        public string Name { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.VirtualMachine; }
        }

        public IResourceDetail[] Details { get; private set; }

        public VirtualMachineResource(VirtualMachine virtualMachine)
        {
            this.Name = virtualMachine.Hostname;

            List<IResourceDetail> details = new List<IResourceDetail>();

            details.Add(new DataCenterResourceDetail(virtualMachine.DataCenter));
            foreach (Disk disk in virtualMachine.AttachedDisks)
                details.Add(new DiskResourceDetail(disk));
            foreach (Interface iface in virtualMachine.AttachedInterfaces)
                details.Add(new InterfaceResourceDetail(iface));
            details.Add(new TextResourceDetail(VirtualMachineResource.CPUName, String.Format(VirtualMachineResource.CPUValueTemplate, virtualMachine.Cores, (virtualMachine.Cores == 1 ? null : VirtualMachineResource.PluralSuffix))));
            details.Add(new TextResourceDetail(VirtualMachineResource.MemoryName, String.Format(VirtualMachineResource.MemoryValueTemplate, virtualMachine.Memory)));
            
            this.Details = details.ToArray();
        }
    }
}