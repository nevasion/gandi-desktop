using System.Windows.Input;

namespace GandiDesktop.Presentation.Model
{
    public class ResourceDetailAction
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
        public bool ConfirmationRequired { get; set; }

        public ResourceDetailAction(string name, bool confirmationRequired)
        {
            this.Name = name;
            this.ConfirmationRequired = confirmationRequired;
        }

        public ResourceDetailAction(string name)
            : this(name, false)
        {
        }
    }
}