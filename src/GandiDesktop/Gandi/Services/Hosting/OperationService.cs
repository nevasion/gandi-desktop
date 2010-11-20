namespace GandiDesktop.Gandi.Services.Hosting
{
    public class OperationService
    {
        private IHostingService service;
        private string apiKey;

        public OperationService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public VirtualMachineOperation Update(VirtualMachineOperation virtualMachineOperation)
        {
            Operation operation = this.Info(virtualMachineOperation.Id);

            virtualMachineOperation.LastUpdated = operation.LastUpdated;
            virtualMachineOperation.Step = operation.Step;

            return virtualMachineOperation;
        }

        public DiskOperation Update(DiskOperation diskOperation)
        {
            Operation operation = this.Info(diskOperation.Id);

            diskOperation.LastUpdated = operation.LastUpdated;
            diskOperation.Step = operation.Step;

            return diskOperation;
        }

        public InterfaceOperation Update(InterfaceOperation interfaceOperation)
        {
            Operation operation = this.Info(interfaceOperation.Id);

            interfaceOperation.LastUpdated = operation.LastUpdated;
            interfaceOperation.Step = operation.Step;

            return interfaceOperation;
        }

        public IpAddressOperation Update(IpAddressOperation ipAddressOperation)
        {
            Operation operation = this.Info(ipAddressOperation.Id);

            ipAddressOperation.LastUpdated = operation.LastUpdated;
            ipAddressOperation.Step = operation.Step;

            return ipAddressOperation;
        }

        private Operation Info(int operationId)
        {
            return new Operation(this.service.operation_info(this.apiKey, operationId));
        }
    }
}