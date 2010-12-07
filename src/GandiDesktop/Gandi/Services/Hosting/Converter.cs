using System;
namespace GandiDesktop.Gandi.Services.Hosting
{
    public enum DiskType
    {
        Data,
        Backup
    }

    public enum ImageArchitecture
    {
        x8632,
        x8664
    }

    public enum ImageVisibility
    {
        Public,
        Private,
        Alpha
    }

    public enum InterfaceStatus
    {
        Free,
        Used
    }

    public enum InterfaceType
    {
        Public,
        Private
    }

    public enum IpAddressStatus
    {
        Free,
        Used
    }

    public enum IpVersion
    {
        v4,
        v6
    }

    public enum OperationStep
    {
        Billing,
        Waiting,
        Running,
        Done,
        Error,
        Cancelled
    }

    [Flags]
    public enum OperationType
    {
        None = 0,
        Create = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        Attach = 1 << 3,
        Detach = 1 << 4,
        Start = 1 << 5,
        Stop = 1 << 6,
        Reboot = 1 << 7,
        VirtualMachine = 1 << 8,
        Disk = 1 << 9,
        Interface = 1 << 10,
        IpAddress = 1 << 11
    }

    public enum VirtualMachineStatus
    {
        BeingCreated,
        Halted,
        Running,
        Paused,
        Locked,
        Invalid,
        LegallyLocked,
        Deleted
    }

    public static class Converter
    {
        private const string DiskTypeData = "data";
        private const string DiskTypeBackup = "backup";

        private const string ImageArchitectureX8632 = "x86-32";
        private const string ImageArchitectureX8664 = "x86-64";

        private const string ImageVisibilityPublic = "public";
        private const string ImageVisibilityPrivate = "private";
        private const string ImageVisibilityAlpha = "alpha";

        private const string InterfaceStatusFree = "free";
        private const string InterfaceStatusUsed = "used";

        private const string InterfaceTypePublic = "public";
        private const string InterfaceTypePrivate = "private";

        private const string IpAddressStatusFree = "free";
        private const string IpAddressStatusUsed = "used";

        private const int IpVersion4 = 4;
        private const int IpVersion6 = 6;

        private const string OperationStepBilling = "BILL";
        private const string OperationStepWaiting = "WAIT";
        private const string OperationStepRunning = "RUN";
        private const string OperationStepDone = "DONE";
        private const string OperationStepError = "ERROR";
        private const string OperationStepCancelled = "CANCEL";

        private const string OperationTypeVirtualMachineCreate = "vm_create";
        private const string OperationTypeVirtualMachineUpdate = "vm_update";
        private const string OperationTypeVirtualMachineDelete = "vm_delete";
        private const string OperationTypeVirtualMachineStart = "vm_start";
        private const string OperationTypeVirtualMachineStop = "vm_stop";
        private const string OperationTypeVirtualMachineReboot = "vm_reboot";
        private const string OperationTypeDiskCreate = "disk_create";
        private const string OperationTypeDiskUpdate = "disk_update";
        private const string OperationTypeDiskDelete = "disk_delete";
        private const string OperationTypeDiskAttach = "disk_attach";
        private const string OperationTypeDiskDetach = "disk_detach";
        private const string OperationTypeInterfaceCreate = "iface_create";
        private const string OperationTypeInterfaceUpdate = "iface_update";
        private const string OperationTypeInterfaceDelete = "iface_delete";
        private const string OperationTypeInterfaceAttach = "iface_attach";
        private const string OperationTypeInterfaceDetach = "iface_detach";
        private const string OperationTypeIpAddressCreate = "ip_create";
        private const string OperationTypeIpAddressUpdate = "ip_update";
        private const string OperationTypeIpAddressDelete = "ip_delete";
        private const string OperationTypeIpAddressAttach = "ip_attach";
        private const string OperationTypeIpAddressDetach = "ip_detach";

        private const string VirtualMachineBeingCreated = "being_created";
        private const string VirtualMachineHalted = "halted";
        private const string VirtualMachineRunning = "running";
        private const string VirtualMachinePaused = "paused";
        private const string VirtualMachineLocked = "locked";
        private const string VirtualMachineInvalid = "invalid";
        private const string VirtualMachineLegallyLocked = "legally_locked";
        private const string VirtualMachineDeleted = "deleted";

        public static DiskType ToDiskType(string diskType)
        {
            switch (diskType)
            {
                case Converter.DiskTypeBackup:
                    return DiskType.Backup;
                case Converter.DiskTypeData:
                    return DiskType.Data;
                default:
                    throw new InvalidCastException();
            }
        }

        public static string ToDiskType(DiskType diskType)
        {
            switch (diskType)
            {
                case DiskType.Backup:
                    return Converter.DiskTypeBackup;
                case DiskType.Data:
                    return Converter.DiskTypeData;
                default:
                    throw new InvalidCastException();
            }
        }

        public static ImageArchitecture ToImageArchitecture(string imageArchitecture)
        {
            switch (imageArchitecture)
            {
                case Converter.ImageArchitectureX8632:
                    return ImageArchitecture.x8632;
                case Converter.ImageArchitectureX8664:
                    return ImageArchitecture.x8664;
                default:
                    throw new InvalidCastException();
            }
        }

        public static ImageVisibility ToImageVisibility(string imageVisibility)
        {
            switch (imageVisibility)
            {
                case Converter.ImageVisibilityPublic:
                    return ImageVisibility.Public;
                case Converter.ImageVisibilityPrivate:
                    return ImageVisibility.Private;
                case Converter.ImageVisibilityAlpha:
                    return ImageVisibility.Alpha;
                default:
                    throw new InvalidCastException();
            }
        }

        public static InterfaceStatus ToInterfaceStatus(string interfaceStatus)
        {
            switch (interfaceStatus)
            {
                case Converter.InterfaceStatusFree:
                    return InterfaceStatus.Free;
                case Converter.InterfaceStatusUsed:
                    return InterfaceStatus.Used;
                default:
                    throw new InvalidCastException();
            }
        }

        public static InterfaceType ToInterfaceType(string interfaceType)
        {
            switch (interfaceType)
            {
                case Converter.InterfaceTypePublic:
                    return InterfaceType.Public;
                case Converter.InterfaceTypePrivate:
                    return InterfaceType.Private;
                default:
                    throw new InvalidCastException();
            }
        }

        public static string ToInterfaceType(InterfaceType interfaceType)
        {
            switch (interfaceType)
            {
                case InterfaceType.Public:
                    return Converter.InterfaceTypePublic;
                case InterfaceType.Private:
                    return Converter.InterfaceTypePrivate;
                default:
                    throw new InvalidCastException();
            }
        }

        public static IpAddressStatus ToIpAddressStatus(string ipAddressStatus)
        {
            switch (ipAddressStatus)
            {
                case Converter.IpAddressStatusFree:
                    return IpAddressStatus.Free;
                case Converter.IpAddressStatusUsed:
                    return IpAddressStatus.Used;
                default:
                    throw new InvalidCastException();
            }
        }

        public static IpVersion ToIpVersion(int ipVersion)
        {
            switch (ipVersion)
            {
                case Converter.IpVersion4:
                    return IpVersion.v4;
                case Converter.IpVersion6:
                    return IpVersion.v6;
                default:
                    throw new InvalidCastException();
            }
        }

        public static int ToIpVersion(IpVersion ipVersion)
        {
            switch (ipVersion)
            {
                case IpVersion.v4:
                    return Converter.IpVersion4;
                case IpVersion.v6:
                    return Converter.IpVersion6;
                default:
                    throw new InvalidCastException();
            }
        }

        public static OperationStep ToOperationStep(string operationStep)
        {
            switch (operationStep)
            {
                case Converter.OperationStepBilling:
                    return OperationStep.Billing;
                case Converter.OperationStepWaiting:
                    return OperationStep.Waiting;
                case Converter.OperationStepRunning:
                    return OperationStep.Running;
                case Converter.OperationStepDone:
                    return OperationStep.Done;
                case Converter.OperationStepError:
                    return OperationStep.Error;
                case Converter.OperationStepCancelled:
                    return OperationStep.Cancelled;
                default:
                    throw new InvalidCastException();
            }
        }

        public static OperationType ToOperationType(string operationType)
        {
            switch (operationType)
            {
                case Converter.OperationTypeVirtualMachineCreate:
                    return OperationType.Create | OperationType.VirtualMachine;
                case Converter.OperationTypeVirtualMachineUpdate:
                    return OperationType.Update | OperationType.VirtualMachine;
                case Converter.OperationTypeVirtualMachineDelete:
                    return OperationType.Delete | OperationType.VirtualMachine;
                case Converter.OperationTypeVirtualMachineStart:
                    return OperationType.Start | OperationType.VirtualMachine;
                case Converter.OperationTypeVirtualMachineStop:
                    return OperationType.Stop | OperationType.VirtualMachine;
                case Converter.OperationTypeVirtualMachineReboot:
                    return OperationType.Reboot | OperationType.VirtualMachine;
                case Converter.OperationTypeDiskCreate:
                    return OperationType.Create | OperationType.Disk;
                case Converter.OperationTypeDiskUpdate:
                    return OperationType.Update | OperationType.Disk;
                case Converter.OperationTypeDiskDelete:
                    return OperationType.Delete | OperationType.Disk;
                case Converter.OperationTypeDiskAttach:
                    return OperationType.Attach | OperationType.Disk;
                case Converter.OperationTypeDiskDetach:
                    return OperationType.Detach | OperationType.Disk;
                case Converter.OperationTypeInterfaceCreate:
                    return OperationType.Create | OperationType.Interface;
                case Converter.OperationTypeInterfaceUpdate:
                    return OperationType.Update | OperationType.Interface;
                case Converter.OperationTypeInterfaceDelete:
                    return OperationType.Delete | OperationType.Interface;
                case Converter.OperationTypeInterfaceAttach:
                    return OperationType.Attach | OperationType.Interface;
                case Converter.OperationTypeInterfaceDetach:
                    return OperationType.Detach | OperationType.Interface;
                case Converter.OperationTypeIpAddressCreate:
                    return OperationType.Create | OperationType.IpAddress;
                case Converter.OperationTypeIpAddressUpdate:
                    return OperationType.Update | OperationType.IpAddress;
                case Converter.OperationTypeIpAddressDelete:
                    return OperationType.Delete | OperationType.IpAddress;
                case Converter.OperationTypeIpAddressAttach:
                    return OperationType.Attach | OperationType.IpAddress;
                case Converter.OperationTypeIpAddressDetach:
                    return OperationType.Detach | OperationType.IpAddress;
                default:
                    throw new InvalidCastException();
            }
        }

        public static VirtualMachineStatus ToVirtualMachineStatus(string virtualMachineStatus)
        {
            switch (virtualMachineStatus)
            {
                case Converter.VirtualMachineBeingCreated:
                    return VirtualMachineStatus.BeingCreated;
                case Converter.VirtualMachineHalted:
                    return VirtualMachineStatus.Halted;
                case Converter.VirtualMachineRunning:
                    return VirtualMachineStatus.Running;
                case Converter.VirtualMachinePaused:
                    return VirtualMachineStatus.Paused;
                case Converter.VirtualMachineLocked:
                    return VirtualMachineStatus.Locked;
                case Converter.VirtualMachineInvalid:
                    return VirtualMachineStatus.Invalid;
                case Converter.VirtualMachineLegallyLocked:
                    return VirtualMachineStatus.LegallyLocked;
                case Converter.VirtualMachineDeleted:
                    return VirtualMachineStatus.Deleted;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}