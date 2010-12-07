using System;
using System.Linq;
using System.Net;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class IpAddress : IHostingResource
    {
        public int Id { get; private set; }
        public int DataCenterId { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public int? InterfaceId { get; private set; }
        public Interface Interface { get; internal set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public IPAddress Ip { get; private set; }
        public string Reverse { get; private set; }
        public IpAddressStatus Status { get; private set; }
        public IpVersion Version { get; private set; }

        public IpAddress(Mapping.IpAddress ipAddress, DataCenter[] dataCenters = null)
        {
            this.Id = ipAddress.Id;
            this.DataCenterId = ipAddress.DataCenterId;
            if (dataCenters != null)
                this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == ipAddress.DataCenterId);
            if (ipAddress.InterfaceId != null)
                this.InterfaceId = Convert.ToInt32(ipAddress.InterfaceId);
            this.Interface = null;
            this.Created = ipAddress.Created;
            this.LastUpdated = ipAddress.LastUpdated;
            this.Ip = IPAddress.Parse(ipAddress.Ip);
            this.Reverse = ipAddress.Reverse;
            this.Status = Converter.ToIpAddressStatus(ipAddress.State);
            this.Version = Converter.ToIpVersion(ipAddress.Version);
        }
    }
}