using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GandiDesktop.Presentation.ViewModel;

namespace GandiDesktop.Presentation.View
{
    public partial class DesktopView : UserControl
    {
        private bool isDraggingResource = false;
        private Point resourceClickMousePosition = new Point();
        private ResourceView resourceDragged;

        public DesktopView()
        {
            InitializeComponent();

            this.SizeChanged += delegate { this.MoveResourcesInBounds(); };
            this.MouseLeftButtonUp += delegate { this.MoveResourcesInBounds(); };
            this.MouseLeave += delegate 
            { 
                this.MoveResourcesInBounds();

                this.isDraggingResource = false;
                if (this.resourceDragged != null)
                    this.resourceDragged.IsDragging = false;
            };
        }

        private void OnResourceViewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResourceView view = (ResourceView)sender;
            ResourceViewModel viewModel = (ResourceViewModel)view.DataContext;

            viewModel.ZIndex = (this.GetMaxResourceZIndex() + 1);

            this.isDraggingResource = true;
            this.resourceClickMousePosition = e.GetPosition(view);
            this.resourceDragged = view;
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.isDraggingResource)
            {
                Point resourceMousePosition = e.GetPosition(this.resourceDragged);

                ResourceViewModel viewModel = (ResourceViewModel)this.resourceDragged.DataContext;
                Point viewPosition = new Point(viewModel.Left, viewModel.Top);

                double left = (viewPosition.X + resourceMousePosition.X - this.resourceClickMousePosition.X);
                double top = (viewPosition.Y + resourceMousePosition.Y - this.resourceClickMousePosition.Y);

                viewModel.Left = left;
                viewModel.Top = top;

                this.resourceDragged.IsDragging = true;
            }
        }

        private void OnResourceViewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ResourceView view = (ResourceView)sender;

            if (this.resourceDragged != null)
                this.resourceDragged.IsDragging = false;

            this.isDraggingResource = false;
            this.resourceDragged = null;
        }

        private void MoveResourcesInBounds()
        {
            DesktopViewModel viewModel = (DesktopViewModel)this.DataContext;

            if (viewModel != null)
            {
                foreach (ResourceViewModel resourceViewModel in viewModel.ResourceViewModeCollection)
                {
                    this.MoveResourceInBounds(resourceViewModel);
                }
            }
        }

        private void MoveResourceInBounds(ResourceViewModel resourceViewModel)
        {
            Storyboard storyboard = new Storyboard();

            var view = (FrameworkElement)(desktopView.ItemContainerGenerator.ContainerFromItem(resourceViewModel));

            const double offset = 4;

            double left = resourceViewModel.Left;
            double right = left + view.ActualWidth;
            double top = resourceViewModel.Top;
            double bottom = top + view.ActualHeight;

            bool leftOutBound = left < 0;
            bool rightOutBound = right > this.ActualWidth;
            bool topOutBound = top < 0;
            bool bottomOutBound = bottom > this.ActualHeight;

            if (leftOutBound || rightOutBound)
            {
                double from = left;
                double to = (leftOutBound ? offset : (this.ActualWidth - view.ActualWidth - offset));

                DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames()
                {
                    Duration = TimeSpan.FromMilliseconds(125)
                };

                animation.KeyFrames.Add(new SplineDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(to + ((leftOutBound ? offset : offset * -1) * 2), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(80))));
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(125))));

                AnimationClock clock = animation.CreateClock();
                clock.CurrentTimeInvalidated += (sender, e) =>
                {
                    double current = animation.GetCurrentValue(from, to, clock);
                    resourceViewModel.Left = current;
                };
                
                storyboard.Children.Add(animation);

                Storyboard.SetTargetProperty(animation, new PropertyPath(ResourceView.DummyLeftProperty));
            }

            if (topOutBound || bottomOutBound)
            {
                double from = top;
                double to = (topOutBound ? offset : (this.ActualHeight - view.ActualHeight - offset));

                DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames()
                {
                    Duration = TimeSpan.FromMilliseconds(125)
                };

                animation.KeyFrames.Add(new SplineDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(to + ((topOutBound ? offset : offset * -1) * 2), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(80))));
                animation.KeyFrames.Add(new SplineDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(125))));

                AnimationClock clock = animation.CreateClock();
                clock.CurrentTimeInvalidated += (sender, e) =>
                {
                    double current = animation.GetCurrentValue(from, to, clock);
                    resourceViewModel.Top = current;
                };

                storyboard.Children.Add(animation);

                Storyboard.SetTargetProperty(animation, new PropertyPath(ResourceView.DummyTopProperty));
            }

            this.BeginStoryboard(storyboard);
        }

        private int GetMaxResourceZIndex()
        {
            DesktopViewModel viewModel = (DesktopViewModel)this.DataContext;

            int maxZIndex = viewModel.ResourceViewModeCollection.Max(r => r.ZIndex);

            return maxZIndex;
        }
    }
}