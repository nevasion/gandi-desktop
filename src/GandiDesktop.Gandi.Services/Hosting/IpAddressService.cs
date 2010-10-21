using System.Collections.Generic;
using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class IpAddressService
    {
        private IHostingService service;
        private string apiKey;

        public IpAddressService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public IpAddress[] List(DataCenter[] dataCenters)
        {
            Mapping.IpAddress[] mappingIpAddresses = this.service.ip_list(this.apiKey);

            List<IpAddress> ipAddressList = new List<IpAddress>();
            foreach (Mapping.IpAddress mappingIpAddress in mappingIpAddresses)
                ipAddressList.Add(new IpAddress(mappingIpAddress, dataCenters));

            IpAddress[] ipAddresses = ipAddressList.ToArray();

            return ipAddresses;
        }
    }
}