using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GandiDesktop.Gandi.Services.Hosting;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
{
    public class DesktopViewModel : ViewModelBase
    {
        private ResourceSettingInfo[] resourceInfos;

        public ObservableCollection<ResourceViewModel> ResourceViewModeCollection { get; private set; }

        public DesktopViewModel()
        {
            this.ResourceViewModeCollection = new ObservableCollection<ResourceViewModel>();
            this.resourceInfos = Settings.ResourceInfos;

            DataCenter[] dataCenters = Service.Hosting.DataCenter.List();
            Disk[] disks = Service.Hosting.Disk.List(dataCenters);
            IpAddress[] ipAddresses = Service.Hosting.IpAddress.List(dataCenters);
            Interface[] interfaces = Service.Hosting.Interface.List(dataCenters, ipAddresses);
            VirtualMachine[] virtualMachines = Service.Hosting.VirtualMachine.List(dataCenters, interfaces, disks);

            for (int i = 0; i < virtualMachines.Length; i++)
            {
                VirtualMachine virtualMachine = virtualMachines[i];

                ResourceViewModel resourceViewModel = new ResourceViewModel(new VirtualMachineResource(virtualMachine))
                {
                    Left = (i * 58) + 8,
                    Top = 10
                };

                this.ApplySettings(resourceViewModel);
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            IEnumerable<Disk> detachedDisks = disks.Where(d => d.VirtualMachineIds.Length == 0);
            for (int i = 0; i < detachedDisks.Count(); i++)
            {
                Disk disk = detachedDisks.ElementAt(i);

                if (disk.VirtualMachineIds.Length == 0)
                {
                    ResourceViewModel resourceViewModel = new ResourceViewModel(new DiskResource(disk))
                    {
                        Left = (i * 58) + 8,
                        Top = 80
                    };

                    this.ApplySettings(resourceViewModel); 
                    this.ResourceViewModeCollection.Add(resourceViewModel);
                }
            }

            IEnumerable<Interface> detachedInterfaces = interfaces.Where(i => i.VirtualMachineId == null);
            for (int i = 0; i < detachedInterfaces.Count(); i++)
            {
                Interface iface = detachedInterfaces.ElementAt(i);

                ResourceViewModel resourceViewModel = new ResourceViewModel(new InterfaceResource(iface))
                {
                    Left = (i * 58) + 8,
                    Top = 150
                };

                this.ApplySettings(resourceViewModel); 
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }
        }

        private void ApplySettings(ResourceViewModel resourceViewModel)
        {
            ResourceSettingInfo resourceInfo = this.resourceInfos.SingleOrDefault(r => r.Name == resourceViewModel.Name);
            if (resourceInfo != null)
            {
                resourceViewModel.Left = resourceInfo.Location.X;
                resourceViewModel.Top = resourceInfo.Location.Y;
                resourceViewModel.ZIndex = resourceInfo.ZIndex;
            }
        }

        public void SaveResourceLocations()
        {
            int zIndex = 0;
            List<ResourceSettingInfo> resourceInfoList = new List<ResourceSettingInfo>();
            foreach (ResourceViewModel resourceViewModel in this.ResourceViewModeCollection.OrderBy(r => r.ZIndex))
                resourceInfoList.Add(new ResourceSettingInfo()
                {
                    Name = resourceViewModel.Name,
                    Location = new Point(resourceViewModel.Left, resourceViewModel.Top),
                    ZIndex = zIndex++
                });

            Settings.ResourceInfos = resourceInfoList.ToArray();
            Settings.Save();
        }
    }
}