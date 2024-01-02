using MediatR;
using Microsoft.AspNetCore.Http;

namespace InfoTablo.Application.BackgroundImage.Commands.UploadImage
{
    public class UploadBackgroundImageCommand : IRequest
    {
        public IFormFile UploadFile { get; set; }

        public bool Special { get; set; }

        public DateTime DateTarget { get; set; } = DateTime.Now;
    }
}
