using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace GandiDesktop.Presentation.View
{
    public partial class ResourceDetailView : UserControl
    {
        private const string IsExpandedPropertyName = "IsExpanded";

        private Grid gridQuickActions;

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(ResourceDetailView.IsExpandedPropertyName, typeof(bool), typeof(ResourceDetailView), new UIPropertyMetadata(false));

        public ResourceDetailView()
        {
            InitializeComponent();
        }

        private void OnButtonQuickActionsClick(object sender, RoutedEventArgs e)
        {
            if (gridQuickActions != null)
            {
                double from = this.ActualHeight;
                double to;

                if (this.IsExpanded)
                {
                    to = 14;
                }
                else
                {
                    to = 14 + gridQuickActions.ActualHeight;
                }

                this.IsExpanded = !this.IsExpanded;

                DoubleAnimation animation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(100)));

                this.BeginAnimation(ResourceDetailView.HeightProperty, animation);
            }
        }

        private void OnGridQuickActionsLoaded(object sender, RoutedEventArgs e)
        {
            gridQuickActions = (Grid)sender;
        }
    }
}