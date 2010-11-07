using System.IO;

namespace GandiDesktop.Presentation.Model
{
    public static class Service
    {
        private static readonly GandiDesktop.Gandi.Services.GandiService service;

        public static GandiDesktop.Gandi.Services.Hosting.HostingService Hosting
        {
            get { return service.Hosting; }
        }

        static Service()
        {
            service = new GandiDesktop.Gandi.Services.GandiService(File.ReadAllText("apikey.txt"));
        }
    }
}