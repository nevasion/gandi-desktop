using Mapping = GandiDesktop.Gandi.Services.Hosting.Model;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DataCenter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }

        public DataCenter(Mapping.DataCenter dataCenter)
        {
            this.Id = dataCenter.Id;
            this.Name = dataCenter.Name;
            this.Country = dataCenter.Country;
        }
    }
}