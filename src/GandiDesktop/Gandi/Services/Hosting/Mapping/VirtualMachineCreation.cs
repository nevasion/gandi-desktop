using System;
using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class VirtualMachineCreation
    {
        private class XmlRpcMappingNames
        {
            public const string Hostname = "hostname";
            public const string Memory = "memory";
            public const string Shares = "shares";
            public const string DataCenterId = "datacenter_id";
            public const string SystemDiskId = "sys_disk_id";
            public const string IpVersion = "ip_version";
            public const string IsGandiAI = "ai_active";
            public const string UnixLogin = "login";
            public const string UnixPassword = "password";
        }

        [XmlRpcMember(XmlRpcMappingNames.Hostname)]
        public string Hostname { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Memory)]
        public int Memory { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Shares)]
        public int Shares { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.SystemDiskId)]
        public int SystemDiskId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IpVersion)]
        public int IpVersion { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IsGandiAI)]
        public int IsGandiAI { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.UnixLogin)]
        public string UnixLogin { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.UnixPassword)]
        public string UnixPassword { get; set; }

        public VirtualMachineCreation(Model.VirtualMachineCreation virtualMachineCreation)
        {
            this.Hostname = virtualMachineCreation.Hostname;
            this.Memory = virtualMachineCreation.Memory;
            this.Shares = virtualMachineCreation.Shares;
            this.DataCenterId = virtualMachineCreation.DataCenter.Id;
            this.SystemDiskId = virtualMachineCreation.SystemDisk.Id;
            this.IpVersion = Converter.ToIpVersion(virtualMachineCreation.IpVersion);
            this.IsGandiAI = (virtualMachineCreation.IsGandiAI ? 1 : 0);
            this.UnixLogin = virtualMachineCreation.UnixLogin;
            this.UnixPassword = virtualMachineCreation.UnixPassword;
        }
    }
}