using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class Disk
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string Name = "name";
            public const string Created = "date_created";
            public const string LastUpdated = "date_updated";
            public const string Size = "size";
            public const string State = "state";
            public const string Source = "source";
            public const string Visibility = "visibility";
            public const string Type = "type";
            public const string IsBookDisk = "is_boot_disk";
            public const string VirtualMachineIds = "vms_id";
            public const string KernelVersion = "kernel_version";
            public const string Label = "label";
            public const string DataCenterId = "datacenter_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Name)]
        public string Name { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Created)]
        public DateTime Created { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.LastUpdated)]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Size)]
        public int Size { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.State)]
        public string State { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Source)]
        public int? Source { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Visibility)]
        public string Visibility { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Type)]
        public string Type { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IsBookDisk)]
        public bool IsBootDisk { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.VirtualMachineIds)]
        public int[] VirtualMachineIds { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.KernelVersion)]
        public string KernelVersion { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Label)]
        public string Label { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }
    }
}