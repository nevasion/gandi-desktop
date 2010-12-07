namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DiskOperation : Operation
    {
        public int DiskId { get; private set; }
        public Disk Disk { get; private set; }

        public DiskOperation(Mapping.DiskOperation operation, Disk disk = null)
            : base(operation)
        {
            this.DiskId = operation.DiskId;
            this.Disk = disk;
        }
    }
}