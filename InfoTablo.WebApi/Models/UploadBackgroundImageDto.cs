using AutoMapper;
using InfoTablo.Application.BackgroundImage.Commands.UploadImage;
using InfoTablo.Application.Common.Mappings;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace InfoTablo.WebApi.Models
{
    public class UploadBackgroundImageDto : IMapWith<UploadBackgroundImageCommand>
    {
        public IFormFile UploadFile { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BackgroundUploadTypes TypeUpload { get; set; }

        public DateTime DateTarget { get; set; } = DateTime.Now;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UploadBackgroundImageDto, UploadBackgroundImageCommand>()
                .ForMember(dist => dist.Special, opt => opt.MapFrom(dto =>
                    dto.TypeUpload != BackgroundUploadTypes.Main));
        }
    }

    public enum BackgroundUploadTypes
    {
        Main,
        Other
    }
}
