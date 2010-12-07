using System.IO;

namespace GandiDesktop.Presentation.Model
{
    public static class Service
    {
        public static HostingService Hosting { get; private set; }

        static Service()
        {
            GandiDesktop.Gandi.Services.GandiService service = new GandiDesktop.Gandi.Services.GandiService(File.ReadAllText("apikey.txt"));

            Hosting = new HostingService(service.Hosting);
        }
    }
}