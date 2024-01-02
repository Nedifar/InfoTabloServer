using System.ComponentModel.DataAnnotations;

namespace InfoTablo.Domain.AdditionalLessonsModels
{
    public class ScheduleAdditionalLesson
    {
        public int IdSheduleAdditionalLesson { get; set; }

        public string Name { get; set; }

        public int DurationLesson { get; set; }

        public virtual List<DayWeek> DayWeeks { get; set; } = new();

        public virtual List<Time> Times { get; set; } = new();
    }
}
