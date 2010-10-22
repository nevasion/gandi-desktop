using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting
{
    [XmlRpcUrl("https://rpc.gandi.net/xmlrpc/2.0/")]
    public interface IHostingService : IXmlRpcProxy
    {
        [XmlRpcMethod("datacenter.list")]
        Mapping.DataCenter[] datacenter_list(string apiKey);

        [XmlRpcMethod("disk.list")]
        Mapping.Disk[] disk_list(string apiKey);

        [XmlRpcMethod("iface.list")]
        Mapping.Interface[] iface_list(string apiKey);

        [XmlRpcMethod("image.list")]
        Mapping.Image[] image_list(string apiKey);

        [XmlRpcMethod("ip.list")]
        Mapping.IpAddress[] ip_list(string apiKey);

        [XmlRpcMethod("vm.list")]
        Mapping.VirtualMachine[] vm_list(string apiKey);
    }
}