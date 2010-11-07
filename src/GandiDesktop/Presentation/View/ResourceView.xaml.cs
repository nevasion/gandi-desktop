﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace GandiDesktop.Presentation.View
{
    public partial class ResourceView : UserControl
    {
        private const string IsExpandedPropertyName = "IsExpanded";
        private const string IsDraggingPropertyName = "IsDragging";
        private const string DummyLeftPropertyName = "DummyLeft";
        private const string DummyTopPropertyName = "DummyTop";
        private const string StoryboardCollapseKey = "Collapse";
        private const string StoryboardExpandKey = "Expand";
        private const string StoryboardFadeOutKey = "FadeOut";
        private const string StoryboardFadeInKey = "FadeIn";

        public Storyboard CollapseStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardCollapseKey] as Storyboard); }
        }

        public Storyboard ExpandStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardExpandKey] as Storyboard); }
        }

        public Storyboard FadeOutStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardFadeOutKey] as Storyboard); }
        }

        public Storyboard FadeInStoryboard
        {
            get { return (this.Resources[ResourceView.StoryboardFadeInKey] as Storyboard); }
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

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.Register(
                ResourceView.IsDraggingPropertyName, 
                typeof(bool), 
                typeof(ResourceView),
                new UIPropertyMetadata(false, new PropertyChangedCallback(ResourceView.OnIsDraggingPropertyChanged)));

        public double DummyLeft
        {
            get { return (double)GetValue(DummyLeftProperty); }
            set { SetValue(DummyLeftProperty, value); }
        }

        public static readonly DependencyProperty DummyLeftProperty =
            DependencyProperty.Register(ResourceView.DummyLeftPropertyName, typeof(double), typeof(ResourceView), new UIPropertyMetadata(0.0));

        public double DummyTop
        {
            get { return (double)GetValue(DummyTopProperty); }
            set { SetValue(DummyTopProperty, value); }
        }

        public static readonly DependencyProperty DummyTopProperty =
            DependencyProperty.Register(ResourceView.DummyTopPropertyName, typeof(double), typeof(ResourceView), new UIPropertyMetadata(0.0));

        public ResourceView()
        {
            InitializeComponent();
        }

        private void OnResourceTypeMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsDragging)
                this.IsExpanded = !this.IsExpanded;
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

        private static void OnIsDraggingPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ResourceView view = (ResourceView)sender;

            if ((bool)e.NewValue)
            {
                view.FadeOutStoryboard.Begin();
            }
            else
            {
                view.FadeInStoryboard.Begin();
            }
        }
    }
}