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
        public const string VirtualMachineReboot = "vm.reboot";
        public const string VirtualMachineAttachDisk = "vm.disk_attach";
        public const string VirtualMachineDetachDisk = "vm.disk_detach";
        public const string VirtualMachineAttachInterface = "vm.iface_attach";
        public const string VirtualMachineDetachInterface = "vm.iface_detach";
        public const string OperationInfo = "operation.info";
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
        Mapping.DiskOperation disk_create(string apiKey, Mapping.DiskCreation diskCreation);

        [XmlRpcMethod(XmlRpcMethodNames.DiskCreateFrom)]
        Mapping.DiskOperation disk_create_from(string apiKey, Mapping.DiskCopy diskCopy, int sourceDiskId);

        [XmlRpcMethod(XmlRpcMethodNames.DiskUpdate)]
        Mapping.DiskOperation disk_update(string apiKey, int diskId, Mapping.DiskUpdate diskUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.DiskDelete)]
        Mapping.DiskOperation disk_delete(string apiKey, int diskId);
        #endregion

        #region Interface
        [XmlRpcMethod(XmlRpcMethodNames.InterfaceList)]
        Mapping.Interface[] iface_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceCreate)]
        Mapping.InterfaceOperation iface_create(string apiKey, Mapping.InterfaceCreation ifaceCreation);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceUpdate)]
        Mapping.InterfaceOperation iface_update(string apiKey, int ifaceId, Mapping.InterfaceUpdate ifaceUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDelete)]
        Mapping.InterfaceOperation iface_delete(string apiKey, int ifaceId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceAttachIp)]
        Mapping.InterfaceOperation iface_ip_attach(string apiKey, int ifaceId, int ipAddressId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDetachIp)]
        Mapping.InterfaceOperation iface_ip_detach(string apiKey, int ifaceId, int ipAddressId);
        #endregion

        #region Image
        [XmlRpcMethod(XmlRpcMethodNames.ImageList)]
        Mapping.Image[] image_list(string apiKey);
        #endregion

        #region IpAddress
        [XmlRpcMethod(XmlRpcMethodNames.IpAddressList)]
        Mapping.IpAddress[] ip_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressCreate)]
        Mapping.IpAddressOperation ip_create(string apiKey, Mapping.IpAddressCreation ipAddressCreation);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressUpdate)]
        Mapping.IpAddressOperation ip_update(string apiKey, int ipAddressId, Mapping.IpAddressUpdate ipAddressUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressDelete)]
        Mapping.IpAddressOperation ip_delete(string apiKey, int ipAddressId);
        #endregion

        #region VirtualMachine
        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineList)]
        Mapping.VirtualMachine[] vm_list(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineCreate)]
        Mapping.VirtualMachineOperation vm_create(string apiKey, Mapping.VirtualMachineCreation virtualMachineCreation);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineCreateFrom)]
        Mapping.VirtualMachineOperation vm_create_from(string apiKey, Mapping.VirtualMachineCreation virtualMachineCreation, Mapping.DiskCreation diskCreation, int sourceDiskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineUpdate)]
        Mapping.VirtualMachineOperation vm_update(string apiKey, int virtualMachineId, Mapping.VirtualMachineUpdate virtualMachineUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDelete)]
        Mapping.VirtualMachineOperation vm_delete(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineStart)]
        Mapping.VirtualMachineOperation vm_start(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineStop)]
        Mapping.VirtualMachineOperation vm_stop(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineReboot)]
        Mapping.VirtualMachineOperation vm_reboot(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineAttachDisk)]
        Mapping.VirtualMachineOperation vm_disk_attach(string apiKey, int virtualMachineId, int diskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDetachDisk)]
        Mapping.VirtualMachineOperation vm_disk_detach(string apiKey, int virtualMachineId, int diskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineAttachInterface)]
        Mapping.VirtualMachineOperation vm_iface_attach(string apiKey, int virtualMachineId, int ifaceId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDetachInterface)]
        Mapping.VirtualMachineOperation vm_iface_detach(string apiKey, int virtualMachineId, int ifaceId);
        #endregion

        #region Operation
        [XmlRpcMethod(XmlRpcMethodNames.OperationInfo)]
        Mapping.Operation operation_info(string apiKey, int operationId);
        #endregion
    }
}