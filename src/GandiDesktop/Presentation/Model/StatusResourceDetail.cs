namespace GandiDesktop.Presentation.Model
{
    public class StatusResourceDetail : IResourceDetail
    {
        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.Status; }
        }

        public ResourceDetailAction[] Actions { get; private set; }

        public event ResourceDetailActionHandler DetailAction;

        public StatusResourceDetail(string status)
        {
            this.Value = status;
        }
    }
}