using System;
using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class IpAddressCreation
    {
        private class XmlRpcMappingNames
        {
            public const string DataCenterId = "datacenter_id";
            public const string IpVersion = "ip_version";
            public const string Reverse = "reverse";
        }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.IpVersion)]
        public int IpVersion { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Reverse)]
        public string Reverse { get; private set; }

        public IpAddressCreation(Model.IpAddressCreation ipAddressCreation)
        {
            this.DataCenterId = ipAddressCreation.DataCenter.Id;
            this.IpVersion = Convert.ToInt32(ipAddressCreation.IpVersion.ToString().Substring(1));
            this.Reverse = ipAddressCreation.Reverse;
        }
    }
}