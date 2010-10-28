﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Interface
    {
        public int Id { get; private set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public int? Index { get; private set; }
        public string State { get; private set; }
        public double Bandwidth { get; private set; }
        public string Type { get; private set; }
        public int? VirtualMachineId { get; private set; }
        public VirtualMachine VirtualMachine { get; internal set; }
        public int[] IpAddressIds { get; private set; }
        public IpAddress[] IpAddresses { get; private set; }

        public Interface(Mapping.Interface iface, DataCenter[] dataCenters, IpAddress[] ips)
        {
            this.Id = iface.Id;
            this.DataCenterId = iface.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == iface.DataCenterId);
            this.Created = iface.Created;
            this.LastUpdated = iface.LastUpdated;
            if (iface.Num != null)
                this.Index = Convert.ToInt32(iface.Num);
            this.State = iface.State;
            this.Bandwidth = iface.Bandwidth;
            this.Type = iface.Type;
            if (iface.VirtualMachineId != null)
                this.VirtualMachineId = Convert.ToInt32(iface.VirtualMachineId);
            this.VirtualMachine = null;
            this.IpAddressIds = iface.IpAddressIds;
            if (ips != null)
            {
                List<IpAddress> ipList = new List<IpAddress>();
                foreach (int ipId in iface.IpAddressIds)
                {
                    IpAddress ipAddress = ips.SingleOrDefault(i => i.Id == ipId);

                    if (ipAddress.Interface == null)
                        ipAddress.Interface = this;

                    ipList.Add(ipAddress);
                }
                this.IpAddresses = ipList.ToArray();
            }
        }
    }
}