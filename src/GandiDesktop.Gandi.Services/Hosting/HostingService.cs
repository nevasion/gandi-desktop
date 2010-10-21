using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class HostingService
    {
        public DataCenterService DataCenter { get; private set; }
        public DiskService Disk { get; private set; }
        public InterfaceService Interface { get; private set; }
        public IpAddressService IpAddress { get; private set; }
        public VirtualMachineService VirtualMachine { get; private set; }

        public HostingService(string apiKey)
        {
            IHostingService service = XmlRpcProxyGen.Create<IHostingService>();

            this.DataCenter = new DataCenterService(service, apiKey);
            this.Disk = new DiskService(service, apiKey);
            this.Interface = new InterfaceService(service, apiKey);
            this.IpAddress = new IpAddressService(service, apiKey);
            this.VirtualMachine = new VirtualMachineService(service, apiKey);
        }
    }
}