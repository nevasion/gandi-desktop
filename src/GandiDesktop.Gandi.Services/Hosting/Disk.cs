using System;
using System.Linq;
using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Disk
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public int Size { get; private set; }
        public object State { get; private set; }
        public object Visibility { get; private set; }
        public object Type { get; private set; }
        public bool IsBootDisk { get; private set; }
        public VirtualMachine[] VirtualMachines { get; internal set; }
        public string KernelVersion { get; private set; }
        public string Label { get; private set; }
        public DataCenter DataCenter { get; private set; }

        public Disk(Mapping.Disk disk, DataCenter[] dataCenters)
        {
            this.Id = disk.Id;
            this.Name = disk.Name;
            this.Created = disk.Created;
            this.LastUpdated = disk.LastUpdated;
            this.Size = disk.Size;
            this.State = disk.State;
            this.Visibility = disk.Visibility;
            this.Type = disk.Type;
            this.IsBootDisk = disk.IsBootDisk;
            this.KernelVersion = disk.KernelVersion;
            this.Label = disk.Label;
            this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == disk.DataCenterId);
        }
    }
}