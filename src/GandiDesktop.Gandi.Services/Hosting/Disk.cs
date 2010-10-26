using System;
using System.Linq;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public enum DiskType
    {
        Data,
        Backup
    }

    public class Disk
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public int Size { get; private set; }
        public string State { get; private set; }
        public int? Source { get; private set; }
        public string Visibility { get; private set; }
        public DiskType Type { get; private set; }
        public bool IsBootDisk { get; private set; }
        public int[] VirtualMachineIds { get; private set; }
        public VirtualMachine[] VirtualMachines { get; internal set; }
        public string KernelVersion { get; private set; }
        public string Label { get; private set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; private set; }

        public Disk(Mapping.Disk disk, DataCenter[] dataCenters)
        {
            this.Id = disk.Id;
            this.Name = disk.Name;
            this.Created = disk.Created;
            this.LastUpdated = disk.LastUpdated;
            this.Size = disk.Size;
            this.State = disk.State;
            if (disk.Source != null)
                this.Source = Convert.ToInt32(disk.Source);
            this.Visibility = disk.Visibility;
            this.Type = (DiskType)Enum.Parse(typeof(DiskType), disk.Type, true);
            this.IsBootDisk = disk.IsBootDisk;
            this.VirtualMachineIds = disk.VirtualMachineIds;
            this.VirtualMachines = null;
            this.KernelVersion = disk.KernelVersion;
            this.Label = disk.Label;
            this.DataCenterId = disk.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == disk.DataCenterId);
        }
    }
}