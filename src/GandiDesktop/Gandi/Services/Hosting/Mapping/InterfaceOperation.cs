using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class InterfaceOperation : Operation
    {
        private class XmlRpcMappingNames
        {
            public const string InterfaceId = "iface_id";
            public const string IpAddressId = "ip_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.InterfaceId)]
        public int InterfaceId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IpAddressId)]
        public int? IpAddressId { get; set; }
    }
}