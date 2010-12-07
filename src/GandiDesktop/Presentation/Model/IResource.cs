namespace GandiDesktop.Presentation.Model
{
    public interface IResource
    {
        int Id { get; }
        string Name { get; }
        string Status { get; }
        ResourceType Type { get; }
        IResourceDetail[] Details { get; }
        object Resource { get; }
    }
}