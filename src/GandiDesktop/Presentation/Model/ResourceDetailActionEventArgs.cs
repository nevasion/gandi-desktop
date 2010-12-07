using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public delegate void ResourceDetailActionHandler(object sender, ResourceDetailActionEventArgs e);

    public class ResourceDetailActionEventArgs : EventArgs
    {
        public string Text { get; private set; }
        public ResourceDetailActionType Type { get; private set; }
        public object Resource { get; set; }
        public Operation Operation { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

        public ResourceDetailActionEventArgs(ResourceDetailActionType type, string text)
        {
            this.Type = type;
            this.Text = text;
        }

        public ResourceDetailActionEventArgs(ResourceDetailActionType type)
            : this(type, null)
        {

        }
    }
}
