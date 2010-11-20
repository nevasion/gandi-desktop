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

            return new InterfaceOperation(this.service.iface_create(this.apiKey, mappingIfaceCreation), null);
        }

        public InterfaceOperation Update(Interface iface, InterfaceUpdate ifaceUpdate)
        {
            Mapping.InterfaceUpdate mappingIfaceUpdate = new Mapping.InterfaceUpdate(ifaceUpdate);

            return new InterfaceOperation(this.service.iface_update(this.apiKey, iface.Id, mappingIfaceUpdate), iface);
        }

        public InterfaceOperation Delete(Interface iface)
        {
            return new InterfaceOperation(this.service.iface_delete(this.apiKey, iface.Id), iface);
        }

        public InterfaceOperation AttachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new InterfaceOperation(this.service.iface_ip_attach(this.apiKey, iface.Id, ipAddress.Id), iface, ipAddress);
        }

        public InterfaceOperation DetachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            return new InterfaceOperation(this.service.iface_ip_detach(this.apiKey, iface.Id, ipAddress.Id), iface, ipAddress);
        }

        public Interface[] List()
        {
            return this.List(null, null);
        }

        public Interface[] List(IpAddress[] ipAddresses)
        {
            return this.List(null, ipAddresses);
        }

        public Interface[] List(DataCenter[] dataCenters)
        {
            return this.List(dataCenters, null);
        }

        public Interface[] List(DataCenter[] dataCenters, IpAddress[] ipAddresses)
        {
            Mapping.Interface[] mappingInterfaces = this.service.iface_list(this.apiKey);

            List<Interface> interfaceList = new List<Interface>();
            foreach (Mapping.Interface mappingInterface in mappingInterfaces)
                interfaceList.Add(new Interface(mappingInterface, dataCenters, ipAddresses));

            Interface[] interfaces = interfaceList.ToArray();

            return interfaces;
        }
    }
}