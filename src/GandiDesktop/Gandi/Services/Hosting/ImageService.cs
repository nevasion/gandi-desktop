using System.Collections.Generic;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class ImageService
    {
        private IHostingService service;
        private string apiKey;

        public ImageService(IHostingService service, string apiKey)
        {
            this.service = service;
            this.apiKey = apiKey;
        }

        public Image[] List()
        {
            return this.List(null, null);
        }

        public Image[] List(Disk[] disks)
        {
            return this.List(null, disks);
        }

        public Image[] List(DataCenter[] dataCenters)
        {
            return this.List(dataCenters, null);
        }

        public Image[] List(DataCenter[] dataCenters, Disk[] disks)
        {
            Mapping.Image[] mappingImages = this.service.image_list(this.apiKey);

            List<Image> imageList = new List<Image>();
            foreach (Mapping.Image mappingImage in mappingImages)
                imageList.Add(new Image(mappingImage, dataCenters, disks));

            Image[] images = imageList.ToArray();

            return images;
        }
    }
}