using System;
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

        public int Id { get; private set; }

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

        private string status;
        public string Status
        {
            get { return this.status; }
            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    base.OnPropertyChanged(() => Status);
                }
            }
        }

        private bool isRunning;
        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    base.OnPropertyChanged(() => IsRunning);
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

        public object Resource { get; private set; }

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

        public event ResourceDetailActionHandler DetailAction;

        public ResourceViewModel(IResource resource)
        {
            this.InitializeResource(resource);
        }

        private void InitializeResource(IResource resource)
        {
            this.Id = resource.Id;
            this.Name = resource.Name;
            this.Status = resource.Status;
            if (!String.IsNullOrEmpty(this.Status))
            {
                this.Status = this.Status.Replace('_', ' ');
                this.Status = this.Status.Substring(0, 1).ToUpperInvariant() + this.Status.Substring(1);
            }
            this.IsRunning = (this.Status == "Running");
            this.Type = resource.Type;
            this.Resource = resource;

            if (this.ResourceDetailViewModelCollection == null)
                this.ResourceDetailViewModelCollection = new ObservableCollection<ResourceDetailViewModel>();
            else
                this.ResourceDetailViewModelCollection.Clear();

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
                            ResourceDetailViewModel[] ipAddresseDetails = this.ResourceDetailViewModelCollection.Where(r => r.Type == ResourceDetailType.IpAddress).ToArray();
                            for (int j = 0; j < ipAddresseDetails.Length; j++)
                            {
                                ResourceDetailViewModel ipAddressDetail = ipAddresseDetails[j];
                                this.ResourceDetailViewModelCollection.Remove(ipAddressDetail);
                            }

                            int i = this.ResourceDetailViewModelCollection.IndexOf(resourceDetailViewModel);
                            Interface iface = (Interface)e.Resource;

                            foreach (IpAddress ipAddress in iface.IpAddresses)
                                this.ResourceDetailViewModelCollection.Insert(++i, new ResourceDetailViewModel(new IpAddressResourceDetail(ipAddress)));
                        }
                    }

                    if (this.DetailAction != null)
                        this.DetailAction(this, e);
                };

                this.ResourceDetailViewModelCollection.Add(resourceDetailViewModel);
            }
        }

        public void Update(IResource resource)
        {
            this.InitializeResource(resource);
        }
    }
}