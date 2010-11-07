using System;

namespace GandiDesktop.Presentation.Model
{
    public delegate void ResourceDetailQuickActionHandler(object sender, ResourceDetailQuickActionEventArgs e);

    public class ResourceDetailQuickActionEventArgs : EventArgs
    {
        public string Text { get; private set; }
        public ResourceDetailQuickAction Type { get; private set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

        public ResourceDetailQuickActionEventArgs(ResourceDetailQuickAction type, string text)
        {
            this.Type = type;
            this.Text = text;
        }

        public ResourceDetailQuickActionEventArgs(ResourceDetailQuickAction type)
            : this(type, null)
        {
            
        }
    }

    public interface IResourceDetail
    {
        string Name { get; }
        string Value { get; }
        ResourceDetailType Type { get; }
        ResourceDetailAction[] Actions { get; }
        event ResourceDetailQuickActionHandler DetailQuickAction;
    }
}