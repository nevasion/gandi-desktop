using CookComputing.XmlRpc;
using Model = GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services.Hosting.Mapping
{
    public class VirtualMachineUpdate
    {
        private class XmlRpcMappingNames
        {
            public const string MaxMemory = "vm_max_memory";
            public const string Shares = "shares"; 
            public const string Memory = "memory";
            public const string IsRescueConsoleActive = "console";
            public const string UnixPassword = "password";
        }

        [XmlRpcMember(XmlRpcMappingNames.MaxMemory)]
        public int? MaxMemory { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Shares)]
        public int? Shares { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.Memory)]
        public int? Memory { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.IsRescueConsoleActive)]
        public int? IsRescueConsoleActive { get; set; }

        [XmlRpcMember(XmlRpcMappingNames.UnixPassword)]
        public string UnixPassword { get; set; }

        public VirtualMachineUpdate(Model.VirtualMachineUpdate virtualMachineUpdate)
        {
            this.MaxMemory = virtualMachineUpdate.MaxMemory;
            this.Shares = virtualMachineUpdate.Shares;
            this.Memory = virtualMachineUpdate.Memory;
            if (virtualMachineUpdate.IsRescueConsoleActive.HasValue)
                this.IsRescueConsoleActive = (virtualMachineUpdate.IsRescueConsoleActive.Value ? 1 : 0);
            this.UnixPassword = virtualMachineUpdate.UnixPassword;
        }
    }
}