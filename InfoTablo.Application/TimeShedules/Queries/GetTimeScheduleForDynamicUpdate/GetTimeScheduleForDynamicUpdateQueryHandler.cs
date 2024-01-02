using AutoMapper;
using InfoTablo.Application.Interfaces;
using InfoTablo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InfoTablo.Application.TimeShedules.Queries.GetTimeScheduleForDynamicUpdate
{
    public class GetTimeScheduleForDynamicUpdateQueryHandler
        : IRequestHandler<GetTimeScheduleForDynamicUpdateQuery, GetTimeScheduleForDynamicUpdateVm>
    {
        private readonly IInfoTabloDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTimeScheduleForDynamicUpdateQueryHandler(IInfoTabloDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GetTimeScheduleForDynamicUpdateVm> Handle(GetTimeScheduleForDynamicUpdateQuery request, CancellationToken cancellationToken)
        {
            var timeSchedules = await _dbContext.TimeSchedules.ToListAsync(cancellationToken);

            GetTimeScheduleForDynamicUpdateVm vm = new()
            {
                TimeNow = DateTime.Now.ToString("HH:mm"),
            };

            TimeSchedule currentSchedule;

            try
            {
                if (timeSchedules.Where(p => p.Name == DateTime.Now.ToShortDateString()).Any())
                {
                    currentSchedule = timeSchedules.FirstOrDefault(p => p.Name == DateTime.Now.ToShortDateString());
                }
                else if ((int)DateTime.Now.DayOfWeek == _dbContext.SpecialDayWeekNames.FirstOrDefault().DayWeek)
                {
                    currentSchedule = timeSchedules.FirstOrDefault(p => p.Name == "ЧКР");
                }
                else if (DateTime.Now.DayOfWeek == 0)
                {
                    vm.TbNumberPara = "Сегодня выходной";
                    return vm;
                }
                else
                {
                    currentSchedule = timeSchedules.FirstOrDefault(p => p.Name == "Основной");
                }

                vm = _mapper.Map<GetTimeScheduleForDynamicUpdateVm>(currentSchedule);
                return vm;
            }
            catch 
            { 
                return vm;
            }
        }
    }
}
