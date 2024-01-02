using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.Paras.Queries.GetParasToday
{
    public class GetParasTodayQueryHandler : IRequestHandler<GetParasTodayQuery, IList<Para>>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetParasTodayQueryHandler(IInfoTabloDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IList<Para>> Handle(GetParasTodayQuery request, CancellationToken cancellationToken)
        {
            var timeSchedulesCollection = await _dbContext.TimeSchedules.ToListAsync(cancellationToken);
            var specialDayWeek = (await _dbContext.SpecialDayWeekNames.FirstOrDefaultAsync(cancellationToken)).DayWeek;

            List<Para> result;

            if (timeSchedulesCollection.FirstOrDefault(p => p.Name == DateTime.Now.ToShortDateString())
                is TimeSchedule specialDateSchedule)
            {
                result = GetOnlyParasCollection(specialDateSchedule);
            }
            else if ((int)DateTime.Now.DayOfWeek == specialDayWeek)
            {
                var specialDayWeekSchedule = timeSchedulesCollection.FirstOrDefault(p => p.Name == "ЧКР");
                result = GetOnlyParasCollection(specialDayWeekSchedule);
            }
            else
            {
                var mainSchedule = timeSchedulesCollection.FirstOrDefault(p => p.Name == "Основной");
                result = GetOnlyParasCollection(mainSchedule);
            }

            return result;
        }

        private static List<Para> GetOnlyParasCollection(TimeSchedule timeSchedule)
        {
            var result = timeSchedule.Paras.Where(p => p.TypeInterval.Name == "ЧКР" || p.TypeInterval.Name == "Пара")
                .OrderBy(p => p.NumberInterval)
                .ToList();

            return result;
        }
    }
}
