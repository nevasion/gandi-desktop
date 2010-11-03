using GandiDesktop.Gandi.Services.Hosting;
using System;

namespace GandiDesktop.Presentation.Model
{
    public class DiskResourceDetail : IResourceDetail
    {
        private const string ValueTemplate = "{0} ({1} Go)";

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type { get; private set; }

        public DiskResourceDetail(Disk disk)
        {
            this.Value = String.Format(DiskResourceDetail.ValueTemplate, disk.Name, (disk.Size / 1000));

            if (disk.IsBootDisk) this.Type = ResourceDetailType.SystemDisk;
            else this.Type = ResourceDetailType.Disk;
        }
    }
}