using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class VirtualMachine
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string DataCenterId = "datacenter_id";
            public const string Hostname = "hostname";
            public const string Description = "description";
            public const string State = "state";
            public const string Memory = "memory";
            public const string MaxMemory = "vm_max_memory";
            public const string Shares = "shares";
            public const string FlexShares = "flex_shares";
            public const string Created = "date_created";
            public const string LastUpdated = "date_updated";
            public const string IsRescueConsoleActive = "console";
            public const string IsGandiAI = "ai_active";
            public const string Cores = "cores";
            public const string AttachedInterfaceIds = "ifaces_id";
            public const string AttachedDiskIds = "disks_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Hostname)]
        public string Hostname { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Description)]
        public string Description { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.State)]
        public string State { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Memory)]
        public int Memory { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.MaxMemory)]
        public int MaxMemory { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Shares)]
        public int Shares { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.FlexShares)]
        public int FlexShares { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Created)]
        public DateTime Created { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.LastUpdated)]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IsRescueConsoleActive)]
        public int IsRescueConsoleActive { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IsGandiAI)]
        public int IsGandiAI { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Cores)]
        public int Cores { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.AttachedInterfaceIds)]
        public int[] AttachedInterfaceIds { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.AttachedDiskIds)]
        public int[] AttachedDiskIds { get; set; }
    }
}