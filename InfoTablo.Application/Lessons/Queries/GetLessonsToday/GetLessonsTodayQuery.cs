using InfoTablo.Domain.AdditionalLessonsModels;
using MediatR;

namespace InfoTablo.Application.Lessons.Queries.GetLessonsToday
{
    public record GetLessonsTodayQuery() : IRequest<IList<Lesson>>;
}
