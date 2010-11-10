using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class DiskResourceDetail : IResourceDetail
    {
        private const string ValueTemplate = "{0} ({1} Go)";
        private const string DetachCommandName = "Detach";
        private const string EditCommandName = "Edit";

        private Disk disk;
        private VirtualMachine virtualMachine;

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type { get; private set; }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public DiskResourceDetail(Disk disk, VirtualMachine virtualMachine)
        {
            this.disk = disk;
            this.virtualMachine = virtualMachine;

            this.Value = String.Format(DiskResourceDetail.ValueTemplate, disk.Name, (disk.Size / 1000));

            if (disk.IsBootDisk) this.Type = ResourceDetailType.SystemDisk;
            else this.Type = ResourceDetailType.Disk;

            ResourceDetailAction detachAction = new ResourceDetailAction(DiskResourceDetail.DetachCommandName, true);
            detachAction.Command = new RelayCommand((parameter) => this.Detach(parameter));

            ResourceDetailAction editAction = new ResourceDetailAction(DiskResourceDetail.EditCommandName);
            editAction.Command = new RelayCommand((parameter) => this.Edit(parameter));

            this.Actions = new ResourceDetailAction[] 
            {
                detachAction,
                editAction
            };
        }

        public void Detach(object parameter)
        {
            ResourceDetailActionEventArgs actionEventArgs = new ResourceDetailActionEventArgs(ResourceDetailActionType.Detach);

            try { Service.Hosting.VirtualMachine.DetachDisk(this.virtualMachine, this.disk); }
            catch (Exception x)
            {
                actionEventArgs.Resource = this;
                actionEventArgs.Error = true;
                actionEventArgs.ErrorMessage = x.Message;
            }

            if (this.DetailAction != null)
                this.DetailAction(this, actionEventArgs);
        }

        public void Edit(object parameter)
        {

        }
    }
}