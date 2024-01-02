using InfoTablo.Application.Interfaces;
using InfoTablo.Domain.AdditionalLessonsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.Lessons.Queries.GetLessonsToday
{
    public class GetLessonsTodayQueryHandler : IRequestHandler<GetLessonsTodayQuery, IList<Lesson>>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetLessonsTodayQueryHandler(IInfoTabloDbContext dbContext)=>
            _dbContext = dbContext;

        public async Task<IList<Lesson>> Handle(GetLessonsTodayQuery request, CancellationToken cancellationToken)
        {
            var lessonsCollection = await _dbContext.Lessons.ToListAsync(cancellationToken);
            var todayLessons = lessonsCollection.Where(p => p.DayWeek.IdDayWeek == (int)DateTime.Now.DayOfWeek).ToList();
            return todayLessons;
        }
    }
}
