using System;
using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class InterfaceCreation
    {
        private class XmlRpcMappingNames
        {
            public const string DataCenterId = "datacenter_id";
            public const string IpVersion = "ip_version";
            public const string Bandwidth = "bandwidth";
        }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.IpVersion)]
        public int IpVersion { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Bandwidth)]
        public double? Bandwidth { get; private set; }

        public InterfaceCreation(Model.InterfaceCreation ifaceCreation)
        {
            this.DataCenterId = ifaceCreation.DataCenter.Id;
            this.IpVersion = Convert.ToInt32(ifaceCreation.IpVersion.ToString().Substring(1));
            this.Bandwidth = ifaceCreation.Bandwidth;
        }
    }
}