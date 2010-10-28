namespace GandiDesktop.Model
{
    public interface IResourceDetail
    {
        string Name { get; }
        string Value { get; }
        ResourceDetailType Type { get; }
    }
}