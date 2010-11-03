using System;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.Model
{
    public class DataCenterResourceDetail : IResourceDetail
    {
        private const string ValueTemplate = "{0} ({1})";

        public string Name { get; private set; }

        public string Value { get; private set; }

        public ResourceDetailType Type
        {
            get { return ResourceDetailType.DataCenter; }
        }

        public DataCenterResourceDetail(DataCenter dataCenter)
        {
            this.Value = String.Format(DataCenterResourceDetail.ValueTemplate, dataCenter.Name, dataCenter.Country);
        }
    }
}