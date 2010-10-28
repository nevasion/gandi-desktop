using System.Collections.ObjectModel;
using GandiDesktop.Model;

namespace GandiDesktop.ViewModel
{
    public class ResourceViewModel : ViewModelBase
    {
        public ObservableCollection<ResourceDetailViewModel> ResourceDetailViewModelCollection { get; set; }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    base.OnPropertyChanged(() => Name);
                }
            }
        }

        private ResourceType type;
        public ResourceType Type
        {
            get { return this.type; }
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    base.OnPropertyChanged(() => Type);
                }
            }
        }

        private double left;
        public double Left
        {
            get { return this.left; }
            set
            {
                if (this.left != value)
                {
                    this.left = value;
                    base.OnPropertyChanged(() => Left);
                }
            }
        }

        private double top;
        public double Top
        {
            get { return this.top; }
            set
            {
                if (this.top != value)
                {
                    this.top = value;
                    base.OnPropertyChanged(() => Top);
                }
            }
        }

        private int zIndex;
        public int ZIndex
        {
            get { return this.zIndex; }
            set
            {
                if (this.zIndex != value)
                {
                    this.zIndex = value;
                    base.OnPropertyChanged(() => ZIndex);
                }
            }
        }

        private bool isMouseOver;
        public bool IsMouseOver
        {
            get { return this.isMouseOver; }
            set
            {
                if (this.isMouseOver != value)
                {
                    this.isMouseOver = value;
                    base.OnPropertyChanged(() => IsMouseOver);
                }
            }
        }

        public ResourceViewModel(IResource resource)
        {
            this.Name = resource.Name;
            this.Type = resource.Type;

            this.ResourceDetailViewModelCollection = new ObservableCollection<ResourceDetailViewModel>();
            foreach (IResourceDetail detail in resource.Details)
                this.ResourceDetailViewModelCollection.Add(new ResourceDetailViewModel(detail));
        }

        public ResourceViewModel(string name, ResourceType type, double left, double top)
        {
            this.Name = name;
            this.Type = type;
            this.Left = left;
            this.Top = top;
        }
    }
}