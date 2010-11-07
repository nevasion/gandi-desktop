namespace GandiDesktop.Presentation.Model
{
    public class TextResourceDetail : IResourceDetail
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Text; }
        }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailQuickActionHandler DetailQuickAction;

        public TextResourceDetail(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}