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

        private Grid gridActions;
        private AnimationClock toggleActionsClock;
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
                    this.toggleActionsClock.Controller.Begin();
                    this.toggleActionsClock.Controller.Pause();
                }
            };

            this.MouseLeave += delegate
            {
                ResourceDetailViewModel viewModel = this.DataContext as ResourceDetailViewModel;
                if (viewModel != null)
                {
                    if (this.IsExpanded)
                        this.ToggleActionsDisplay(true);

                    if (viewModel.HasActions)
                        foreach (ResourceDetailActionViewModel action in viewModel.Actions)
                            action.AskConfirmation = false;
                }
            };
        }

        private void OnGridActionsLoaded(object sender, RoutedEventArgs e)
        {
            this.gridActions = (Grid)sender;
        }

        private void OnButtonToggleActionsClick(object sender, RoutedEventArgs e)
        {
            this.ToggleActionsDisplay(false);
        }

        private void ToggleActionsDisplay(bool delayed)
        {
            if (this.gridActions != null)
            {
                double from = this.ActualHeight;
                double to = 14;

                if (!this.IsExpanded)
                    to += this.gridActions.ActualHeight;

                DoubleAnimation actionsAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromMilliseconds(100)));
                actionsAnimation.Completed += delegate
                {
                    this.animating = false;
                    this.IsExpanded = !this.IsExpanded;
                };

                if (delayed)
                    actionsAnimation.BeginTime = TimeSpan.FromMilliseconds(1500);

                this.animating = true; 
                this.toggleActionsClock = actionsAnimation.CreateClock();
                this.ApplyAnimationClock(ResourceDetailView.HeightProperty, this.toggleActionsClock);
            }
        }
    }
}