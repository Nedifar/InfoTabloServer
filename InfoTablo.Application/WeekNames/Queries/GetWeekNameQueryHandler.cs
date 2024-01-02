using InfoTablo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace InfoTablo.Application.WeekNames.Queries
{
    public class GetWeekNameQueryHandler : IRequestHandler<GetWeekNameQuery, string>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetWeekNameQueryHandler(IInfoTabloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetWeekNameQuery request, CancellationToken cancellationToken)
        {
            var dt = (await _dbContext.WeekNames.ToListAsync(cancellationToken))
                .LastOrDefault();

            var customCalendar = new GregorianCalendar();
            var weekNumber = customCalendar.GetWeekOfYear(dt.Begin, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            var selectedWeekNumber = customCalendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            if ((weekNumber % 2 == 0 && selectedWeekNumber % 2 == 0)
                || (weekNumber % 2 != 0 && selectedWeekNumber % 2 != 0))
            {
                return dt.Name.ToLower().Contains("верх")
                    ? "Верхняя неделя"
                    : "Нижняя неделя";
            }
            else
            {
                return dt.Name.ToLower().Contains("верх")
                    ? "Нижняя неделя"
                    : "Верхняя неделя";
            }
        }
    }
}
