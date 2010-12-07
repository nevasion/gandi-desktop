using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class InterfaceResourceDetail : IResourceDetail
    {
        private const string PluralSuffix = "s";
        private const string ValueTemplate = "{0} Kbps ({1} IP{2})";
        private const string DetachCommandName = "Detach";
        private const string SeeIpAddressesCommandName = "See IP{0}";
        private const string EditCommandName = "Edit";

        private Interface iface;
        private VirtualMachine virtualMachine;

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Interface; }
        }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public InterfaceResourceDetail(Interface iface, VirtualMachine virtualMachine)
        {
            this.iface = iface;
            this.virtualMachine = virtualMachine;

            this.Value = String.Format(
                InterfaceResourceDetail.ValueTemplate, 
                iface.Bandwidth, 
                iface.IpAddressIds.Length, 
                (iface.IpAddressIds.Length <= 1 ? null : InterfaceResourceDetail.PluralSuffix));

            ResourceDetailAction detachAction = new ResourceDetailAction(InterfaceResourceDetail.DetachCommandName, true);
            detachAction.Command = new RelayCommand((parameter) => this.Detach(parameter));

            ResourceDetailAction seeIpAddressesAction = new ResourceDetailAction(String.Format(InterfaceResourceDetail.SeeIpAddressesCommandName, (iface.IpAddressIds.Length <= 1 ? null : InterfaceResourceDetail.PluralSuffix)));
            seeIpAddressesAction.Command = new RelayCommand((parameter) => this.SeeIpAddresses(parameter));

            ResourceDetailAction editAction = new ResourceDetailAction(InterfaceResourceDetail.EditCommandName);
            editAction.Command = new RelayCommand((parameter) => this.Edit(parameter));

            this.Actions = new ResourceDetailAction[] 
            {
                detachAction,
                seeIpAddressesAction,
                editAction
            };
        }

        public void SeeIpAddresses(object parameter)
        {
            if (this.DetailAction != null)
                this.DetailAction(this, new ResourceDetailActionEventArgs(ResourceDetailActionType.SeeIpAddresses) { Resource = this.iface });
        }

        public void Detach(object parameter)
        {
            ResourceDetailActionEventArgs actionEventArgs = new ResourceDetailActionEventArgs(ResourceDetailActionType.Detach);
            Operation operation = null;

            try { operation = Service.Hosting.VirtualMachine.DetachInterface(this.virtualMachine, this.iface); }
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

        public void Edit(object parameter)
        {

        }
    }
}