namespace InfoTablo.Domain.AdditionalLessonsModels
{
    public class Lesson
    {
        public int IdLesson { get; set; }

        public string GroupName { get; set; }

        public string TeacherName { get; set; }

        public string Cabinet { get; set; }

        public int IdDayWeek { get; set; }

        public virtual DayWeek DayWeek { get; set; }

        public int IdTime { get; set; }

        public virtual Time Time { get; set; }
    }
}
