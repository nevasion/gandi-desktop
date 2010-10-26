using System.Collections.Generic;
using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class DiskCopy
    {
        private class XmlRpcMappingNames
        {
            public const string DataCenterId = "datacenter_id";
            public const string Name = "name";
            public const string RepulsedDiskIds = "repulse_from";
        }

        [XmlRpcMember(XmlRpcMappingNames.DataCenterId)]
        public int DataCenterId { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.Name)]
        public string Name { get; private set; }

        [XmlRpcMember(XmlRpcMappingNames.RepulsedDiskIds)]
        public int[] RepulsedDiskIds { get; private set; }

        public DiskCopy(Model.DiskCopy diskCopy)
        {
            this.DataCenterId = diskCopy.DataCenter.Id;
            this.Name = diskCopy.Name;

            List<int> repulsedDiskIdList = new List<int>();
            if (diskCopy.RepulsedDisks != null)
            {
                foreach (Model.Disk repulsedDisk in diskCopy.RepulsedDisks)
                    repulsedDiskIdList.Add(repulsedDisk.Id);
            }
            this.RepulsedDiskIds = repulsedDiskIdList.ToArray();
        }
    }
}