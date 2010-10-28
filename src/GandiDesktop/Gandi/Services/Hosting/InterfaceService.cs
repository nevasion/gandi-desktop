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

        public void Create(InterfaceCreation ifaceCreation)
        {
            Mapping.InterfaceCreation mappingIfaceCreation = new Mapping.InterfaceCreation(ifaceCreation);

            object mappingOperation = this.service.iface_create(this.apiKey, mappingIfaceCreation);
        }

        public void Update(Interface iface, InterfaceUpdate ifaceUpdate)
        {
            Mapping.InterfaceUpdate mappingIfaceUpdate = new Mapping.InterfaceUpdate(ifaceUpdate);

            object mappingOperation = this.service.iface_update(this.apiKey, iface.Id, mappingIfaceUpdate);
        }

        public void Delete(Interface iface)
        {
            object mappingOperation = this.service.iface_delete(this.apiKey, iface.Id);
        }

        public void AttachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            //object mappingOperation = this.service.iface_ip_attach(this.apiKey, iface.Id, ipAddress.Id);
        }

        public void DetachIpAddress(Interface iface, IpAddress ipAddress)
        {
            throw new System.NotImplementedException();

            //object mappingOperation = this.service.iface_ip_detach(this.apiKey, iface.Id, ipAddress.Id);
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