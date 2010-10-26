using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting
{
    static class XmlRpcUrl
    {
        public const string Url = "https://rpc.gandi.net/xmlrpc/2.0/";
    }

    static class XmlRpcMethodNames
    {
        public const string DataCenterList = "datacenter.list";
        public const string DiskList = "disk.list";
        public const string DiskCreate = "disk.create";
        public const string DiskCreateFrom = "disk.create_from";
        public const string DiskUpdate = "disk.update";
        public const string DiskDelete = "disk.delete";
        public const string InterfaceList = "iface.list";
        public const string InterfaceCreate = "iface.create";
        public const string InterfaceUpdate = "iface.update";
        public const string InterfaceDelete = "iface.delete";
        public const string InterfaceAttachIp = "iface.ip_attach";
        public const string InterfaceDetachIp = "iface.ip_detach";
        public const string ImageList = "image.list";
        public const string IpAddressList = "ip.list";
        public const string IpAddressCreate = "ip.create";
        public const string IpAddressUpdate = "ip.update";
        public const string IpAddressDelete = "ip.delete";
        public const string VirtualMachineList = "vm.list";
        public const string VirtualMachineCreate = "vm.create";
        public const string VirtualMachineCreateFrom = "vm.create_from";
        public const string VirtualMachineUpdate = "vm.update";
        public const string VirtualMachineDelete = "vm.delete";
        public const string VirtualMachineStart = "vm.start";
        public const string VirtualMachineStop = "vm.stop";
    }

    [XmlRpcUrl(XmlRpcUrl.Url)]
    public interface IHostingService : IXmlRpcProxy
    {
        #region DataCenter
        [XmlRpcMethod(XmlRpcMethodNames.DataCenterList)]
        Mapping.DataCenter[] datacenter_list(string apiKey);
        #endregion

        #region Disk
        [XmlRpcMethod(XmlRpcMethodNames.DiskList)]
        Mapping.Disk[] disk_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.DiskCreate)]
        object disk_create(string apiKey, Mapping.DiskCreation diskCreation);

        [XmlRpcMethod(XmlRpcMethodNames.DiskCreateFrom)]
        object disk_create_from(string apiKey, Mapping.DiskCopy diskCopy, int sourceDiskId);

        [XmlRpcMethod(XmlRpcMethodNames.DiskUpdate)]
        object disk_update(string apiKey, int diskId, Mapping.DiskUpdate diskUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.DiskDelete)]
        object disk_delete(string apiKey, int diskId);
        #endregion

        #region Interface
        [XmlRpcMethod(XmlRpcMethodNames.InterfaceList)]
        Mapping.Interface[] iface_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceCreate)]
        object iface_create(string apiKey, Mapping.InterfaceCreation ifaceCreation);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceUpdate)]
        object iface_update(string apiKey, int ifaceId, Mapping.InterfaceUpdate ifaceUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDelete)]
        object iface_delete(string apiKey, int ifaceId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceAttachIp)]
        object iface_ip_attach(string apiKey, int ifaceId, int ipAddressId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDetachIp)]
        object iface_ip_detach(string apiKey, int ifaceId, int ipAddressId);
        #endregion

        #region Image
        [XmlRpcMethod(XmlRpcMethodNames.ImageList)]
        Mapping.Image[] image_list(string apiKey);
        #endregion

        #region IpAddress
        [XmlRpcMethod(XmlRpcMethodNames.IpAddressList)]
        Mapping.IpAddress[] ip_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressCreate)]
        object ip_create(string apiKey, Mapping.IpAddressCreation ipAddressCreation);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressUpdate)]
        object ip_update(string apiKey, int ipAddressId, Mapping.IpAddressUpdate ipAddressUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressDelete)]
        object ip_delete(string apiKey, int ipAddressId);
        #endregion

        #region VirtualMachine
        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineList)]
        Mapping.VirtualMachine[] vm_list(string apiKey);
        #endregion
    }
}