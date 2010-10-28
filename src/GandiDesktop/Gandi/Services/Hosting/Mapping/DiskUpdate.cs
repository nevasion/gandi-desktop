using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class DiskUpdate
    {
        private class XmlRpcMappingNames
        {
            public const string Name = "name";
            public const string Size = "size";
            public const string KernelVersion = "kernel";
            public const string CommandLine = "cmdline";
        }

        [XmlRpcMember(XmlRpcMappingNames.Name)]
        public string Name { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Size)]
        public int? Size { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.KernelVersion)]
        public string KernelVersion { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.CommandLine)]
        public string CommandLine { get; private set; }

        public DiskUpdate(Model.DiskUpdate diskUpdate)
        {
            this.Name = diskUpdate.Name;
            this.Size = diskUpdate.Size;
            this.KernelVersion = diskUpdate.KernelVersion;
            this.CommandLine = diskUpdate.CommandLine;
        }
    }
}