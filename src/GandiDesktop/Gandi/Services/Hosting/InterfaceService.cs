using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class InterfaceService
    {
        private IHostingService service;
        private string apiKey;

        public InterfaceService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public InterfaceOperation Create(InterfaceCreation ifaceCreation)
        {
            Mapping.InterfaceCreation mappingIfaceCreation = new Mapping.InterfaceCreation(ifaceCreation);

            return new InterfaceOperation(this.service.InterfaceCreate(this.apiKey, mappingIfaceCreation), null);
        }

        public InterfaceOperation Update(Interface iface, InterfaceUpdate ifaceUpdate)
        {
            Mapping.InterfaceUpdate mappingIfaceUpdate = new Mapping.InterfaceUpdate(ifaceUpdate);

            return new InterfaceOperation(this.service.InterfaceUpdate(this.apiKey, iface.Id, mappingIfaceUpdate), iface);
        }

        public InterfaceOperation Delete(Interface iface)
        {
            return new InterfaceOperation(this.service.InterfaceDelete(this.apiKey, iface.Id), iface);
        }

        public InterfaceOperation AttachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new InterfaceOperation(this.service.InterfaceAttachIp(this.apiKey, iface.Id, ipAddress.Id), iface, ipAddress);
        }

        public InterfaceOperation DetachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new InterfaceOperation(this.service.InterfaceDetachIp(this.apiKey, iface.Id, ipAddress.Id), iface, ipAddress);
        }

        public Interface Single(int interfaceId, DataCenter[] dataCenters = null, IpAddress[] ipAddresses = null)
        {
            Mapping.Interface mappingInterface = this.service.InterfaceInfo(this.apiKey, interfaceId);

            if (mappingInterface != null)
                return new Interface(mappingInterface, dataCenters, ipAddresses);
            else
                return null;
        }

        public Interface[] List(DataCenter[] dataCenters = null, IpAddress[] ipAddresses = null)
        {
            Mapping.Interface[] mappingInterfaces = this.service.InterfaceList(this.apiKey);

            List<Interface> interfaceList = new List<Interface>();
            foreach (Mapping.Interface mappingInterface in mappingInterfaces)
                interfaceList.Add(new Interface(mappingInterface, dataCenters, ipAddresses));

            Interface[] interfaces = interfaceList.ToArray();

            return interfaces;
        }
    }
}