using System.Windows;

namespace GandiDesktop.Presentation.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            this.InitializeComponent();
            this.LoadSettings();

            this.Closing += delegate
            {
                Settings.WindowLocation = new Point(this.Left, this.Top);
                Settings.WindowSize = new Size(this.Width, this.Height);

                Settings.Save();
            };
        }

        private void LoadSettings()
        {
            if (Settings.WindowLocation.HasValue)
            {
                this.Left = Settings.WindowLocation.Value.X;
                this.Top = Settings.WindowLocation.Value.Y;
            }

            if (Settings.WindowSize.HasValue)
            {
                this.Width = Settings.WindowSize.Value.Width;
                this.Height = Settings.WindowSize.Value.Height;
            }
        }
    }
}