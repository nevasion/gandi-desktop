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

        public string Status { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.VirtualMachine; }
        }

        public IResourceDetail[] Details { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public VirtualMachineResource(VirtualMachine virtualMachine)
        {
            this.Name = virtualMachine.Hostname;
            this.Status = virtualMachine.State;

            List<IResourceDetail> details = new List<IResourceDetail>();

            details.Add(new StatusResourceDetail(virtualMachine));
            details.Add(new DataCenterResourceDetail(virtualMachine.DataCenter));

            foreach (Disk disk in virtualMachine.AttachedDisks)
            {
                DiskResourceDetail diskResourceDetail = new DiskResourceDetail(disk, virtualMachine);
                diskResourceDetail.DetailAction += (sender, e) => 
                {
                    if (this.DetailAction != null)
                        this.DetailAction(this, e);
                };

                details.Add(diskResourceDetail);
            }

            foreach (Interface iface in virtualMachine.AttachedInterfaces)
            {
                InterfaceResourceDetail ifaceResourceDetail = new InterfaceResourceDetail(iface, virtualMachine);
                ifaceResourceDetail.DetailAction += (sender, e) =>
                {
                    if (this.DetailAction != null)
                        this.DetailAction(this, e);
                };

                details.Add(ifaceResourceDetail);
            }

            details.Add(new TextResourceDetail(VirtualMachineResource.CPUName, String.Format(VirtualMachineResource.CPUValueTemplate, virtualMachine.Cores, (virtualMachine.Cores == 1 ? null : VirtualMachineResource.PluralSuffix))));
            details.Add(new TextResourceDetail(VirtualMachineResource.MemoryName, String.Format(VirtualMachineResource.MemoryValueTemplate, virtualMachine.Memory)));

            this.Details = details.ToArray();
        }
    }
}