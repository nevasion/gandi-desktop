﻿using System.Linq;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Image : IHostingResource
    {
        public int Id { get; private set; }
        public string Label { get; private set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public int DiskId { get; private set; }
        public Disk Disk { get; private set; }
        public ImageVisibility Visibility { get; private set; }
        public ImageArchitecture Architecture { get; private set; }
        public int AuthorId { get; private set; }

        public Image(Mapping.Image image, DataCenter[] dataCenters = null, Disk[] disks = null)
        {
            this.Id = image.Id;
            this.Label = image.Label;
            this.DataCenterId = image.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == image.DataCenterId);
            this.DiskId = image.DiskId;
            if (disks != null)
                this.Disk = disks.SingleOrDefault(d => d.Id == image.DiskId);
            this.Visibility = Converter.ToImageVisibility(image.Visibility);
            this.Architecture = Converter.ToImageArchitecture(image.OsArchitecture);
            this.AuthorId = image.AuthorId;
        }
    }
}