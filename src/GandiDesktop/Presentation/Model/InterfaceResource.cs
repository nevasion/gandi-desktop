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

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Status { get; private set; }

        public ResourceType Type
        {
            get { return ResourceType.Interface; }
        }

        public IResourceDetail[] Details { get; private set; }

        public IHostingResource Resource { get; private set; }

        public bool CanAttach { get; private set; }

        public InterfaceResource(Interface iface)
        {
            this.Id = iface.Id;
            this.Name = String.Format(InterfaceResource.NameTemplate, iface.Bandwidth);
            this.Resource = iface;
            this.CanAttach = true;

            List<IResourceDetail> details = new List<IResourceDetail>();

            foreach (IpAddress ipAddress in iface.IpAddresses)
                details.Add(new IpAddressResourceDetail(ipAddress));

            this.Details = details.ToArray();
        }

        public bool CanReceiveAttachement(IHostingResource resource)
        {
            return false;
            //return resource is IpAddressResource;
        }
    }
}