using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using GandiDesktop.Presentation.ViewModel;

namespace GandiDesktop.Presentation.View
{
    public partial class ResourceDetailView : UserControl
    {
        private const string IsExpandedPropertyName = "IsExpanded";

        private Grid gridQuickActions;
        private AnimationClock quickActionsClock;
        private bool animating;

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

            this.MouseEnter += delegate
            {
                if (this.animating)
                {
                    this.quickActionsClock.Controller.Begin();
                    this.quickActionsClock.Controller.Pause();
                }
            };

            this.MouseLeave += delegate
            {
                ResourceDetailViewModel viewModel = this.DataContext as ResourceDetailViewModel;
                if (viewModel != null)
                {
                    if (this.IsExpanded)
                        this.ToggleQuickActionsDisplay(true);

                    if (viewModel.HasQuickActions)
                        foreach (ResourceDetailActionViewModel quickAction in viewModel.Actions)
                            quickAction.AskConfirmation = false;
                }
            };
        }

        private void OnGridQuickActionsLoaded(object sender, RoutedEventArgs e)
        {
            this.gridQuickActions = (Grid)sender;
        }

        private void OnButtonQuickActionsClick(object sender, RoutedEventArgs e)
        {
            this.ToggleQuickActionsDisplay(false);
        }

        private void ToggleQuickActionsDisplay(bool delayed)
        {
            if (this.gridQuickActions != null)
            {
                double from = this.ActualHeight;
                double to = 14;

                if (!this.IsExpanded)
                    to += this.gridQuickActions.ActualHeight;

                DoubleAnimation quickActionsAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(100)));
                quickActionsAnimation.Completed += delegate
                {
                    this.animating = false;
                    this.IsExpanded = !this.IsExpanded;
                };

                if (delayed)
                    quickActionsAnimation.BeginTime = TimeSpan.FromMilliseconds(1500);

                this.animating = true; 
                this.quickActionsClock = quickActionsAnimation.CreateClock();
                this.ApplyAnimationClock(ResourceDetailView.HeightProperty, this.quickActionsClock);
            }
        }
    }
}