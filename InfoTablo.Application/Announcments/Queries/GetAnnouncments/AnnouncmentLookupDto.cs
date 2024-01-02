using AutoMapper;
using Core.InfoTablo.Domain;
using InfoTablo.Application.Common.Mappings;

namespace InfoTablo.Application.Announcments.Queries.GetAnnouncments
{
    public class AnnouncmentLookupDto : IMapWith<Announcment>
    {
        public string? Header { get; set; }

        public string? Name { get; set; }

        public DateTime DateAdded { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Announcment, AnnouncmentLookupDto>()
                .ForMember(annDto => annDto.Name,
                    opt => opt.MapFrom(ann => ann.Name))
                .ForMember(annDto => annDto.Header,
                    opt => opt.MapFrom(ann => ann.Header))
                .ForMember(annDto => annDto.DateAdded,
                    opt => opt.MapFrom(ann => ann.DateAdded));
        }
    }
}
