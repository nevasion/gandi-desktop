using CookComputing.XmlRpc;

namespace GandiDesktop.Gandi.Services.Hosting.Model
{
    public class DataCenter
    {
        [XmlRpcMember("id")]
        public int Id { get; set; }

        [XmlRpcMember("name")]
        public string Name { get; set; }

        [XmlRpcMember("country")]
        public string Country { get; set; }
    }
}