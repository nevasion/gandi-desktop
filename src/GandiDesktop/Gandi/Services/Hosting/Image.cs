using System.Linq;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Image
    {
        public int Id { get; private set; }
        public string Label { get; private set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public int DiskId { get; private set; }
        public Disk Disk { get; private set; }
        public string Visibility { get; private set; }
        public string OsArchitecture { get; private set; }
        public int AuthorId { get; private set; }

        public Image(Mapping.Image image, DataCenter[] dataCenters, Disk[] disks)
        {
            this.Id = image.Id;
            this.Label = image.Label;
            this.DataCenterId = image.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == image.DataCenterId);
            this.DiskId = image.DiskId;
            if (disks != null)
                this.Disk = disks.SingleOrDefault(d => d.Id == image.DiskId);
            this.Visibility = image.Visibility;
            this.OsArchitecture = image.OsArchitecture;
            this.AuthorId = image.AuthorId;
        }
    }
}