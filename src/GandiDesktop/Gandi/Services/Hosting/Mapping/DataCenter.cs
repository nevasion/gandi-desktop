using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class DataCenter
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string Name = "name";
            public const string Country = "country";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Name)]
        public string Name { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Country)]
        public string Country { get; set; }
    }
}