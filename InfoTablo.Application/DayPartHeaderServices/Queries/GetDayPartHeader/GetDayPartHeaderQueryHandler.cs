using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.DayPartHeaderServices.Queries.GetDayPartHeader
{
    public class GetDayPartHeaderQueryHandler : IRequestHandler<GetDayPartHeaderQuery, string>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public async Task<string> Handle(GetDayPartHeaderQuery request, CancellationToken cancellationToken)
        {
            var dateNow = DateTime.Now;
            var dph = await _dbContext.DayPartHeaders.ToListAsync(cancellationToken);
            var currentHeader = dph.FirstOrDefault(p => p.BeginTime.TimeOfDay <= dateNow.TimeOfDay
                && p.EndTime.TimeOfDay >= dateNow.TimeOfDay)
                ?.Header;
            return currentHeader ?? new DayPartHeader().Header;
        }
    }
}
