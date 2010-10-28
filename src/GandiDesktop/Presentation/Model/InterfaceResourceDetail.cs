using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Model
{
    public class InterfaceResourceDetail : IResourceDetail
    {
        private const string PluralSuffix = "s";
        private const string ValueTemplate = "{0} Kbps ({1} IP{2})";

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Interface; }
        }

        public InterfaceResourceDetail(Interface iface)
        {
            this.Value = String.Format(
                InterfaceResourceDetail.ValueTemplate, 
                iface.Bandwidth, 
                iface.IpAddressIds.Length, 
                (iface.IpAddressIds.Length <= 1 ? null : InterfaceResourceDetail.PluralSuffix));
        }
    }
}