using System;
using System.Collections.Generic;
using System.Linq;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class InterfaceResource : IResource
    {
        private const string NameTemplate = "{0} Kbps";
        private const string IpAddressName = "IP";

        public string Name { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.Interface; }
        }

        public IResourceDetail[] Details { get; private set; }

        public InterfaceResource(Interface iface)
        {
            this.Name = String.Format(InterfaceResource.NameTemplate, iface.Bandwidth);

            List<IResourceDetail> details = new List<IResourceDetail>();

            foreach (IpAddress ipAddress in iface.IpAddresses)
                details.Add(new TextResourceDetail(InterfaceResource.IpAddressName, ipAddress.Ip.ToString()));

            this.Details = details.ToArray();
        }
    }
}