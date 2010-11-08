using System.Windows;
using GandiDesktop.Presentation.View;
using GandiDesktop.Presentation.ViewModel;

namespace GandiDesktop
{
    public partial class App : Application
    {
        private MainViewModel mainViewModel = new MainViewModel();

        public App()
        {
            this.mainViewModel = new MainViewModel();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            this.mainViewModel.SaveResourceLocations();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainView mainView = new MainView()
            {
                DataContext = this.mainViewModel
            };

            mainView.Show();
        }
    }
}