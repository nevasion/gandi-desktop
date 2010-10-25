using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GandiDesktop.ViewModel;
using System.Windows.Media.Animation;

namespace GandiDesktop.View
{
    public partial class ResourceView : UserControl
    {
        private const string IsExpandedPropertyName = "IsExpanded";
        private const string StoryboardCollapseKey = "Collapse";
        private const string StoryboardExpandKey = "Expand";

        public Storyboard CollapseStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardCollapseKey] as Storyboard); }
        }

        public Storyboard ExpandStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardExpandKey] as Storyboard); }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            ResourceView.IsExpandedPropertyName, 
            typeof(bool), 
            typeof(ResourceView), 
            new UIPropertyMetadata(false, new PropertyChangedCallback(ResourceView.OnIsExpandedPropertyChanged)));

        public ResourceView()
        {
            InitializeComponent();
        }

        private static void OnIsExpandedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ResourceView view = (ResourceView)sender;

            if ((bool)e.NewValue)
            {
                view.ExpandStoryboard.Begin();
            }
            else
            {
                view.CollapseStoryboard.Begin();
            }
        }
    }
}