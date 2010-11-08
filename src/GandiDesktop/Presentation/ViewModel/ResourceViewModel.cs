using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GandiDesktop.Gandi.Services.Hosting;
using GandiDesktop.Presentation.Model;

namespace GandiDesktop.Presentation.ViewModel
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

        public ResourceViewModel(IResource resource)
        {
            this.Name = resource.Name;
            this.Type = resource.Type;

            this.ResourceDetailViewModelCollection = new ObservableCollection<ResourceDetailViewModel>();
            foreach (IResourceDetail detail in resource.Details)
            {
                ResourceDetailViewModel resourceDetailViewModel = new ResourceDetailViewModel(detail);

                resourceDetailViewModel.DetailAction += (sender, e) =>
                {
                    if (!e.Error)
                    {
                        if (e.Type == ResourceDetailActionType.Copy)
                        {
                            Clipboard.SetText(e.Text);
                        }
                        else if (e.Type == ResourceDetailActionType.SeeIpAddresses)
                        {
                            int i = this.ResourceDetailViewModelCollection.IndexOf(resourceDetailViewModel);
                            Interface iface = (Interface)e.Resource;

                            ResourceDetailViewModel[] ipAddresseDetails = this.ResourceDetailViewModelCollection.Where(r => r.Type == ResourceDetailType.IpAddress).ToArray();
                            for (int j = 0; j < ipAddresseDetails.Length; j++)
                            {
                                ResourceDetailViewModel ipAddressDetail = ipAddresseDetails[j];
                                this.ResourceDetailViewModelCollection.Remove(ipAddressDetail);
                            }

                            foreach (IpAddress ipAddress in iface.IpAddresses)
                                this.ResourceDetailViewModelCollection.Insert(++i, new ResourceDetailViewModel(new IpAddressResourceDetail(ipAddress)));
                        }
                        else if (e.Type == ResourceDetailActionType.Detach)
                        {
                            this.ResourceDetailViewModelCollection.Remove(resourceDetailViewModel);
                        }
                        else if (e.Type == ResourceDetailActionType.Edit)
                        {
                        }
                    }
                    else
                    {

                    }
                };

                this.ResourceDetailViewModelCollection.Add(resourceDetailViewModel);
            }
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