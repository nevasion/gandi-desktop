namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachineOperation : Operation
    {
        public int? VirtualMachineId { get; private set; }
        public VirtualMachine VirtualMachine { get; private set; }

        public int? DiskId { get; private set; }
        public Disk Disk { get; private set; }

        public int? IntefaceId {get; private set; }
        public Interface Interface { get; private set; }

        public VirtualMachineOperation(Mapping.VirtualMachineOperation operation, VirtualMachine virtualMachine)
            : this(operation, virtualMachine, null, null)
        {
        }

        public VirtualMachineOperation(Mapping.VirtualMachineOperation operation, VirtualMachine virtualMachine, Disk disk)
            : this(operation, virtualMachine, disk, null)
        {
        }

        public VirtualMachineOperation(Mapping.VirtualMachineOperation operation, VirtualMachine virtualMachine, Interface iface)
            : this(operation, virtualMachine, null, iface)
        {
        }

        private VirtualMachineOperation(Mapping.VirtualMachineOperation operation, VirtualMachine virtualMachine, Disk disk, Interface iface)
            : base(operation)
        {
            this.VirtualMachineId = operation.VirtualMachineId;
            this.VirtualMachine = virtualMachine;

            this.DiskId = operation.DiskId;
            this.Disk = disk;

            this.IntefaceId = operation.InterfaceId;
            this.Interface = iface;
        }
    }
}