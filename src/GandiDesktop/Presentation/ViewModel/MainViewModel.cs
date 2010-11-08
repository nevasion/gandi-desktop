namespace GandiDesktop.Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public DesktopViewModel DesktopViewModel { get; private set; }

        public MainViewModel()
        {
            this.DesktopViewModel = new DesktopViewModel();
        }

        public void SaveResourceLocations()
        {
            this.DesktopViewModel.SaveResourceLocations();
        }
    }
}