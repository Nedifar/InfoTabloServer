using AutoMapper;
using InfoTablo.Application.Common.Mappings;
using InfoTablo.Domain;
using System.Data;

namespace InfoTablo.Application.TimeShedules.Queries.GetTimeScheduleForDynamicUpdate
{
    public class GetTimeScheduleForDynamicUpdateVm : IMapWith<TimeSchedule>
    {
        public string TimeNow { get; set; }
        public string ToEndPara { get; set; }
        public double ProgressBarPara { get; set; }
        public List<Para> ParasCollection { get; set; } = new List<Para>();
        public double GrLineHeight { get; set; }
        public double ColorLineHeight { get; set; }
        public string TbNumberPara { get; set; }
        public string ParaNow { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TimeSchedule, GetTimeScheduleForDynamicUpdateVm>()
                .ForMember(dest => dest.ParaNow,
                    opt => opt.MapFrom(t => MapToParaNow(t)))
                .ForMember(dest => dest.ParasCollection,
                    opt => opt.MapFrom(t => t.Paras.OrderBy(p => p.NumberInList)
                        .ToList()))
                .ForMember(dest => dest.GrLineHeight,
                    opt => opt.MapFrom(t => MapToGrLineHeight(t)))
                .ForMember(dest => dest.ColorLineHeight,
                    opt => opt.MapFrom(t => MapToColorLineHeight(t)))
                .ForMember(dest => dest.TbNumberPara,
                    opt => opt.MapFrom(t => MapToTbNumberPara(t)))
                .ForMember(dest => dest.ToEndPara,
                    opt => opt.MapFrom(t => MapToEndTime(t)))
                .ForMember(dest => dest.ProgressBarPara,
                    opt => opt.MapFrom(t => MapToProgressBarPara(t)));
        }

        private static string MapToParaNow(TimeSchedule schedule)
        {
            var onlyParas = schedule.Paras.Where(p => p.TypeInterval.Name == "ЧКР"
                && p.TypeInterval.Name == "Пара")
                    .ToList();

            var onlyParaNow = onlyParas.OrderBy(p => p.NumberInList)
                .FirstOrDefault(p => p.End.TimeOfDay >= DateTime.Now.TimeOfDay);

            string result = onlyParaNow?.TypeInterval.Name switch
            {
                "Перемена" => "П",
                "Пара" => onlyParaNow.NumberInterval.ToString(),
                "Событие" => onlyParaNow.Name,
                "ЧКР" => "ЧКР",
                _ => throw new Exception()
            };

            return result;
        }

        private static double MapToGrLineHeight(TimeSchedule schedule)
        {
            var result = schedule.Paras.Sum(p =>
            {
                return (p.End.TimeOfDay - p.Begin.TimeOfDay).TotalMinutes;
            });

            return result;
        }

        private static double MapToColorLineHeight(TimeSchedule timeSchedule)
        {
            var parasCollection = timeSchedule.Paras.OrderBy(p => p.NumberInList)
                .ToList();

            double height = DateTime.Now.TimeOfDay.TotalMinutes - parasCollection[0].Begin.TimeOfDay.TotalMinutes;

            var result = height < 0
                ? 0
                : height;

            return result;
        }

        private static string MapToTbNumberPara(TimeSchedule timeSchedule)
        {
            var dateNow = DateTime.Now;
            var parasCollection = timeSchedule.Paras.OrderBy(p => p.NumberInList)
                .ToList();

            double height = dateNow.TimeOfDay.TotalMinutes - parasCollection[0].Begin.TimeOfDay.TotalMinutes;

            if (height < 0)
            {
                double minThreshold = (parasCollection[0].Begin.TimeOfDay - dateNow.TimeOfDay).TotalMinutes;

                return minThreshold <= 20
                    ? $"До начала пар {Math.Ceiling(minThreshold)} мин."
                    : "Пары не начались";
            }

            if (parasCollection[^1].End.TimeOfDay < dateNow.TimeOfDay)
            {
                return "Занятия закончились";
            }

            var eventNow = GetEventNow(timeSchedule);
            return eventNow?.Name;
        }

        private static string MapToEndTime(TimeSchedule timeSchedule)
        {
            var eventNow = GetEventNow(timeSchedule);

            double toEndMinutes = Math.Ceiling(eventNow.End.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes);
            var toEndHours = Math.Truncate(toEndMinutes / 60);

            if (toEndMinutes - toEndHours * 60 == 0)
                return toEndHours + " час ";
            else if (toEndHours != 0)
                return toEndHours + " час " + (toEndMinutes - toEndHours * 60) + " мин.";
            else
                return toEndMinutes + " мин.";
        }

        private static double MapToProgressBarPara(TimeSchedule timeSchedule)
        {
            var eventNow = GetEventNow(timeSchedule);
            var totalTime = (eventNow.End.TimeOfDay - eventNow.Begin.TimeOfDay).TotalMinutes;

            var result = 100 - 100 * (eventNow.End.TimeOfDay.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes) / totalTime;

            return result;
        }

        private static Para GetEventNow(TimeSchedule timeSchedule)
        {
            var dateNow = DateTime.Now;
            var eventNow = timeSchedule.Paras.OrderBy(p => p.NumberInList)
                            .FirstOrDefault(p => p.Begin.TimeOfDay <= dateNow.TimeOfDay
                                && p.End.TimeOfDay >= dateNow.TimeOfDay);

            return eventNow;
        }
    }
}
