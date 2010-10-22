using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachineService
    {
        private IHostingService service;
        private string apiKey;

        public VirtualMachineService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public VirtualMachine[] List(DataCenter[] dataCenters, Interface[] interfaces, Disk[] disks)
        {
            Mapping.VirtualMachine[] mappingVirtualMachines = this.service.vm_list(this.apiKey);

            List<VirtualMachine> virtualMachineList = new List<VirtualMachine>();
            foreach (Mapping.VirtualMachine mappingVirtualMachine in mappingVirtualMachines)
                virtualMachineList.Add(new VirtualMachine(mappingVirtualMachine, dataCenters, interfaces, disks));

            VirtualMachine[] virtualMachines = virtualMachineList.ToArray();

            return virtualMachines;
        }
    }
}