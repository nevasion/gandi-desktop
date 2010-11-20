using System;
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

        public DiskOperation Create(DiskCreation diskCreation)
        {
            Mapping.DiskCreation mappingDiskCreation = new Mapping.DiskCreation(diskCreation);

            return new DiskOperation(this.service.disk_create(this.apiKey, mappingDiskCreation), null);
        }

        public DiskOperation CreateFrom(Disk sourceDisk, DiskCopy diskCopy)
        {
            Mapping.DiskCopy mappingDiskCopy = new Mapping.DiskCopy(diskCopy);

            return new DiskOperation(this.service.disk_create_from(this.apiKey, mappingDiskCopy, sourceDisk.Id), null);
        }

        public DiskOperation Update(Disk disk, DiskUpdate diskUpdate)
        {
            Mapping.DiskUpdate mappingDiskUpdate = new Mapping.DiskUpdate(diskUpdate);

            return new DiskOperation(this.service.disk_update(this.apiKey, disk.Id, mappingDiskUpdate), disk);
        }

        public DiskOperation Delete(Disk disk)
        {
            return new DiskOperation(this.service.disk_delete(this.apiKey, disk.Id), disk);
        }

        public Disk[] List()
        {
            return this.List(null);
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