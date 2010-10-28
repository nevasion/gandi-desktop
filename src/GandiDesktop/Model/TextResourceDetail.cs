namespace GandiDesktop.Model
{
    public class TextResourceDetail : IResourceDetail
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Text; }
        }

        public TextResourceDetail(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}