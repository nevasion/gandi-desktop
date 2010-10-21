using System.Collections.Generic;
using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

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

        public Interface[] List(DataCenter[] dataCenters, IpAddress[] ips)
        {
            Mapping.Interface[] mappingInterfaces = this.service.iface_list(this.apiKey);

            List<Interface> interfaceList = new List<Interface>();
            foreach (Mapping.Interface mappingInterface in mappingInterfaces)
                interfaceList.Add(new Interface(mappingInterface, dataCenters, ips));

            Interface[] interfaces = interfaceList.ToArray();

            return interfaces;
        }
    }
}