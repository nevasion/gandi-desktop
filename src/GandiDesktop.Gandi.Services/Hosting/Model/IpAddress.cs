using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class IpAddress 
    {
        [XmlRpcMember("id")]
        public int Id { get; set; }

        [XmlRpcMember("datacenter_id")]
        public int DataCenterId { get; set; }

        [XmlRpcMember("iface_id")]
        public int InterfaceId { get; set; }

        [XmlRpcMember("date_created")]
        public DateTime Created { get; set; }

        [XmlRpcMember("date_updated")]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember("ip")]
        public string Ip { get; set; }

        [XmlRpcMember("reverse")]
        public string Reverse { get; set; }

        [XmlRpcMember("state")]
        public object State { get; set; }

        [XmlRpcMember("version")]
        public object Version { get; set; }
    }
}
