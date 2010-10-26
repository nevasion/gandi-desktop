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

        public void Create(VirtualMachineCreation virtualMachineCreation)
        {
            Mapping.VirtualMachineCreation mappingVirtualMachineCreation = new Mapping.VirtualMachineCreation(virtualMachineCreation);

            object mappingOperation = this.service.vm_create(this.apiKey, mappingVirtualMachineCreation);
        }

        public void CreateFrom(VirtualMachineCreation virtualMachineCreation, DiskCreation diskCreation, Disk sourceDisk)
        {
            Mapping.VirtualMachineCreation mappingVirtualMachineCreation = new Mapping.VirtualMachineCreation(virtualMachineCreation);
            Mapping.DiskCreation mappingDiskCreation = new Mapping.DiskCreation(diskCreation);

            object mappingOperation = this.service.vm_create_from(this.apiKey, mappingVirtualMachineCreation, mappingDiskCreation, sourceDisk.Id);
        }

        public void Update(VirtualMachine virtualMachine, VirtualMachineUpdate virtualMachineUpdate)
        {
            Mapping.VirtualMachineUpdate mappingVirtualMachineUpdate = new Mapping.VirtualMachineUpdate(virtualMachineUpdate);

            object mappingOperation = this.service.vm_update(this.apiKey, virtualMachine.Id, mappingVirtualMachineUpdate);
        }

        public void Delete(VirtualMachine virtualMachine)
        {
            object mappingOperation = this.service.vm_delete(this.apiKey, virtualMachine.Id);
        }

        public void Start(VirtualMachine virtualMachine)
        {
            object mappingOperation = this.service.vm_start(this.apiKey, virtualMachine.Id);
        }

        public void Stop(VirtualMachine virtualMachine)
        {
            object mappingOperation = this.service.vm_stop(this.apiKey, virtualMachine.Id);
        }

        public void Reboot(VirtualMachine virtualMachine)
        {
            object mappingOperation = this.service.vm_reboot(this.apiKey, virtualMachine.Id);
        }

        public void AttachDisk(VirtualMachine virtualMachine, Disk disk)
        {
            object mappingOperation = this.service.vm_disk_attach(this.apiKey, virtualMachine.Id, disk.Id);
        }

        public void DetachDisk(VirtualMachine virtualMachine, Disk disk)
        {
            object mappingOperation = this.service.vm_disk_detach(this.apiKey, virtualMachine.Id, disk.Id);
        }

        public void AttachInterface(VirtualMachine virtualMachine, Interface iface)
        {
            object mappingOperation = this.service.vm_iface_attach(this.apiKey, virtualMachine.Id, iface.Id);
        }

        public void DetachInterface(VirtualMachine virtualMachine, Interface iface)
        {
            object mappingOperation = this.service.vm_iface_detach(this.apiKey, virtualMachine.Id, iface.Id);
        }

        public VirtualMachine[] List()
        {
            return this.List(null, null, null);
        }

        public VirtualMachine[] List(Disk[] disks)
        {
            return this.List(null, null, disks);
        }

        public VirtualMachine[] List(Interface[] interfaces)
        {
            return this.List(null, interfaces, null);
        }

        public VirtualMachine[] List(Interface[] interfaces, Disk[] disks)
        {
            return this.List(null, interfaces, disks);
        }

        public VirtualMachine[] List(DataCenter[] dataCenters)
        {
            return this.List(dataCenters, null, null);
        }

        public VirtualMachine[] List(DataCenter[] dataCenters, Disk[] disks)
        {
            return this.List(dataCenters, null, disks);
        }

        public VirtualMachine[] List(DataCenter[] dataCenters, Interface[] interfaces)
        {
            return this.List(dataCenters, interfaces, null);
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