using System;
using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class Operation
    {
        private class XmlRpcMappingNames
        {
            public const string Id = "id";
            public const string Type = "type";
            public const string Source = "source";
            public const string Created = "date_created";
            public const string LastUpdated = "date_updated";
            public const string Step = "step";
        }

        [XmlRpcMember(XmlRpcMappingNames.Id)]
        public int Id { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Type)]
        public string Type { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Source)]
        public string Source { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Created)]
        public DateTime Created { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.LastUpdated)]
        public DateTime LastUpdated { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Step)]
        public string Step { get; set; }
    }
}