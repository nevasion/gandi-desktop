using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class Disk
    {
        [XmlRpcMember("id")]
        public int Id { get; set; }

        [XmlRpcMember("name")]
        public string Name { get; set; }

        [XmlRpcMember("date_created")]
        public DateTime Created { get; set; }

        [XmlRpcMember("date_updated")]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember("size")]
        public int Size { get; set; }

        [XmlRpcMember("state")]
        public object State { get; set; }

        [XmlRpcMember("source")]
        public int Source { get; set; }

        [XmlRpcMember("visibility")]
        public object Visibility { get; set; }

        [XmlRpcMember("type")]
        public object Type { get; set; }

        [XmlRpcMember("is_boot_disk")]
        public bool IsBootDisk { get; set; }

        [XmlRpcMember("vms_id")]
        public int[] VirtualMachineIds { get; set; }

        [XmlRpcMember("kernel_version")]
        public string KernelVersion { get; set; }

        [XmlRpcMember("label")]
        public string Label { get; set; }

        [XmlRpcMember("datacenter_id")]
        public int DataCenterId { get; set; }
    }
}