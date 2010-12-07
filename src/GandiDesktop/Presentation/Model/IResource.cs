using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public interface IResource
    {
        int Id { get; }
        string Name { get; }
        string Status { get; }
        ResourceType Type { get; }
        IResourceDetail[] Details { get; }
        IHostingResource Resource { get; }
        bool CanAttach { get; }
        bool CanReceiveAttachement(IHostingResource resource);
    }
}