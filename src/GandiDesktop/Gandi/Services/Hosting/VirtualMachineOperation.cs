namespace GandiDesktop.Gandi.Services.Hosting
{
    public class VirtualMachineOperation : Operation
    {
        private Mapping.VirtualMachineOperation virtualMachineOperation;
        private Hosting.Interface iface;

        public int? VirtualMachineId { get; private set; }
        public VirtualMachine VirtualMachine { get; private set; }

        public int? DiskId { get; private set; }
        public Disk Disk { get; private set; }

        public int? IntefaceId {get; private set; }
        public Interface Interface { get; private set; }

        public VirtualMachineOperation(Mapping.VirtualMachineOperation operation, VirtualMachine virtualMachine, Disk disk = null, Interface iface = null)
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