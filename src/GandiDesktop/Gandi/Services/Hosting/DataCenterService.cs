using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class DataCenterService
    {
        private IHostingService service;
        private string apiKey;

        public DataCenterService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public DataCenter[] List()
        {
            Mapping.DataCenter[] mappingDataCenters = this.service.DataCenterList(this.apiKey);

            List<DataCenter> dataCenterList = new List<DataCenter>();
            foreach (Mapping.DataCenter mappingDataCenter in mappingDataCenters)
                dataCenterList.Add(new DataCenter(mappingDataCenter));

            DataCenter[] dataCenters = dataCenterList.ToArray();

            return dataCenters;
        }
    }
}