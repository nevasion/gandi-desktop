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

        public DesktopViewModel()
        {
            this.ResourceViewModeCollection = new ObservableCollection<ResourceViewModel>();
            this.OperationsViewModel = new OperationsViewModel();
            this.resourceInfoList = new List<ResourceSettingInfo>(Settings.ResourceInfos);

            Service.Hosting.FetchAll(false);

            IEnumerable<VirtualMachine> virtualMachineList = Service.Hosting.VirtualMachines;
            for (int i = 0; i < virtualMachineList.Count(); i++)
            {
                VirtualMachine virtualMachine = virtualMachineList.ElementAt(i);

                ResourceViewModel resourceViewModel = new ResourceViewModel(new VirtualMachineResource(virtualMachine))
                {
                    Left = (i * 58) + 8,
                    Top = 10
                };

                resourceViewModel.DetailAction += OnDetailAction;

                this.ApplySettings(resourceViewModel);
                this.ResourceViewModeCollection.Add(resourceViewModel);
            }

            IEnumerable<Disk> detachedDisks = Service.Hosting.Disks.Where(d => d.VirtualMachineIds.Length == 0);
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

            IEnumerable<Interface> detachedInterfaces = Service.Hosting.Interfaces.Where(i => i.VirtualMachineId == null);
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
                dynamic operation = operationViewModel.Operation;

                if ((operationViewModel.Type & OperationType.Create) == OperationType.Create)
                {
                    if (operation is DiskOperation)
                    {
                        Disk disk = Service.Hosting.Fetch<Disk>(operation.DiskId);

                        this.AddResource(new DiskResource(disk), 20, 20);
                    }
                    else if (operation is InterfaceOperation)
                    {
                        Interface iface = Service.Hosting.Fetch<Interface>(operation.InterfaceId);

                        this.AddResource(new InterfaceResource(iface), 20, 20);
                    }
                    else if (operation is IpAddressOperation)
                    {
                    }
                    else if (operation is VirtualMachineOperation)
                    {
                        VirtualMachine virtualMachine = Service.Hosting.Fetch<VirtualMachine>(operation.VirtualMachineId.Value);

                        this.AddResource(new VirtualMachineResource(virtualMachine), 20, 20);
                    }
                }
                else if ((operationViewModel.Type & OperationType.Detach) == OperationType.Detach)
                {
                    if ((operationViewModel.Type & OperationType.Disk) == OperationType.Disk)
                    {
                        this.AddResource(new DiskResource(operation.Disk), 20, 20);
                    }
                    else if ((operationViewModel.Type & OperationType.Interface) == OperationType.Interface)
                    {
                        this.AddResource(new DiskResource(operation.Interface), 20, 20);
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
                Disk disk = Service.Hosting.Fetch<Disk>(resourceViewModel.Id);
                resourceViewModel.Update(new DiskResource(disk));
            }
            else if (resourceViewModel.Type == ResourceType.Interface)
            {
                Interface iface = Service.Hosting.Fetch<Interface>(resourceViewModel.Id);
                resourceViewModel.Update(new InterfaceResource(iface));
            }
            else if (resourceViewModel.Type == ResourceType.VirtualMachine)
            {
                VirtualMachine virtualMachine = Service.Hosting.Fetch<VirtualMachine>(resourceViewModel.Id);
                resourceViewModel.Update(new VirtualMachineResource(virtualMachine));
            }
        }

        private void RemoveResource(ResourceViewModel resourceViewModel)
        {
            Service.Hosting.Remove(resourceViewModel.Resource);

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