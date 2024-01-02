using MediatR;

namespace InfoTablo.Application.TimeShedules.Queries
{
    public record GetAllTimeShedulesQuery() : IRequest<GetAllTimeScheduleVm>;
}
