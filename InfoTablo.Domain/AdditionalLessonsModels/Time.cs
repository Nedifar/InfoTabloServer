using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTablo.Domain.AdditionalLessonsModels
{
    public class Time
    {
        [Key]
        public int IdTime { get; set; }

        public DateTime BeginTime { get; set; } = DateTime.Now;

        public virtual List<Lesson> Lessons { get; set; } = new();

        [ForeignKey("SheduleAdditionalLesson")]
        public int IdScheduleAdditionalLesson { get; set; }

        public virtual ScheduleAdditionalLesson ScheduleAdditionalLesson { get; set; }
    }
}
