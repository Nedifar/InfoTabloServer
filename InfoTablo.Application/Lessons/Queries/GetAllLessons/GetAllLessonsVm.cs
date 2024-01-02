using InfoTablo.Domain.AdditionalLessonsModels;

namespace InfoTablo.Application.Lessons.Queries.GetAllLessons
{
    public class GetAllLessonsVm 
    {
        public IList<Lesson> AllLessons { get; set; }
    }
}
