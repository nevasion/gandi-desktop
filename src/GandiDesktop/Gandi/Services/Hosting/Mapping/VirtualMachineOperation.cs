using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class VirtualMachineOperation : Operation
    {
        private class XmlRpcMappingNames
        {
            public const string VirtualMachineId = "vm_id";
            public const string DiskId = "disk_id";
            public const string InterfaceId = "iface_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.VirtualMachineId)]
        public int VirtualMachineId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DiskId)]
        public int? DiskId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.InterfaceId)]
        public int? InterfaceId { get; set; }
    }
}