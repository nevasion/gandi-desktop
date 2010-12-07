using System;
using System.Collections.Generic;
using System.Linq;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachine
    {
        public int Id { get; set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; set; }
        public string Hostname { get; set; }
        public string Description { get; set; }
        public VirtualMachineStatus Status { get; set; }
        public int Memory { get; set; }
        public int MaxMemory { get; set; }
        public int Shares { get; set; }
        public int FlexShares { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsRescueConsoleActive { get; set; }
        public bool IsGandiAI { get; set; }
        public int Cores { get; set; }
        public int[] AttachedInterfaceIds { get; private set; }
        public Interface[] AttachedInterfaces { get; set; }
        public int[] AttachedDiskIds { get; private set; }
        public Disk[] AttachedDisks { get; set; }

        public VirtualMachine(Mapping.VirtualMachine virtualMachine, DataCenter[] dataCenters = null, Interface[] interfaces = null, Disk[] disks = null)
        {
            this.Id = virtualMachine.Id;
            this.DataCenterId = virtualMachine.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == virtualMachine.DataCenterId);
            this.Hostname = virtualMachine.Hostname;
            this.Description = virtualMachine.Description;
            this.Status = Converter.ToVirtualMachineStatus(virtualMachine.State);
            this.Memory = virtualMachine.Memory;
            this.Shares = virtualMachine.Shares;
            this.FlexShares = virtualMachine.FlexShares;
            this.Created = virtualMachine.Created;
            this.LastUpdated = virtualMachine.LastUpdated;
            this.IsRescueConsoleActive = (virtualMachine.IsRescueConsoleActive == 1);
            this.IsGandiAI = (virtualMachine.IsGandiAI == 1);
            this.Cores = virtualMachine.Cores;
            this.AttachedInterfaceIds = virtualMachine.AttachedInterfaceIds;
            if (interfaces != null)
            {
                List<Interface> interfaceList = new List<Interface>();
                foreach (int interfaceId in virtualMachine.AttachedInterfaceIds)
                {
                    Interface iface = interfaces.SingleOrDefault(i => i.Id == interfaceId);

                    if (iface.VirtualMachine == null)
                        iface.VirtualMachine = this;

                    interfaceList.Add(iface);
                }
                this.AttachedInterfaces = interfaceList.ToArray();
            }
            this.AttachedDiskIds = virtualMachine.AttachedDiskIds;
            if (disks != null)
            {
                List<Disk> diskList = new List<Disk>();
                foreach (int diskId in virtualMachine.AttachedDiskIds)
                {
                    Disk disk = disks.SingleOrDefault(d => d.Id == diskId);

                    List<VirtualMachine> virtualMachines = new List<VirtualMachine>();
                    bool virtualMachineAlreadyPresent = false;
                    if (disk.VirtualMachines != null)
                    {
                        virtualMachines = new List<VirtualMachine>(disk.VirtualMachines);
                        virtualMachineAlreadyPresent = disk.VirtualMachines.Contains(this, new VirtualMachineComparer());
                    }

                    if (!virtualMachineAlreadyPresent)
                        virtualMachines.Add(this);

                    disk.VirtualMachines = virtualMachines.ToArray();

                    diskList.Add(disk);
                }
                this.AttachedDisks = diskList.ToArray();
            }
        }
    }

    public class VirtualMachineComparer : IEqualityComparer<VirtualMachine>
    {
        public bool Equals(VirtualMachine x, VirtualMachine y)
        {
            return (x.Id == y.Id);
        }

        public int GetHashCode(VirtualMachine obj)
        {
            return obj.GetHashCode();
        }
    }
}