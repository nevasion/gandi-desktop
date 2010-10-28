using System.Collections.Generic;
using GandiDesktop.Gandi.Services.Hosting;
using System;

namespace GandiDesktop.Model
{
    public class DiskResource : IResource
    {
        private const string DetailSizeName = "Size";
        private const string DetailSizeValueTemplate = "{0} Mo";
        private const string DetailTypeName = "Type";

        public string Name { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.Disk; }
        }

        public IResourceDetail[] Details { get; private set; }

        public DiskResource(Disk disk)
        {
            this.Name = disk.Name;

            List<IResourceDetail> details = new List<IResourceDetail>();

            details.Add(new DataCenterResourceDetail(disk.DataCenter)); 
            details.Add(new TextResourceDetail(DiskResource.DetailSizeName, String.Format(DiskResource.DetailSizeValueTemplate, disk.Size)));
            details.Add(new TextResourceDetail(DiskResource.DetailTypeName, disk.Type.ToString()));

            this.Details = details.ToArray();
        }
    }
}