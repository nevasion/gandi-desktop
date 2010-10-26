namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DiskCreation
    {
        public DataCenter DataCenter { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DiskType Type { get; set; }
        public Disk[] RepulsedDisks { get; set; }
    }
}