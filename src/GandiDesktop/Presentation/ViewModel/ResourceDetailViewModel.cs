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

        private bool hasQuickActions;
        public bool HasQuickActions
        {
            get { return this.hasQuickActions; }
            set
            {
                if (this.hasQuickActions != value)
                {
                    this.hasQuickActions = value;
                    base.OnPropertyChanged(() => HasQuickActions);
                }
            }
        }

        public ObservableCollection<ResourceDetailActionViewModel> Actions { get; private set; }

        public event ResourceDetailQuickActionHandler DetailQuickAction;

        public ResourceDetailViewModel(IResourceDetail resourceDetail)
        {
            this.Name = resourceDetail.Name;
            this.Value = resourceDetail.Value;
            this.Type = resourceDetail.Type;
            this.HasQuickActions = (resourceDetail.Actions != null && resourceDetail.Actions.Length > 0);

            this.Actions = new ObservableCollection<ResourceDetailActionViewModel>();
            if (this.HasQuickActions)
                foreach (ResourceDetailAction action in resourceDetail.Actions)
                    this.Actions.Add(new ResourceDetailActionViewModel(action));

            resourceDetail.DetailQuickAction += (sender, e) => 
            {
                if (this.DetailQuickAction != null)
                    this.DetailQuickAction(this, e);
            };
        }
    }
}