using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class IpAddressOperation : Operation
    {
        private class XmlRpcMappingNames
        {
            public const string IpAddressId = "ip_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.IpAddressId)]
        public int IpAddressId { get; set; }
    }
}