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

            return new IpAddressOperation(this.service.IpAddressCreate(this.apiKey, mappingIpAddressCreation), null);
        }

        public IpAddressOperation Update(IpAddress ipAddress, IpAddressUpdate ipAddressUpdate)
        {
            Mapping.IpAddressUpdate mappingIpAddressUpdate = new Mapping.IpAddressUpdate(ipAddressUpdate);

            return new IpAddressOperation(this.service.IpAddressUpdate(this.apiKey, ipAddress.Id, mappingIpAddressUpdate), ipAddress);
        }

        public IpAddressOperation Delete(IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new IpAddressOperation(this.service.IpAddressDelete(this.apiKey, ipAddress.Id), ipAddress);
        }

        public IpAddress Single(int ipAddressId, DataCenter[] dataCenters)
        {
            Mapping.IpAddress mappingIpAddress = this.service.IpAddressInfo(this.apiKey, ipAddressId);

            if (mappingIpAddress != null)
                return new IpAddress(mappingIpAddress, dataCenters);
            else
                return null;
        }

        public IpAddress[] List(DataCenter[] dataCenters)
        {
            Mapping.IpAddress[] mappingIpAddresses = this.service.IpAddressList(this.apiKey);

            List<IpAddress> ipAddressList = new List<IpAddress>();
            foreach (Mapping.IpAddress mappingIpAddress in mappingIpAddresses)
                ipAddressList.Add(new IpAddress(mappingIpAddress, dataCenters));

            IpAddress[] ipAddresses = ipAddressList.ToArray();

            return ipAddresses;
        }
    }
}