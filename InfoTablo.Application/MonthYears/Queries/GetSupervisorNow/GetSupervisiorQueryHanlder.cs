using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.MonthYears.Queries.GetSupervisorNow
{
    public class GetSupervisiorQueryHanlder : IRequestHandler<GetSupervisorNowQuery, string>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetSupervisiorQueryHanlder(IInfoTabloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetSupervisorNowQuery request, CancellationToken cancellationToken)
        {
            var monthYears = await _dbContext.MonthYears.ToListAsync(cancellationToken);
            var dateNow = DateTime.Now;

            var monthResult = monthYears.FirstOrDefault(p => p.Date.Year == dateNow.Year
                && p.Date.Month == dateNow.Month);

            var selectedSupervisiorDate = await _dbContext.DatesSupervisiors.FirstOrDefaultAsync(p => p.Date.Year == dateNow.Year
                && p.Date.Month == dateNow.Month, cancellationToken);

            return GetSupervisiorFromSimpleMonthYear(selectedSupervisiorDate);
        }

        private static string GetSupervisiorFromSimpleMonthYear(DatesSupervisior supervisior)
        {
            for(int i = 1;  i <= 31; i++)
            {
                Type supervisiorType = typeof(DatesSupervisior);
                var propertyValue = supervisiorType.GetField($"d{i}")
                    ?.GetValue(supervisior);
                if(propertyValue != null)
                {
                    return propertyValue.ToString();
                }
            }
            return String.Empty;
        }
    }
}
