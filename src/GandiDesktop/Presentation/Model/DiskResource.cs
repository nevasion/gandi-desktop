using System.Collections.Generic;
using GandiDesktop.Gandi.Services.Hosting;
using System;

namespace GandiDesktop.Presentation.Model
{
    public class DiskResource : IResource
    {
        private const string DetailSizeName = "Size";
        private const string DetailSizeValueTemplate = "{0} Mo";
        private const string DetailTypeName = "Type";

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Status { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.Disk; }
        }

        public IResourceDetail[] Details { get; private set; }

        public IHostingResource Resource { get; private set; }

        public bool CanAttach { get; private set; }

        public DiskResource(Disk disk)
        {
            this.Id = disk.Id;
            this.Name = disk.Name;
            this.Resource = disk;
            this.CanAttach = true;

            List<IResourceDetail> details = new List<IResourceDetail>();

            details.Add(new DataCenterResourceDetail(disk.DataCenter)); 
            details.Add(new TextResourceDetail(DiskResource.DetailSizeName, String.Format(DiskResource.DetailSizeValueTemplate, disk.Size)));
            details.Add(new TextResourceDetail(DiskResource.DetailTypeName, disk.Type.ToString()));

            this.Details = details.ToArray();
        }

        public bool CanReceiveAttachement(IHostingResource resource)
        {
            return false;
        }
    }
}