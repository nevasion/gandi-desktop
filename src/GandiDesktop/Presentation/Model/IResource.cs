namespace GandiDesktop.Presentation.Model
{
    public interface IResource
    {
        string Name { get; }
        string Status { get; }
        ResourceType Type { get; }
        IResourceDetail[] Details { get; }
    }
}