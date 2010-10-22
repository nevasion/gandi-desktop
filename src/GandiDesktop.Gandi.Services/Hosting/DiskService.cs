using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DiskService
    {
        private IHostingService service;
        private string apiKey;

        public DiskService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public Disk[] List(DataCenter[] dataCenters)
        {
            Mapping.Disk[] mappingDisks = this.service.disk_list(this.apiKey);

            List<Disk> diskList = new List<Disk>();
            foreach (Mapping.Disk mappingDisk in mappingDisks)
                diskList.Add(new Disk(mappingDisk, dataCenters));

            Disk[] disks = diskList.ToArray();

            return disks;
        }
    }
}