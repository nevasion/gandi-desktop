using GandiDesktop.Gandi.Services.Hosting;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GandiDesktop.Presentation.Model
{
    public class HostingService
    {
        private GandiDesktop.Gandi.Services.Hosting.HostingService service;

        private List<IHostingResource> resources;

        public IQueryable<DataCenter> DataCenters { get { return resources.Cast<DataCenter>().AsQueryable<DataCenter>(); } }
        public IQueryable<Disk> Disks { get { return resources.Cast<Disk>().AsQueryable<Disk>(); } }
        public IQueryable<Interface> Interfaces { get { return resources.Cast<Interface>().AsQueryable<Interface>(); } }
        public IQueryable<Image> Images { get { return resources.Cast<Image>().AsQueryable<Image>(); } }
        public IQueryable<IpAddress> IpAddresses { get { return resources.Cast<IpAddress>().AsQueryable<IpAddress>(); } }
        public IQueryable<VirtualMachine> VirtualMachines { get { return resources.Cast<VirtualMachine>().AsQueryable<VirtualMachine>(); } }

        public DataCenterService DataCenter { get { return service.DataCenter; } }

        public DiskService Disk { get { return service.Disk; } }

        public InterfaceService Interface { get { return service.Interface; } }

        public ImageService Image { get { return service.Image; } }

        public IpAddressService IpAddress { get { return service.IpAddress; } }

        public VirtualMachineService VirtualMachine { get { return service.VirtualMachine; } }

        public HostingService(GandiDesktop.Gandi.Services.Hosting.HostingService hostingService)
        {
            service = hostingService;
        }

        public T Fetch<T>(int id) where T : IHostingResource
        {
            if (typeof(T) == typeof(Disk))
            {
                object disk = service.Disk.Single(id, this.DataCenters.ToArray());

                this.AddOrUpdate((IHostingResource)disk);

                return (T)disk;
            }
            else if (typeof(T) == typeof(Interface))
            {
                object iface = service.Interface.Single(id, this.DataCenters.ToArray(), this.IpAddresses.ToArray());

                this.AddOrUpdate((IHostingResource)iface);

                return (T)iface;
            }
            else if (typeof(T) == typeof(VirtualMachine))
            {
                object virtualMachine = service.VirtualMachine.Single(id, this.DataCenters.ToArray(), this.Interfaces.ToArray(), this.Disks.ToArray());

                this.AddOrUpdate((IHostingResource)virtualMachine);

                return (T)virtualMachine;
            }
            else
            {
                return default(T);
            }
        }

        public void FetchAll(bool image)
        {
            DataCenter[] dataCenters = this.service.DataCenter.List();
            Disk[] disks = this.service.Disk.List(dataCenters);
            IpAddress[] ipAddresses = this.service.IpAddress.List(dataCenters);
            Interface[] interfaces = this.service.Interface.List(dataCenters, ipAddresses);
            VirtualMachine[] virtualMachines = this.service.VirtualMachine.List(dataCenters, interfaces, disks);

            this.resources.Clear();

            if (image)
                this.resources.AddRange(this.service.Image.List(dataCenters));

            this.resources.AddRange(dataCenters);
            this.resources.AddRange(disks);
            this.resources.AddRange(ipAddresses);
            this.resources.AddRange(interfaces);
            this.resources.AddRange(virtualMachines);
        }

        public void AddOrUpdate(IHostingResource resource)
        {
            if (this.resources.Contains(resource))
            {
                IHostingResource oldResource = this.resources.Single(r => r.Id == resource.Id);
                this.resources[this.resources.IndexOf(oldResource)] = resource;
            }
            else
            {
                this.resources.Add(resource);
            }
        }

        public void Remove(IHostingResource resource)
        {
            this.resources.Remove(resource);
        }

        public Operation UpdateOperation(Operation operation)
        {
            return service.Operation.Update(operation);
        }
    }
}
