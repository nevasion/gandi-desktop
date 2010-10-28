using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class InterfaceUpdate
    {
        private class XmlRpcMappingNames
        {
            public const string Bandwidth = "bandwidth";
        }

        [XmlRpcMember(XmlRpcMappingNames.Bandwidth)]
        public double Bandwidth { get; private set; }

        public InterfaceUpdate(Model.InterfaceUpdate ifaceUpdate)
        {
            this.Bandwidth = ifaceUpdate.Bandwidth;
        }
    }
}