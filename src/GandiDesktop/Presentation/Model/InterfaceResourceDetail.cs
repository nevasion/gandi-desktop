using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class InterfaceResourceDetail : IResourceDetail
    {
        private const string PluralSuffix = "s";
        private const string ValueTemplate = "{0} Kbps ({1} IP{2})";
        private const string DetachCommandName = "Detach";
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

        public event ResourceDetailQuickActionHandler DetailQuickAction;

        public InterfaceResourceDetail(Interface iface, VirtualMachine virtualMachine)
        {
            this.iface = iface;
            this.virtualMachine = virtualMachine;

            this.Value = String.Format(
                InterfaceResourceDetail.ValueTemplate, 
                iface.Bandwidth, 
                iface.IpAddressIds.Length, 
                (iface.IpAddressIds.Length <= 1 ? null : InterfaceResourceDetail.PluralSuffix));

            ResourceDetailAction detachAction = new ResourceDetailAction() { Name = InterfaceResourceDetail.DetachCommandName };
            detachAction.Command = new RelayCommand((parameter) => this.Detach(parameter));

            ResourceDetailAction editAction = new ResourceDetailAction() { Name = InterfaceResourceDetail.EditCommandName };
            editAction.Command = new RelayCommand((parameter) => this.Edit(parameter));

            this.Actions = new ResourceDetailAction[] 
            {
                detachAction,
                editAction
            };
        }

        public void Detach(object parameter)
        {
            if (this.DetailQuickAction != null)
                this.DetailQuickAction(this, new ResourceDetailQuickActionEventArgs(ResourceDetailQuickAction.Detach));

            Service.Hosting.VirtualMachine.DetachInterface(virtualMachine, iface);
        }

        public void Edit(object parameter)
        {

        }
    }
}