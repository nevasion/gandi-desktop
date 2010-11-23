using System.Windows.Input;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
{
    public class ResourceDetailActionViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
        public bool ConfirmationRequired { get; set; }

        private Cursor cursor;
        public Cursor Cursor
        {
            get { return this.cursor; }
            set
            {
                if (this.cursor != value)
                {
                    this.cursor = value;
                    base.OnPropertyChanged(() => Cursor);
                }
            }
        }

        private bool askConfirmation;
        public bool AskConfirmation
        {
            get { return this.askConfirmation; }
            set
            {
                if (this.askConfirmation != value)
                {
                    this.askConfirmation = value;
                    base.OnPropertyChanged(() => AskConfirmation);
                }
            }
        }

        private ICommand askCommand;
        public ICommand AskCommand
        {
            get
            {
                if (this.askCommand == null)
                    this.askCommand = new RelayCommand(
                        (parameter) => this.Ask(parameter),
                        (parameter) => this.CanAsk(parameter));

                return this.askCommand;
            }
        }

        private ICommand confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (this.confirmCommand == null)
                    this.confirmCommand = new RelayCommand((parameter) => this.Confirm(parameter));

                return this.confirmCommand;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                    this.cancelCommand = new RelayCommand((parameter) => this.Cancel(parameter));

                return this.cancelCommand;
            }
        }

        public ResourceDetailActionViewModel(ResourceDetailAction resourceDetailAction)
        {
            this.Name = resourceDetailAction.Name;
            this.Command = resourceDetailAction.Command;
            this.ConfirmationRequired = resourceDetailAction.ConfirmationRequired;
        }

        public bool CanAsk(object parameter)
        {
            return this.Command.CanExecute(parameter);
        }

        public void Ask(object parameter)
        {
            this.AskConfirmation = true;
        }

        public void Confirm(object parameter)
        {
            this.Cursor = Cursors.Wait;

            if (this.Command != null && this.Command.CanExecute(null))
                this.Command.Execute(null);

            this.AskConfirmation = false;
            this.Cursor = Cursors.Arrow;
        }

        public void Cancel(object parameter)
        {
            this.AskConfirmation = false;
        }
    }
}