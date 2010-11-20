using System.Collections.Generic;

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

        public IpAddressOperation Create(IpAddressCreation ipAddressCreation)
        {
            throw new System.NotImplementedException();

            Mapping.IpAddressCreation mappingIpAddressCreation = new Mapping.IpAddressCreation(ipAddressCreation);

            return new IpAddressOperation(this.service.ip_create(this.apiKey, mappingIpAddressCreation), null);
        }

        public IpAddressOperation Update(IpAddress ipAddress, IpAddressUpdate ipAddressUpdate)
        {
            Mapping.IpAddressUpdate mappingIpAddressUpdate = new Mapping.IpAddressUpdate(ipAddressUpdate);

            return new IpAddressOperation(this.service.ip_update(this.apiKey, ipAddress.Id, mappingIpAddressUpdate), ipAddress);
        }

        public IpAddressOperation Delete(IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new IpAddressOperation(this.service.ip_delete(this.apiKey, ipAddress.Id), ipAddress);
        }

        public IpAddress[] List()
        {
            return this.List(null);
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