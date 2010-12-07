namespace GandiDesktop.Presentation.Model
{
    public interface IResourceDetail
    {
        string Name { get; }
        string Value { get; }
        ResourceDetailType Type { get; }
        ResourceDetailAction[] Actions { get; }
        event ResourceDetailActionHandler DetailAction;
    }
}