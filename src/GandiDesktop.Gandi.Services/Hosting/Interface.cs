using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Interface
    {
        public int Id { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public int Index { get; private set; }
        public object State { get; private set; }
        public object Bandwidth { get; private set; }
        public object Type { get; private set; }
        public VirtualMachine VirtualMachine { get; internal set; }
        public IpAddress[] IpAddresses { get; private set; }

        public Interface(Mapping.Interface iface, DataCenter[] dataCenters, IpAddress[] ips)
        {
            this.Id = iface.Id;
            this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == iface.DataCenterId);
            this.Created = iface.Created;
            this.LastUpdated = iface.LastUpdated;
            this.Index = iface.Num;
            this.State = iface.State;
            this.Bandwidth = iface.Bandwidth;
            this.Type = iface.Type;
            this.VirtualMachine = null;

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