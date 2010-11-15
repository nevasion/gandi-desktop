using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GandiDesktop.Presentation.Model;
using System.Collections.ObjectModel;

namespace GandiDesktop.Presentation.ViewModel
{
    public class ResourceDetailViewModel : ViewModelBase
    {
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

        private string value;
        public string Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    base.OnPropertyChanged(() => Value);
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

        private ResourceDetailType type;
        public ResourceDetailType Type
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

        private bool hasActions;
        public bool HasActions
        {
            get { return this.hasActions; }
            set
            {
                if (this.hasActions != value)
                {
                    this.hasActions = value;
                    base.OnPropertyChanged(() => HasActions);
                }
            }
        }

        public ObservableCollection<ResourceDetailActionViewModel> Actions { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public ResourceDetailViewModel(IResourceDetail resourceDetail)
        {
            this.Name = resourceDetail.Name;
            this.Value = resourceDetail.Value;

            if (resourceDetail.Type == ResourceDetailType.Status)
                this.IsRunning = (resourceDetail.Value == "running");

            this.Type = resourceDetail.Type;
            this.HasActions = (resourceDetail.Actions != null && resourceDetail.Actions.Length > 0);

            this.Actions = new ObservableCollection<ResourceDetailActionViewModel>();
            if (this.HasActions)
                foreach (ResourceDetailAction action in resourceDetail.Actions)
                    this.Actions.Add(new ResourceDetailActionViewModel(action));

            resourceDetail.DetailAction += (sender, e) => 
            {
                if (this.DetailAction != null)
                    this.DetailAction(this, e);
            };
        }
    }
}