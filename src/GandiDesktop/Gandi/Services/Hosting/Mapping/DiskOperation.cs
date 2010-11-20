using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class DiskOperation : Operation
    {
        private class XmlRpcMappingNames
        {
            public const string DiskId = "disk_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.DiskId)]
        public int DiskId { get; set; }
    }
}