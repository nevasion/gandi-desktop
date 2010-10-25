using System.Collections.ObjectModel;
using GandiDesktop.Gandi.Services;
using GandiDesktop.Gandi.Services.Hosting;
using GandiDesktop.Model;

namespace GandiDesktop.ViewModel
{
    public class DesktopViewModel : ViewModelBase
    {
        public ObservableCollection<ResourceViewModel> ResourceViewModeCollection { get; private set; }

        public DesktopViewModel()
        {
            this.ResourceViewModeCollection = new ObservableCollection<ResourceViewModel>();

            GandiService gandiService = new GandiService(System.IO.File.ReadAllText("apikey.txt"));

            DataCenter[] dataCenters = gandiService.Hosting.DataCenter.List();
            Disk[] disks = gandiService.Hosting.Disk.List(dataCenters);
            IpAddress[] ipAddresses = gandiService.Hosting.IpAddress.List(dataCenters);
            Interface[] interfaces = gandiService.Hosting.Interface.List(dataCenters, ipAddresses);
            VirtualMachine[] virtualMachines = gandiService.Hosting.VirtualMachine.List(dataCenters, interfaces, disks);

            for (int i = 0; i < virtualMachines.Length; i++)
            {
                VirtualMachine virtualMachine = virtualMachines[i];

                ResourceViewModel resourceViewModel = new ResourceViewModel(new VirtualMachineResource(virtualMachine))
                {
                    Left = (i * 58) + 8,
                    Top = 10
                };

                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            for (int i = 0; i < disks.Length; i++)
            {
                Disk disk = disks[i];

                ResourceViewModel resourceViewModel = new ResourceViewModel(new DiskResource(disk))
                {
                    Left = (i * 58) + 8,
                    Top = 80
                };

                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            for (int i = 0; i < interfaces.Length; i++)
            {
                Interface iface = interfaces[i];

                ResourceViewModel resourceViewModel = new ResourceViewModel(new InterfaceResource(iface))
                {
                    Left = (i * 58) + 8,
                    Top = 150
                };

                this.ResourceViewModeCollection.Add(resourceViewModel);
            }
        }
    }
}