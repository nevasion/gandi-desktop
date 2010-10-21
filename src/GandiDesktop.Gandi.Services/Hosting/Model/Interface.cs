using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class Interface
    {
        [XmlRpcMember("id")]
        public int Id { get; set; }

        [XmlRpcMember("datacenter_id")]
        public int DataCenterId { get; set; }

        [XmlRpcMember("date_created")]
        public DateTime Created { get; set; }

        [XmlRpcMember("date_updated")]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember("num")]
        public int Num { get; set; }

        [XmlRpcMember("state")]
        public object State { get; set; }

        [XmlRpcMember("bandwidth")]
        public object Bandwidth { get; set; }

        [XmlRpcMember("type")]
        public object Type { get; set; }

        [XmlRpcMember("vm_id")]
        public int VirtualMachineId { get; set; }

        [XmlRpcMember("ips")]
        public int[] IpIds { get; set; }
    }
}
