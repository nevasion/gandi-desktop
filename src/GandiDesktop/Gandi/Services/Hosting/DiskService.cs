﻿using System.Collections.Generic;

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

            return new DiskOperation(this.service.DiskCreate(this.apiKey, mappingDiskCreation));
        }

        public DiskOperation CreateFrom(Disk sourceDisk, DiskCopy diskCopy)
        {
            Mapping.DiskCopy mappingDiskCopy = new Mapping.DiskCopy(diskCopy);

            return new DiskOperation(this.service.DiskCreateFrom(this.apiKey, mappingDiskCopy, sourceDisk.Id));
        }

        public DiskOperation Update(Disk disk, DiskUpdate diskUpdate)
        {
            Mapping.DiskUpdate mappingDiskUpdate = new Mapping.DiskUpdate(diskUpdate);

            return new DiskOperation(this.service.DiskUpdate(this.apiKey, disk.Id, mappingDiskUpdate), disk);
        }

        public DiskOperation Delete(Disk disk)
        {
            return new DiskOperation(this.service.DiskDelete(this.apiKey, disk.Id), disk);
        }

        public Disk Single(int diskId, DataCenter[] dataCenters = null)
        {
            Mapping.Disk mappingDisk = this.service.DiskInfo(this.apiKey, diskId);

            if (mappingDisk != null)
                return new Disk(mappingDisk, dataCenters);
            else
                return null;
        }

        public Disk[] List(DataCenter[] dataCenters = null)
        {
            Mapping.Disk[] mappingDisks = this.service.DiskList(this.apiKey);

            List<Disk> diskList = new List<Disk>();
            foreach (Mapping.Disk mappingDisk in mappingDisks)
                diskList.Add(new Disk(mappingDisk, dataCenters));

            Disk[] disks = diskList.ToArray();

            return disks;
        }
    }
}