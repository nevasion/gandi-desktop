using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class IpAddressResourceDetail : IResourceDetail
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.IpAddress; }
        }

        public IpAddressResourceDetail(IpAddress ipAddress)
        {
            this.Value = ipAddress.Ip.ToString();
        }
    }
}