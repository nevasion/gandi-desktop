namespace GandiDesktop.Gandi.Services.Hosting
{
    public class IpAddressOperation : Operation
    {
        public int IpAddressId { get; private set; }
        public IpAddress IpAddress { get; private set; }

        public IpAddressOperation(Mapping.IpAddressOperation operation, IpAddress ipAddress)
            : base(operation)
        {
            this.IpAddressId = operation.IpAddressId;
            this.IpAddress = ipAddress;
        }
    }
}