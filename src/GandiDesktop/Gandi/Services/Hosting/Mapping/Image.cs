using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class Image
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string Label = "label";
            public const string DataCenterId = "datacenter_id";
            public const string DiskId = "disk_id";
            public const string Visibility = "visibility";
            public const string OsArchitecture = "os_arch";
            public const string AuthorId = "author_id";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Label)]
        public string Label { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.DiskId)]
        public int DiskId { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Visibility)]
        public string Visibility { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.OsArchitecture)]
        public string OsArchitecture { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.AuthorId)]
        public int AuthorId { get; set; }
    }
}