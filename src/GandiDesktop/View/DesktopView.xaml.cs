using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GandiDesktop.ViewModel;

namespace GandiDesktop.View
{
    public partial class DesktopView : UserControl
    {
        private bool isDraggingResource = false;
        private bool resourceHasBeenDragged = false;
        private Point resourceClickMousePosition = new Point();
        private ResourceView resourceClicked;

        public DesktopView()
        {
            InitializeComponent();
        }

        private void OnResourceViewMouseEnter(object sender, MouseEventArgs e)
        {
            ResourceView view = (ResourceView)sender;
            ResourceViewModel viewModel = (ResourceViewModel)view.DataContext;

            viewModel.IsMouseOver = true;
        }

        private void OnResourceViewMouseLeave(object sender, MouseEventArgs e)
        {
            ResourceView view = (ResourceView)sender;
            ResourceViewModel viewModel = (ResourceViewModel)view.DataContext;

            viewModel.IsMouseOver = false;
        }

        private void OnResourceViewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResourceView view = (ResourceView)sender;
            ResourceViewModel viewModel = (ResourceViewModel)view.DataContext;

            viewModel.ZIndex = (this.GetMaxResourceZIndex() + 1);

            this.isDraggingResource = true;
            this.resourceClickMousePosition = e.GetPosition(view);
            this.resourceClicked = view;
        }

        private void OnResourceViewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && isDraggingResource)
            {
                Point resourceMousePosition = e.GetPosition(resourceClicked);

                ResourceViewModel viewModel = (ResourceViewModel)resourceClicked.DataContext;
                Point viewPosition = new Point(viewModel.Left, viewModel.Top);

                double left = (viewPosition.X + resourceMousePosition.X - resourceClickMousePosition.X);
                double top = (viewPosition.Y + resourceMousePosition.Y - resourceClickMousePosition.Y);

                viewModel.Left = left;
                viewModel.Top = top;

                this.resourceHasBeenDragged = true;
            }
        }

        private void OnResourceViewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ResourceView view = (ResourceView)sender;

            if (!this.resourceHasBeenDragged && view == resourceClicked)
            {
                view.IsExpanded = !view.IsExpanded;
            }
            else
            {
                this.resourceHasBeenDragged = false;
            }

            this.isDraggingResource = false;
            this.resourceClicked = null;
        }

        private int GetMaxResourceZIndex()
        {
            DesktopViewModel viewModel = (DesktopViewModel)this.DataContext;

            int maxZIndex = viewModel.ResourceViewModeCollection.Max(r => r.ZIndex);

            return maxZIndex;
        }
    }
}