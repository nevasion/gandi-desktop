namespace GandiDesktop.Model
{
    public interface IResource
    {
        string Name { get; }
        ResourceType Type { get; }
        IResourceDetail[] Details { get; }
    }
}