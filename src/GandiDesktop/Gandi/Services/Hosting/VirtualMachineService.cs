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

        public VirtualMachineOperation Create(VirtualMachineCreation virtualMachineCreation)
        {
            Mapping.VirtualMachineCreation mappingVirtualMachineCreation = new Mapping.VirtualMachineCreation(virtualMachineCreation);

            return new VirtualMachineOperation(this.service.VirtualMachineCreate(this.apiKey, mappingVirtualMachineCreation), null);
        }

        public VirtualMachineOperation CreateFrom(VirtualMachineCreation virtualMachineCreation, DiskCreation diskCreation, Disk sourceDisk)
        {
            Mapping.VirtualMachineCreation mappingVirtualMachineCreation = new Mapping.VirtualMachineCreation(virtualMachineCreation);
            Mapping.DiskCreation mappingDiskCreation = new Mapping.DiskCreation(diskCreation);

            return new VirtualMachineOperation(this.service.VirtualMachineCreateFrom(this.apiKey, mappingVirtualMachineCreation, mappingDiskCreation, sourceDisk.Id), null);
        }

        public VirtualMachineOperation Update(VirtualMachine virtualMachine, VirtualMachineUpdate virtualMachineUpdate)
        {
            Mapping.VirtualMachineUpdate mappingVirtualMachineUpdate = new Mapping.VirtualMachineUpdate(virtualMachineUpdate);

            return new VirtualMachineOperation(this.service.VirtualMachineUpdate(this.apiKey, virtualMachine.Id, mappingVirtualMachineUpdate), virtualMachine);
        }

        public VirtualMachineOperation Delete(VirtualMachine virtualMachine)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineDelete(this.apiKey, virtualMachine.Id), virtualMachine);
        }

        public VirtualMachineOperation Start(VirtualMachine virtualMachine)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineStart(this.apiKey, virtualMachine.Id), virtualMachine);
        }

        public VirtualMachineOperation Stop(VirtualMachine virtualMachine)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineStop(this.apiKey, virtualMachine.Id), virtualMachine);
        }

        public VirtualMachineOperation Reboot(VirtualMachine virtualMachine)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineReboot(this.apiKey, virtualMachine.Id), virtualMachine);
        }

        public VirtualMachine Single(int virtualMachineId, DataCenter[] dataCenters, Interface[] interfaces, Disk[] disks)
        {
            Mapping.VirtualMachine mappingVirtualMachine = this.service.VirtualMachineInfo(this.apiKey, virtualMachineId);

            if (mappingVirtualMachine != null)
                return new VirtualMachine(mappingVirtualMachine, dataCenters, interfaces, disks);
            else
                return null;
        }

        public VirtualMachineOperation AttachDisk(VirtualMachine virtualMachine, Disk disk)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineAttachDisk(this.apiKey, virtualMachine.Id, disk.Id), virtualMachine, disk);
        }

        public VirtualMachineOperation DetachDisk(VirtualMachine virtualMachine, Disk disk)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineDetachDisk(this.apiKey, virtualMachine.Id, disk.Id), virtualMachine, disk);
        }

        public VirtualMachineOperation AttachInterface(VirtualMachine virtualMachine, Interface iface)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineAttachInterface(this.apiKey, virtualMachine.Id, iface.Id), virtualMachine, iface);
        }

        public VirtualMachineOperation DetachInterface(VirtualMachine virtualMachine, Interface iface)
        {
            return new VirtualMachineOperation(this.service.VirtualMachineDetachInterface(this.apiKey, virtualMachine.Id, iface.Id), virtualMachine, iface);
        }

        public VirtualMachine[] List(DataCenter[] dataCenters, Interface[] interfaces, Disk[] disks)
        {
            Mapping.VirtualMachine[] mappingVirtualMachines = this.service.VirtualMachineList(this.apiKey);

            List<VirtualMachine> virtualMachineList = new List<VirtualMachine>();
            foreach (Mapping.VirtualMachine mappingVirtualMachine in mappingVirtualMachines)
                virtualMachineList.Add(new VirtualMachine(mappingVirtualMachine, dataCenters, interfaces, disks));

            VirtualMachine[] virtualMachines = virtualMachineList.ToArray();

            return virtualMachines;
        }
    }
}