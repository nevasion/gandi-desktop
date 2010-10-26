using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting
{
    [XmlRpcUrl("https://rpc.gandi.net/xmlrpc/2.0/")]
    public interface IHostingService : IXmlRpcProxy
    {
        [XmlRpcMethod("datacenter.list")]
        Mapping.DataCenter[] datacenter_list(string apiKey);

        #region Disk
        [XmlRpcMethod("disk.list")]
        Mapping.Disk[] disk_list(string apiKey);

        [XmlRpcMethod("disk.create")]
        object disk_create(string apiKey, Mapping.DiskCreation diskCreation);

        [XmlRpcMethod("disk.create_from")]
        object disk_create_from(string apiKey, Mapping.DiskCopy diskCopy, int sourceDiskId);

        [XmlRpcMethod("disk.update")]
        object disk_update(string apiKey, int diskId, Mapping.DiskUpdate diskUpdate);

        [XmlRpcMethod("disk.delete")]
        object disk_delete(string apiKey, int diskId);
        #endregion

        #region Interface
        [XmlRpcMethod("iface.list")]
        Mapping.Interface[] iface_list(string apiKey);

        [XmlRpcMethod("iface.create")]
        object iface_create(string apiKey, Mapping.InterfaceCreation ifaceCreation);

        [XmlRpcMethod("iface.update")]
        object iface_update(string apiKey, int ifaceId, Mapping.InterfaceUpdate ifaceUpdate);

        [XmlRpcMethod("iface.delete")]
        object iface_delete(string apiKey, int ifaceId);
        #endregion

        [XmlRpcMethod("image.list")]
        Mapping.Image[] image_list(string apiKey);

        [XmlRpcMethod("ip.list")]
        Mapping.IpAddress[] ip_list(string apiKey);

        [XmlRpcMethod("vm.list")]
        Mapping.VirtualMachine[] vm_list(string apiKey);
    }
}