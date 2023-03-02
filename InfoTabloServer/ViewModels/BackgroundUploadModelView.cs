using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace InfoTabloServer.ViewModels
{
    public class BackgroundUploadModelView
    {
        public IFormFile uploadFile { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BackgroundUploadTypes typeUpload { get; set; }

        public DateTime dateTarget { get; set; } = DateTime.Now;
    }

    public enum BackgroundUploadTypes
    {
        Main,
        Other
    }
}
