using System.Windows;
using GandiDesktop.View;
using GandiDesktop.ViewModel;

namespace GandiDesktop
{
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainView mainView = new MainView()
            {
                DataContext = new MainViewModel()
            };

            mainView.Show();
        }
    }
}