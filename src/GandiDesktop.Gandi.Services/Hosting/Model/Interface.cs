using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class Interface
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string DataCenterId = "datacenter_id";
            public const string Created = "date_created";
            public const string LastUpdated = "date_updated";
            public const string Num = "num"; 
            public const string State = "state";
            public const string Bandwidth = "bandwidth";
            public const string Type = "type";
            public const string VirtualMachineId = "vm_id";
            public const string IpAddressIds = "ips";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Created)]
        public DateTime Created { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.LastUpdated)]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Num)]
        public int Num { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.State)]
        public string State { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Bandwidth)]
        public double Bandwidth { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Type)]
        public string Type { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.VirtualMachineId)]
        public int VirtualMachineId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IpAddressIds)]
        public int[] IpAddressIds { get; set; }
    }
}
