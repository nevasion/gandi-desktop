using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GandiDesktop.Presentation.Model;

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

        public ResourceDetailViewModel(IResourceDetail resourceDetail)
        {
            this.Name = resourceDetail.Name;
            this.Value = resourceDetail.Value;
            this.Type = resourceDetail.Type;
        }
    }
}