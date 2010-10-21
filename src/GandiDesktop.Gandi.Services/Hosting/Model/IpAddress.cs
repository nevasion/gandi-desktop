using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class IpAddress 
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string DataCenterId = "datacenter_id";
            public const string InterfaceId = "iface_id";
            public const string Created = "date_created";
            public const string LastUpdated = "date_updated";
            public const string Ip = "ip";
            public const string Reverse = "reverse";
            public const string State = "state";
            public const string Version = "version";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.InterfaceId)]
        public int InterfaceId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Created)]
        public DateTime Created { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.LastUpdated)]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Ip)]
        public string Ip { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Reverse)]
        public string Reverse { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.State)]
        public string State { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Version)]
        public string Version { get; set; }
    }
}
