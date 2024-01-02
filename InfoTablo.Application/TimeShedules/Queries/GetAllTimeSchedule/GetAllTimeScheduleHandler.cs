using InfoTablo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.TimeShedules.Queries
{
    public class GetAllTimeScheduleHandler : IRequestHandler<GetAllTimeShedulesQuery, GetAllTimeScheduleVm>
    {
        private readonly IInfoTabloDbContext _dbContext;

        public GetAllTimeScheduleHandler(IInfoTabloDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<GetAllTimeScheduleVm> Handle(GetAllTimeShedulesQuery request, CancellationToken cancellationToken)
        {
            var timeSchedules = await _dbContext.TimeSchedules.ToListAsync();
            return new GetAllTimeScheduleVm { TimeSchedules = timeSchedules };
        }
    }
}
