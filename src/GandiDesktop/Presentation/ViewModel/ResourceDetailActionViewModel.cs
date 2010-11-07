using System.Windows.Input;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
{
    public class ResourceDetailActionViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }

        public ResourceDetailActionViewModel(ResourceDetailAction resourceDetailAction)
        {
            this.Name = resourceDetailAction.Name;
            this.Command = resourceDetailAction.Command;
        }
    }
}