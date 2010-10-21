using System;
using System.Linq;
using System.Net;
using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class IpAddress
    {
        public int Id { get; private set; }
        public DataCenter DataCenter { get; private set; }
        public Interface Interface { get; internal set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public IPAddress Ip { get; private set; }
        public string Reverse { get; private set; }
        public object State { get; private set; }
        public object Version { get; private set; }

        public IpAddress(Mapping.IpAddress ipAddress, DataCenter[] dataCenters)
        {
            this.Id = ipAddress.Id;
            this.DataCenter = dataCenters.SingleOrDefault(d => d.Id == ipAddress.DataCenterId);
            this.Interface = null;
            this.Created = ipAddress.Created;
            this.LastUpdated = ipAddress.LastUpdated;
            this.Ip = IPAddress.Parse(ipAddress.Ip);
            this.Reverse = ipAddress.Reverse;
            this.State = ipAddress.State;
            this.Version = ipAddress.Version;
        }
    }
}