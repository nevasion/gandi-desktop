namespace GandiDesktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public DesktopViewModel DesktopViewModel { get; private set; }

        public MainViewModel()
        {
            this.DesktopViewModel = new DesktopViewModel();
        }
    }
}