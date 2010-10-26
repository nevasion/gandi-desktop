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

        public void Create(DiskCreation diskCreation)
        {
            Mapping.DiskCreation mappingDiskCreation = new Mapping.DiskCreation(diskCreation);

            object mappingOperation = this.service.disk_create(this.apiKey, mappingDiskCreation);
        }

        public void CreateFrom(Disk sourceDisk, DiskCopy diskCopy)
        {
            Mapping.DiskCopy mappingDiskCopy = new Mapping.DiskCopy(diskCopy);

            object mappingOperation = this.service.disk_create_from(this.apiKey, mappingDiskCopy, sourceDisk.Id);
        }

        public void Update(Disk disk, DiskUpdate diskUpdate)
        {
            Mapping.DiskUpdate mappingDiskUpdate = new Mapping.DiskUpdate(diskUpdate);

            object mappingOperation = this.service.disk_update(this.apiKey, disk.Id, mappingDiskUpdate);
        }

        public void Delete(Disk disk)
        {
            object mappingOperation = this.service.disk_delete(this.apiKey, disk.Id);
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