using System;
using System.Collections.Generic;
using System.Linq;
using GandiDesktop.Gandi.Services.Hosting;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
{
    public class OperationViewModel : ViewModelBase
    {
        private const string OperationActionCreate = "Creating";
        private const string OperationActionUpdate = "Updating";
        private const string OperationActionDelete = "Deleting";
        private const string OperationActionAttach = "Attaching";
        private const string OperationActionDetach = "Detaching";
        private const string OperationActionStart = "Starting";
        private const string OperationActionStop = "Stopping";
        private const string OperationActionReboot = "Rebooting";
        private const string OperationActionDirectionFrom = "from";
        private const string OperationActionDirectionTo = "to";
        private const string OperationResourceInterfaceTemplate = "#{0} ({1} Kbps)";
        
        private Operation operation;
        public Operation Operation
        {
            get { return this.operation; }
        }

        public DateTime Started { get; private set; }
        public OperationType Type { get; private set; }
        public OperationStep Step { get; private set; }

        public string Action { get; private set; }
        public string ActionDirection { get; private set; }
        public string ResourceName1 { get; private set; }
        public string ResourceName2 { get; private set; }
        public OperationType ResourceType1 { get; private set; }
        public OperationType ResourceType2 { get; private set; }

        private DateTime completed;
        public DateTime Completed
        {
            get { return this.completed; }
            set
            {
                if (this.completed != value)
                {
                    this.completed = value;
                    base.OnPropertyChanged(() => Completed);
                }
            }
        }

        public event EventHandler Refreshed;

        public OperationViewModel(Operation operation)
        {
            this.operation = operation;

            this.Started = this.operation.Created;
            this.Type = this.operation.Type;
            this.Step = this.operation.Step;

            this.SetAction();
        }

        private void SetAction()
        {
            if ((this.Type & OperationType.Create) == OperationType.Create)
            {
                this.Action = OperationViewModel.OperationActionCreate;
            }
            else if ((this.Type & OperationType.Update) == OperationType.Update)
            {
                this.Action = OperationViewModel.OperationActionUpdate;
            }
            else if ((this.Type & OperationType.Delete) == OperationType.Delete)
            {
                this.Action = OperationViewModel.OperationActionDelete;
            }
            else if ((this.Type & OperationType.Attach) == OperationType.Attach)
            {
                this.Action = OperationViewModel.OperationActionAttach;
                this.ActionDirection = OperationViewModel.OperationActionDirectionTo;
            }
            else if ((this.Type & OperationType.Detach) == OperationType.Detach)
            {
                this.Action = OperationViewModel.OperationActionDetach;
                this.ActionDirection = OperationViewModel.OperationActionDirectionFrom;
            }
            else if ((this.Type & OperationType.Start) == OperationType.Start)
            {
                this.Action = OperationViewModel.OperationActionStart;
            }
            else if ((this.Type & OperationType.Stop) == OperationType.Stop)
            {
                this.Action = OperationViewModel.OperationActionStop;
            }
            else if ((this.Type & OperationType.Reboot) == OperationType.Reboot)
            {
                this.Action = OperationViewModel.OperationActionReboot;
            }

            this.ResourceType2 = OperationType.None;

            if ((this.Type & OperationType.Disk) == OperationType.Disk)
            {
                DiskOperation diskOperation = this.operation as DiskOperation;
                VirtualMachineOperation virtualMachineOperation = this.operation as VirtualMachineOperation;

                this.ResourceType1 = OperationType.Disk;

                if (diskOperation != null)
                {
                    this.ResourceName1 = diskOperation.Disk.Name;
                }
                else if (virtualMachineOperation != null)
                {
                    this.ResourceName1 = virtualMachineOperation.Disk.Name;

                    this.ResourceName2 = virtualMachineOperation.VirtualMachine.Hostname;
                    this.ResourceType2 = OperationType.VirtualMachine;
                }
            }
            else if ((this.Type & OperationType.Interface) == OperationType.Interface)
            {
                InterfaceOperation interfaceOperation = this.operation as InterfaceOperation;
                VirtualMachineOperation virtualMachineOperation = this.operation as VirtualMachineOperation;

                this.ResourceType1 = OperationType.Interface;

                if (interfaceOperation != null)
                {
                    this.ResourceName1 = String.Format(OperationViewModel.OperationResourceInterfaceTemplate, interfaceOperation.Interface.Index, interfaceOperation.Interface.Bandwidth);
                }
                else if (virtualMachineOperation != null)
                {
                    this.ResourceName1 = String.Format(OperationViewModel.OperationResourceInterfaceTemplate, virtualMachineOperation.Interface.Index, virtualMachineOperation.Interface.Bandwidth); ;

                    this.ResourceName2 = virtualMachineOperation.VirtualMachine.Hostname;
                    this.ResourceType2 = OperationType.VirtualMachine;
                }
            }
            else if ((this.Type & OperationType.IpAddress) == OperationType.IpAddress)
            {
                IpAddressOperation ipAddressOperation = this.operation as IpAddressOperation;
                InterfaceOperation interfaceOperation = this.operation as InterfaceOperation;

                this.ResourceType1 = OperationType.IpAddress;

                if (ipAddressOperation != null)
                {
                    this.ResourceName1 = ipAddressOperation.IpAddress.Ip.ToString();
                }
                else if (interfaceOperation != null)
                {
                    this.ResourceName1 = interfaceOperation.IpAddress.Ip.ToString();

                    this.ResourceName2 = String.Format(OperationViewModel.OperationResourceInterfaceTemplate, interfaceOperation.Interface.Index, interfaceOperation.Interface.Bandwidth);
                    this.ResourceType2 = OperationType.Interface;
                }
            }
            else if ((this.Type & OperationType.VirtualMachine) == OperationType.VirtualMachine)
            {
                VirtualMachineOperation virtualMachineOperation = this.operation as VirtualMachineOperation;

                this.ResourceName1 = virtualMachineOperation.VirtualMachine.Hostname;
                this.ResourceType1 = OperationType.VirtualMachine;
            }
        }

        public OperationStep RefreshStatus()
        {
            Operation refreshedOperation = Service.Hosting.Operation.Update(this.operation);

            this.operation.Step = refreshedOperation.Step;
            this.operation.LastUpdated = refreshedOperation.LastUpdated;

            this.Step = this.operation.Step;

            if (this.Step == OperationStep.Done)
                this.Completed = this.operation.LastUpdated;

            if (this.Refreshed != null)
                this.Refreshed(this, EventArgs.Empty);

            return this.operation.Step;
        }
    }
}