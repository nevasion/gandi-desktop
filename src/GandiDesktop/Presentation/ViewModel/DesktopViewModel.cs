using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GandiDesktop.Gandi.Services.Hosting;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
{
    public class DesktopViewModel : ViewModelBase
    {
        private bool displayError;
        public bool DisplayError
        {
            get { return this.displayError; }
            set
            {
                if (this.displayError != value)
                {
                    this.displayError = value;
                    base.OnPropertyChanged(() => DisplayError);
                }
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    base.OnPropertyChanged(() => ErrorMessage);
                }
            }
        }

        private ICommand closeErrorCommand;
        public ICommand CloseErrorCommand
        {
            get
            {
                if (this.closeErrorCommand == null)
                    this.closeErrorCommand = new RelayCommand((parameter) => CloseError(parameter));

                return this.closeErrorCommand;
            }
        }

        public ObservableCollection<ResourceViewModel> ResourceViewModeCollection { get; private set; }
        public OperationsViewModel OperationsViewModel { get; private set; }

        private List<ResourceSettingInfo> resourceInfoList = null; 
        private List<DataCenter> dataCenterList = null;
        private List<Disk> diskList = null;
        private List<IpAddress> ipAddresseList = null;
        private List<Interface> interfaceList = null;
        private List<VirtualMachine> virtualMachineList = null;

        public DesktopViewModel()
        {
            this.ResourceViewModeCollection = new ObservableCollection<ResourceViewModel>();
            this.OperationsViewModel = new OperationsViewModel();

            this.resourceInfoList = new List<ResourceSettingInfo>(Settings.ResourceInfos);

            DataCenter[] dataCenters = Service.Hosting.DataCenter.List();
            Disk[] disks = Service.Hosting.Disk.List(dataCenters);
            IpAddress[] ipAddresses = Service.Hosting.IpAddress.List(dataCenters);
            Interface[] interfaces = Service.Hosting.Interface.List(dataCenters, ipAddresses);

            this.dataCenterList = new List<DataCenter>(dataCenters);
            this.diskList = new List<Disk>(Service.Hosting.Disk.List(dataCenters));
            this.ipAddresseList = new List<IpAddress>(Service.Hosting.IpAddress.List(dataCenters));
            this.interfaceList = new List<Interface>(Service.Hosting.Interface.List(dataCenters, ipAddresses));
            this.virtualMachineList = new List<VirtualMachine>(Service.Hosting.VirtualMachine.List(dataCenters, interfaces, disks));

            for (int i = 0; i < this.virtualMachineList.Count; i++)
            {
                VirtualMachine virtualMachine = this.virtualMachineList[i];

                ResourceViewModel resourceViewModel = new ResourceViewModel(new VirtualMachineResource(virtualMachine))
                {
                    Left = (i * 58) + 8,
                    Top = 10
                };

                resourceViewModel.DetailAction += OnDetailAction;

                this.ApplySettings(resourceViewModel);
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            IEnumerable<Disk> detachedDisks = this.diskList.Where(d => d.VirtualMachineIds.Length == 0);
            for (int i = 0; i < detachedDisks.Count(); i++)
            {
                Disk disk = detachedDisks.ElementAt(i);

                ResourceViewModel resourceViewModel = new ResourceViewModel(new DiskResource(disk))
                {
                    Left = (i * 58) + 8,
                    Top = 80
                };

                resourceViewModel.DetailAction += OnDetailAction;

                this.ApplySettings(resourceViewModel);
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            IEnumerable<Interface> detachedInterfaces = this.interfaceList.Where(i => i.VirtualMachineId == null);
            for (int i = 0; i < detachedInterfaces.Count(); i++)
            {
                Interface iface = detachedInterfaces.ElementAt(i);

                ResourceViewModel resourceViewModel = new ResourceViewModel(new InterfaceResource(iface))
                {
                    Left = (i * 58) + 8,
                    Top = 150
                };

                resourceViewModel.DetailAction += OnDetailAction;

                this.ApplySettings(resourceViewModel);
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }
        }

        public void CloseError(object parameter)
        {
            this.DisplayError = false;
        }

        private void OnDetailAction(object sender, ResourceDetailActionEventArgs e)
        {
            if (e.Error)
            {
                this.ErrorMessage = e.ErrorMessage;
                this.DisplayError = true;
            }
            else
            {
                if (e.Operation != null)
                {
                    OperationViewModel operationViewModel = new OperationViewModel(e.Operation);

                    operationViewModel.Refreshed += delegate
                    {
                        ResourceViewModel resourceViewModel = (ResourceViewModel)sender;

                        if (operationViewModel.Step == OperationStep.Done)
                        {
                            this.OnOperationCompleted(operationViewModel, resourceViewModel);
                        }
                    };

                    this.OperationsViewModel.OperationViewModelCollection.Add(operationViewModel);
                }
            }
        }

        private void OnOperationCompleted(OperationViewModel operationViewModel, ResourceViewModel resourceViewModel)
        {
            if ((operationViewModel.Type & OperationType.Detach) == OperationType.Detach
                || (operationViewModel.Type & OperationType.Start) == OperationType.Start
                || (operationViewModel.Type & OperationType.Stop) == OperationType.Stop
                || (operationViewModel.Type & OperationType.Reboot) == OperationType.Reboot
                || (operationViewModel.Type & OperationType.Update) == OperationType.Update)
            {
                this.UpdateResource(resourceViewModel, operationViewModel);
            }

            if ((operationViewModel.Type & OperationType.Detach) == OperationType.Detach
                || (operationViewModel.Type & OperationType.Create) == OperationType.Create)
            {
                DiskOperation diskOperation = operationViewModel.Operation as DiskOperation;
                InterfaceOperation interfaceOperation = operationViewModel.Operation as InterfaceOperation;
                IpAddressOperation ipAddressOperation = operationViewModel.Operation as IpAddressOperation;
                VirtualMachineOperation virtualMachineOperation = operationViewModel.Operation as VirtualMachineOperation;

                if (diskOperation != null)
                {
                    Disk disk = Service.Hosting.Disk.Single(diskOperation.DiskId, this.dataCenterList.ToArray());

                    this.AddResource(new DiskResource(disk), 20, 20);
                }
                else if (interfaceOperation != null)
                {
                    Interface iface = Service.Hosting.Interface.Single(interfaceOperation.InterfaceId, this.dataCenterList.ToArray(), this.ipAddresseList.ToArray());

                    this.AddResource(new InterfaceResource(iface), 20, 20);
                }
                else if (ipAddressOperation != null)
                {
                    //IpAddress ipAddress = Service.Hosting.IpAddress.Single(ipAddressOperation.IpAddressId, this.dataCenterList.ToArray());
                }
                else if (virtualMachineOperation != null)
                {
                    if ((operationViewModel.Type & OperationType.Detach) == OperationType.Detach)
                    {
                        if (virtualMachineOperation.Disk != null)
                        {
                            this.AddResource(new DiskResource(virtualMachineOperation.Disk), 20, 20);
                        }
                        else if (virtualMachineOperation.Interface != null)
                        {
                            this.AddResource(new InterfaceResource(virtualMachineOperation.Interface), 20, 20);
                        }
                    }
                    else if ((operationViewModel.Type & OperationType.Create) == OperationType.Create)
                    {
                        VirtualMachine virtualMachine = Service.Hosting.VirtualMachine.Single(virtualMachineOperation.VirtualMachineId.Value, this.dataCenterList.ToArray(), this.interfaceList.ToArray(), this.diskList.ToArray());

                        this.AddResource(new VirtualMachineResource(virtualMachine), 20, 20);
                    }
                }
            }
            else if ((operationViewModel.Type & OperationType.Attach) == OperationType.Attach
                || (operationViewModel.Type & OperationType.Delete) == OperationType.Delete)
            {
                this.RemoveResource(resourceViewModel);
            }
        }

        private void AddResource(IResource resource, double left, double top)
        {
            if (resource.Resource is Disk)
                this.diskList.Add(resource.Resource as Disk);
            else if (resource.Resource is Interface)
                this.interfaceList.Add(resource.Resource as Interface);
            else if (resource.Resource is IpAddress)
                this.ipAddresseList.Add(resource.Resource as IpAddress);
            else if (resource.Resource is VirtualMachine)
                this.virtualMachineList.Add(resource.Resource as VirtualMachine);

            ResourceViewModel resourceViewModel = new ResourceViewModel(resource)
            {
                Left = left,
                Top = top,
                ZIndex = (this.GetMaxResourceZIndex() + 1)
            };

            resourceViewModel.DetailAction += OnDetailAction;

            this.ResourceViewModeCollection.Add(resourceViewModel);
        }

        private void UpdateResource(ResourceViewModel resourceViewModel, OperationViewModel operationViewModel)
        {
            if (resourceViewModel.Type == ResourceType.Disk)
            {
                Disk newdisk = Service.Hosting.Disk.Single(resourceViewModel.Id, this.dataCenterList.ToArray());
                Disk oldDisk = this.diskList.Single(d => d.Id == newdisk.Id);

                oldDisk = newdisk;

                resourceViewModel.Update(new DiskResource(oldDisk));
            }
            else if (resourceViewModel.Type == ResourceType.Interface)
            {
                Interface newInterface = Service.Hosting.Interface.Single(resourceViewModel.Id, this.dataCenterList.ToArray(), this.ipAddresseList.ToArray());
                Interface oldInterface = this.interfaceList.Single(i => i.Id == newInterface.Id);

                oldInterface = newInterface;

                resourceViewModel.Update(new InterfaceResource(oldInterface));
            }
            else if (resourceViewModel.Type == ResourceType.VirtualMachine)
            {
                VirtualMachine newVirtualMachine = Service.Hosting.VirtualMachine.Single(resourceViewModel.Id, this.dataCenterList.ToArray(), this.interfaceList.ToArray(), this.diskList.ToArray());
                VirtualMachine oldVirtualMachine = this.virtualMachineList.Single(v => v.Id == newVirtualMachine.Id);

                oldVirtualMachine = newVirtualMachine;

                resourceViewModel.Update(new VirtualMachineResource(oldVirtualMachine));
            }
        }

        private void RemoveResource(ResourceViewModel resourceViewModel)
        {
            if (resourceViewModel.Resource is Disk)
                this.diskList.Remove(resourceViewModel.Resource as Disk);
            else if (resourceViewModel.Resource is Interface)
                this.interfaceList.Remove(resourceViewModel.Resource as Interface);
            else if (resourceViewModel.Resource is IpAddress)
                this.ipAddresseList.Remove(resourceViewModel.Resource as IpAddress);
            else if (resourceViewModel.Resource is VirtualMachine)
                this.virtualMachineList.Remove(resourceViewModel.Resource as VirtualMachine);

            this.ResourceViewModeCollection.Remove(resourceViewModel);
        }

        private void ApplySettings(ResourceViewModel resourceViewModel)
        {
            ResourceSettingInfo resourceInfo = this.resourceInfoList.SingleOrDefault(r => r.Name == resourceViewModel.Name);
            if (resourceInfo != null)
            {
                resourceViewModel.Left = resourceInfo.Location.X;
                resourceViewModel.Top = resourceInfo.Location.Y;
                resourceViewModel.ZIndex = resourceInfo.ZIndex;
            }
        }

        private int GetMaxResourceZIndex()
        {
            return this.ResourceViewModeCollection.Max(r => r.ZIndex);
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