using System.Collections.Generic;
using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class DiskCreation
    {
        private class XmlRpcMappingNames
        {
            public const string DataCenterId = "datacenter_id";
            public const string Name = "name";
            public const string Size = "size";
            public const string Type = "type";
            public const string RepulsedDiskIds = "repulse_from";
        }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Name)]
        public string Name { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Size)]
        public int Size { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Type)]
        public string Type { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.RepulsedDiskIds)]
        public int[] RepulsedDiskIds { get; private set; }

        public DiskCreation(Model.DiskCreation diskCreation)
        {
            this.DataCenterId = diskCreation.DataCenter.Id;
            this.Name = diskCreation.Name;
            this.Size = diskCreation.Size;
            this.Type = diskCreation.Type.ToString().ToLowerInvariant();

            List<int> repulsedDiskIdList = new List<int>();
            if (diskCreation.RepulsedDisks != null)
            {
                foreach (Model.Disk repulsedDisk in diskCreation.RepulsedDisks)
                    repulsedDiskIdList.Add(repulsedDisk.Id);
            }
            this.RepulsedDiskIds = repulsedDiskIdList.ToArray();
        }
    }
}