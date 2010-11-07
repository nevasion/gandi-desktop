using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class IpAddressResourceDetail : IResourceDetail
    {
        private const string CopyCommandName = "Copy";
        private const string DetachCommandName = "Detach";
        private const string EditCommandName = "Edit";

        private IpAddress ipAddress;

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.IpAddress; }
        }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailQuickActionHandler DetailQuickAction;

        public IpAddressResourceDetail(IpAddress ipAddress)
        {
            this.ipAddress = ipAddress;

            this.Value = ipAddress.Ip.ToString();

            ResourceDetailAction copyAction = new ResourceDetailAction() { Name = IpAddressResourceDetail.CopyCommandName };
            copyAction.Command = new RelayCommand((parameter) => this.Copy(parameter));

            ResourceDetailAction detachAction = new ResourceDetailAction() { Name = IpAddressResourceDetail.DetachCommandName };
            detachAction.Command = new RelayCommand((parameter) => this.Detach(parameter));

            ResourceDetailAction editAction = new ResourceDetailAction() { Name = IpAddressResourceDetail.EditCommandName };
            editAction.Command = new RelayCommand((parameter) => this.Edit(parameter));

            this.Actions = new ResourceDetailAction[] 
            {
                copyAction,
                //detachAction,
                editAction
            };
        }

        public void Copy(object parameter)
        {
            if (this.DetailQuickAction != null)
                this.DetailQuickAction(this, new ResourceDetailQuickActionEventArgs(ResourceDetailQuickAction.Copy, this.ipAddress.Ip.ToString()));
        }

        public void Detach(object parameter)
        {

        }

        public void Edit(object parameter)
        {

        }
    }
}