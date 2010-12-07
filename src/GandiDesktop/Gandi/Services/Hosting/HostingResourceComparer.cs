using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class HostingResourceComparer : IEqualityComparer<IHostingResource>
    {
        public bool Equals(IHostingResource x, IHostingResource y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IHostingResource obj)
        {
            return obj.GetHashCode();
        }
    }
}