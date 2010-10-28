using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class IpAddressUpdate
    {
        private class XmlRpcMappingNames
        {
            public const string Reverse = "reverse";
        }

        [XmlRpcMember(XmlRpcMappingNames.Reverse)]
        public string Reverse { get; private set; }

        public IpAddressUpdate(Model.IpAddressUpdate ipAddressUpdate)
        {
            this.Reverse = ipAddressUpdate.Reverse;
        }
    }
}