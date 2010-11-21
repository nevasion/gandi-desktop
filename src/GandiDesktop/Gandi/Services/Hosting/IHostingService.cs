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
        Mapping.DataCenter[] DataCenterList(string apiKey);
        #endregion

        #region Disk
        [XmlRpcMethod(XmlRpcMethodNames.DiskList)]
        Mapping.Disk[] DiskList(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.DiskCreate)]
        Mapping.DiskOperation DiskCreate(string apiKey, Mapping.DiskCreation diskCreation);

        [XmlRpcMethod(XmlRpcMethodNames.DiskCreateFrom)]
        Mapping.DiskOperation DiskCreateFrom(string apiKey, Mapping.DiskCopy diskCopy, int sourceDiskId);

        [XmlRpcMethod(XmlRpcMethodNames.DiskUpdate)]
        Mapping.DiskOperation DiskUpdate(string apiKey, int diskId, Mapping.DiskUpdate diskUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.DiskDelete)]
        Mapping.DiskOperation DiskDelete(string apiKey, int diskId);
        #endregion

        #region Interface
        [XmlRpcMethod(XmlRpcMethodNames.InterfaceList)]
        Mapping.Interface[] InterfaceList(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceCreate)]
        Mapping.InterfaceOperation InterfaceCreate(string apiKey, Mapping.InterfaceCreation ifaceCreation);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceUpdate)]
        Mapping.InterfaceOperation InterfaceUpdate(string apiKey, int ifaceId, Mapping.InterfaceUpdate ifaceUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDelete)]
        Mapping.InterfaceOperation InterfaceDelete(string apiKey, int ifaceId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceAttachIp)]
        Mapping.InterfaceOperation InterfaceAttachIp(string apiKey, int ifaceId, int ipAddressId);

        [XmlRpcMethod(XmlRpcMethodNames.InterfaceDetachIp)]
        Mapping.InterfaceOperation InterfaceDetachIp(string apiKey, int ifaceId, int ipAddressId);
        #endregion

        #region Image
        [XmlRpcMethod(XmlRpcMethodNames.ImageList)]
        Mapping.Image[] ImageList(string apiKey);
        #endregion

        #region IpAddress
        [XmlRpcMethod(XmlRpcMethodNames.IpAddressList)]
        Mapping.IpAddress[] IpAddressList(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressCreate)]
        Mapping.IpAddressOperation IpAddressCreate(string apiKey, Mapping.IpAddressCreation ipAddressCreation);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressUpdate)]
        Mapping.IpAddressOperation IpAddressUpdate(string apiKey, int ipAddressId, Mapping.IpAddressUpdate ipAddressUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.IpAddressDelete)]
        Mapping.IpAddressOperation IpAddressDelete(string apiKey, int ipAddressId);
        #endregion

        #region VirtualMachine
        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineList)]
        Mapping.VirtualMachine[] VirtualMachineList(string apiKey);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineCreate)]
        Mapping.VirtualMachineOperation VirtualMachineCreate(string apiKey, Mapping.VirtualMachineCreation virtualMachineCreation);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineCreateFrom)]
        Mapping.VirtualMachineOperation VirtualMachineCreateFrom(string apiKey, Mapping.VirtualMachineCreation virtualMachineCreation, Mapping.DiskCreation diskCreation, int sourceDiskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineUpdate)]
        Mapping.VirtualMachineOperation VirtualMachineUpdate(string apiKey, int virtualMachineId, Mapping.VirtualMachineUpdate virtualMachineUpdate);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDelete)]
        Mapping.VirtualMachineOperation VirtualMachineDelete(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineStart)]
        Mapping.VirtualMachineOperation VirtualMachineStart(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineStop)]
        Mapping.VirtualMachineOperation VirtualMachineStop(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineReboot)]
        Mapping.VirtualMachineOperation VirtualMachineReboot(string apiKey, int virtualMachineId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineAttachDisk)]
        Mapping.VirtualMachineOperation VirtualMachineAttachDisk(string apiKey, int virtualMachineId, int diskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDetachDisk)]
        Mapping.VirtualMachineOperation VirtualMachineDetachDisk(string apiKey, int virtualMachineId, int diskId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineAttachInterface)]
        Mapping.VirtualMachineOperation VirtualMachineAttachInterface(string apiKey, int virtualMachineId, int ifaceId);

        [XmlRpcMethod(XmlRpcMethodNames.VirtualMachineDetachInterface)]
        Mapping.VirtualMachineOperation VirtualMachineDetachInterface(string apiKey, int virtualMachineId, int ifaceId);
        #endregion

        #region Operation
        [XmlRpcMethod(XmlRpcMethodNames.OperationInfo)]
        Mapping.Operation OperationInfo(string apiKey, int operationId);
        #endregion
    }
}