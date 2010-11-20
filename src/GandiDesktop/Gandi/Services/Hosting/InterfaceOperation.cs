﻿namespace GandiDesktop.Gandi.Services.Hosting
{
    public class InterfaceOperation : Operation
    {
        public int InterfaceId { get; private set; }
        public Interface Interface { get; private set; }

        public int? IpAddressId { get; private set; }
        public IpAddress IpAddress { get; private set; }

        public InterfaceOperation(Mapping.InterfaceOperation operation, Interface iface)
            : this(operation, iface, null, null)
        {
        }

        public InterfaceOperation(Mapping.InterfaceOperation operation, Interface iface, IpAddress ipAddress)
            : this(operation, iface, ipAddress, null)
        {
        }

        private InterfaceOperation(Mapping.InterfaceOperation operation, Interface iface, IpAddress ipAddress, object dummy)
            : base(operation)
        {
            this.InterfaceId = operation.InterfaceId;
            this.Interface = iface;

            this.IpAddressId = operation.IpAddressId;
            this.IpAddress = ipAddress;
        }
    }
}