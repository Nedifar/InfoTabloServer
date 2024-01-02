using AutoMapper;
using InfoTablo.Application.Common.Mappings;
using InfoTablo.Domain;

namespace InfoTablo.Application.SpecialDayWeekNames.Queries.GetSpecialDay
{
    public class GetSpecialDayVm : IMapWith<SpecialDayWeekName>
    {
        public byte DayWeek { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SpecialDayWeekName, GetSpecialDayVm>()
                .ForMember(dest => dest.DayWeek,
                    opt => opt.MapFrom(vm => vm.DayWeek));
        }
    }
}
