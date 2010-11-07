using System.Windows.Input;

namespace GandiDesktop.Presentation.Model
{
    public class ResourceDetailAction
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
}