using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class StatusResourceDetail : IResourceDetail
    {
        private const string StartCommandName = "Start";
        private const string StopCommandName = "Stop";
        private const string RebootCommandName = "Reboot";

        private VirtualMachine virtualMachine;

        public string Name { get; private set; }

        public string Value { get; private set; }

        public string Status { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Status; }
        }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public StatusResourceDetail(VirtualMachine virtualMachine)
        {
            this.virtualMachine = virtualMachine;

            string status = virtualMachine.Status.ToString();

            status = status.Replace('_', ' ');
            status = status.Substring(0, 1).ToUpperInvariant() + status.Substring(1);

            this.Value = status;

            ResourceDetailAction startAction = new ResourceDetailAction(StatusResourceDetail.StartCommandName, true);
            startAction.Command = new RelayCommand((parameter) => this.Start(parameter), (parameter) => this.CanStart(parameter));

            ResourceDetailAction stopAction = new ResourceDetailAction(StatusResourceDetail.StopCommandName, true);
            stopAction.Command = new RelayCommand((parameter) => this.Stop(parameter), (parameter) => this.CanStop(parameter));

            ResourceDetailAction rebootAction = new ResourceDetailAction(StatusResourceDetail.RebootCommandName, true);
            rebootAction.Command = new RelayCommand((parameter) => this.Reboot(parameter), (parameter) => this.CanReboot(parameter));

            this.Actions = new ResourceDetailAction[] 
            {
                startAction,
                stopAction,
                rebootAction
            };
        }

        public bool CanStart(object parameter)
        {
            return (this.virtualMachine.Status != VirtualMachineStatus.Running);
        }

        public void Start(object parameter)
        {
            ResourceDetailActionEventArgs actionEventArgs = new ResourceDetailActionEventArgs(ResourceDetailActionType.Start);
            Operation operation = null;

            try { operation = Service.Hosting.VirtualMachine.Start(this.virtualMachine); }
            catch (Exception x)
            {
                actionEventArgs.Resource = this;
                actionEventArgs.Error = true;
                actionEventArgs.ErrorMessage = x.Message;
            }

            if (this.DetailAction != null)
            {
                actionEventArgs.Operation = operation;

                this.DetailAction(this, actionEventArgs);
            }
        }

        public bool CanStop(object parameter)
        {
            return (this.virtualMachine.Status == VirtualMachineStatus.Running);
        }

        public void Stop(object parameter)
        {
            ResourceDetailActionEventArgs actionEventArgs = new ResourceDetailActionEventArgs(ResourceDetailActionType.Stop);
            Operation operation = null;

            try { operation = Service.Hosting.VirtualMachine.Stop(this.virtualMachine); }
            catch (Exception x)
            {
                actionEventArgs.Resource = this;
                actionEventArgs.Error = true;
                actionEventArgs.ErrorMessage = x.Message;
            }

            if (this.DetailAction != null)
            {
                actionEventArgs.Operation = operation;

                this.DetailAction(this, actionEventArgs);
            }
        }

        public bool CanReboot(object parameter)
        {
            return (this.virtualMachine.Status == VirtualMachineStatus.Running);
        }

        public void Reboot(object parameter)
        {
            ResourceDetailActionEventArgs actionEventArgs = new ResourceDetailActionEventArgs(ResourceDetailActionType.Reboot);
            Operation operation = null;

            try { operation = Service.Hosting.VirtualMachine.Reboot(this.virtualMachine); }
            catch (Exception x)
            {
                actionEventArgs.Resource = this;
                actionEventArgs.Error = true;
                actionEventArgs.ErrorMessage = x.Message;
            }

            if (this.DetailAction != null)
            {
                actionEventArgs.Operation = operation;

                this.DetailAction(this, actionEventArgs);
            }
        }
    }
}