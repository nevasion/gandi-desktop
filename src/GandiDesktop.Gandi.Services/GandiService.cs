using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Gandi.Services
{
    public class GandiService
    {
        public HostingService Hosting { get; private set; }

        public GandiService(string apiKey)
        {
            this.Hosting = new HostingService(apiKey);
        }
    }
}