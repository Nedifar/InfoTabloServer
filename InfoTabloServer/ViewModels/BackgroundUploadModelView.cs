namespace InfoTabloServer.ViewModels
{
    public class BackgroundUploadModelView
    {
        public IFormFile uploadFile { get; set; }

        public BackgroundUploadTypes typeUpload { get; set; }

        public DateTime dateTarget { get; set; } = DateTime.Now;
    }

    public enum BackgroundUploadTypes
    {
        Main,
        Other
    }
}
