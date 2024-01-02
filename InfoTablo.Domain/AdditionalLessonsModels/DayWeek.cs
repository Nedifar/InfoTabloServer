namespace InfoTablo.Domain.AdditionalLessonsModels
{
    public class DayWeek
    {
        public int IdDayWeek { get; set; }

        public string Name { get; set; }

        public virtual List<ScheduleAdditionalLesson> SheduleAdditionalLessons { get; set; } = new();

        public virtual List<Lesson> Lessons { get; set; } = new(); 
    }
}
