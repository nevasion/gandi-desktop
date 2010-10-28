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

        public void Create(IpAddressCreation ipAddressCreation)
        {
            throw new System.NotImplementedException();

            //Mapping.IpAddressCreation mappingIpAddressCreation = new Mapping.IpAddressCreation(ipAddressCreation);

            //object mappingOperation = this.service.ip_create(this.apiKey, mappingIpAddressCreation);
        }

        public void Update(IpAddress ipAddress, IpAddressUpdate ipAddressUpdate)
        {
            Mapping.IpAddressUpdate mappingIpAddressUpdate = new Mapping.IpAddressUpdate(ipAddressUpdate);

            object mappingOperation = this.service.ip_update(this.apiKey, ipAddress.Id, mappingIpAddressUpdate);
        }

        public void Delete(IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            //object mappingOperation = this.service.ip_delete(this.apiKey, ipAddress.Id);
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